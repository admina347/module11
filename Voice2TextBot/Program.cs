using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Voice2TextBot.Configuration;
using Voice2TextBot.Controllers;
using Voice2TextBot.Services;

namespace Voice2TextBot
{
    public class Program
    {
        public static async Task Main()
        {
            //Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "5401639153:AAHLJi-WffbcNhn1Is7VcI8F5hiWelpfx8s",
                DownloadsFolder = "/home/admina/Загрузки/",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
            };
        }
        static void ConfigureServices(IServiceCollection services)
        {
            //settings
            AppSettings appSettings = BuildAppSettings();
            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            //
            services.AddSingleton<IStorage, MemoryStorage>();
            //
            services.AddSingleton<IFileHandler, AudioFileHandler>();
            //
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }
    }
}