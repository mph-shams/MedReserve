using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UserEntity = Domain.Entities.User;

namespace Infrastructure.Identity;

public class TelegramBotService : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<TelegramBotService> _logger;

    public TelegramBotService(IConfiguration config, IServiceScopeFactory scopeFactory, ILogger<TelegramBotService> logger)
    {
        _botClient = new TelegramBotClient(config["Telegram:BotToken"]!);
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: stoppingToken
        );

        _logger.LogInformation("Telegram Bot started with SendTextMessageAsync.");
        await Task.Delay(-1, stoppingToken);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
    {
        if (update.Message is not { Text: { } messageText } message) return;
        var chatId = message.Chat.Id;

        if (messageText.StartsWith("/start"))
        {
            var menu = new ReplyKeyboardMarkup(new[]
            {
                new[] { new KeyboardButton("My Appointments"), new KeyboardButton("Description") },
            })
            { ResizeKeyboard = true };

            await botClient.SendTextMessageAsync(chatId, "Welcome to MedReserve Bot!\n\nUse /linkbyusername [YourUsername] or /linkbyid [YourId] to connect your account.", replyMarkup: menu, cancellationToken: ct);
        }
        else if (messageText.StartsWith("/description") || messageText == "Description")
        {
            await botClient.SendTextMessageAsync(chatId, "MedReserve is a clinic management system.\nThis bot helps you track appointments.", cancellationToken: ct);
        }
        else if (messageText.StartsWith("/linkbyusername"))
        {
            var parts = messageText.Split(' ');
            if (parts.Length < 2) { await botClient.SendTextMessageAsync(chatId, "Usage: /linkbyusername YourUsername", cancellationToken: ct); return; }
            await LinkUser(chatId, parts[1], true, ct);
        }
        else if (messageText.StartsWith("/linkbyid"))
        {
            var parts = messageText.Split(' ');
            if (parts.Length < 2) { await botClient.SendTextMessageAsync(chatId, "Usage: /linkbyid YourId", cancellationToken: ct); return; }
            await LinkUser(chatId, parts[1], false, ct);
        }
        else if (messageText == "My Appointments")
        {
            await SendUserAppointments(chatId, ct);
        }
    }

    private async Task LinkUser(long chatId, string identifier, bool isUsername, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        UserEntity? user = isUsername
            ? (await uow.Repository<UserEntity>().GetAllAsync()).FirstOrDefault(u => u.Username == identifier)
            : await uow.Repository<UserEntity>().GetByIdAsync(int.Parse(identifier));

        if (user == null)
        {
            await _botClient.SendTextMessageAsync(chatId, "User not found in MedReserve.", cancellationToken: ct);
            return;
        }

        user.TelegramChatId = chatId;
        uow.Repository<UserEntity>().Update(user);
        await uow.SaveChangesAsync(ct);

        await _botClient.SendTextMessageAsync(chatId, $"Account '{user.Username}' linked successfully!", cancellationToken: ct);
    }

    private async Task SendUserAppointments(long chatId, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var user = (await uow.Repository<UserEntity>().GetAllAsync())
            .FirstOrDefault(u => u.TelegramChatId == chatId);

        if (user == null)
        {
            await _botClient.SendTextMessageAsync(chatId, "Account not linked. Use /linkbyusername.", cancellationToken: ct);
            return;
        }

        var appointments = (await uow.Repository<Domain.Entities.Appointment>().GetAllAsync())
            .Where(a => a.PatientId == user.Id)
            .ToList();

        if (!appointments.Any())
        {
            await _botClient.SendTextMessageAsync(chatId, "No appointments found.", cancellationToken: ct);
            return;
        }

        string list = "Your Appointments:\n\n";
        foreach (var app in appointments)
        {
            list += $"📅 Date: {app.AppointmentDate:yyyy-MM-dd HH:mm}\nStatus: {app.Status}\n------------------\n";
        }
        await _botClient.SendTextMessageAsync(chatId, list, cancellationToken: ct);
    }

    public async Task SendNotification(long chatId, string message)
    {
        await _botClient.SendTextMessageAsync(chatId, message);
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
    {
        _logger.LogError(exception, "Bot Error");
        return Task.CompletedTask;
    }
}