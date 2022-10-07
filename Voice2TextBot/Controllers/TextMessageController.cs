using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
namespace Voice2TextBot.Controllers;

public class TextMessageController
{
    private readonly ITelegramBotClient _telegramClient;
    public TextMessageController(ITelegramBotClient telegramBotClient)
    {
        _telegramClient = telegramBotClient;
    }
    public async Task Handle(Message message, CancellationToken ct)
    {
        switch (message.Text)
        {
            case "/start":

                // Объект, представляющий кноки
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($" Подсчёт количества символов" , $"sLen"),
                        InlineKeyboardButton.WithCallbackData($" Вычисление суммы чисел," , $"nSum")
                    });

                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот считает количестов симоволов в строке.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}Или вычисляет сумму чисел.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                break;
            default:
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Отправьте текст.", cancellationToken: ct);
                break;
        }
    }
}
