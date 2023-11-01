using CRMBOT.Database.Contexts;
using Telegram.Bot;
using Telegram.Bot.Types;

internal class CrmBot
{
    private static ITelegramBotClient botClient = new TelegramBotClient("6986911869:AAEX1k-ekEQpjrhK-J9ZNOVbr8ejE3W4Yfc");

    private static void Main()
    {
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        botClient.StartReceiving(HandleUpdateAsync,
                HandleErrorAsync
            );

        Console.WriteLine("Бот запущен. Нажмите Enter, чтобы остановить.");
        Console.ReadLine();
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        var message = update.Message;
        if (message != null)
        {
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Добро пожаловать! Введите /help для справки.", cancellationToken: token);
            }
            else if (message.Text == "/help")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Список команд:\n/help - справка\n/hello - ваше имя и дата задания\n/inn ИНН1 ИНН2 - информация по компаниям", cancellationToken: token);
            }
            else if (message.Text == "/hello")
            {
                var userInfo = $"ФИО: Гаракоев Адам\nEmail: garakoev600@yandex.ru\nДата получения задания: 31.10.2023";
                await botClient.SendTextMessageAsync(message.Chat.Id, userInfo, cancellationToken: token);
            }
            else if (message.Text.Contains("/inn"))
            {
                var inns = message.Text.Split(' ').Skip(1).ToArray();
                if (inns.Length == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, укажите ИНН компании в формате - /inn ИНН1 ИНН2.", cancellationToken: token);
                    return;
                }

                foreach (var inn in inns)
                {
                    var companyInfo = GetCompanyInfoByINN(inn);

                    await botClient.SendTextMessageAsync(message.Chat.Id, companyInfo, cancellationToken: token);
                }
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Неизвестная команда. Введите /help для справки.", cancellationToken: token);
            }
        }
    }

    private static string GetCompanyInfoByINN(string inn)
    {
        var model = new DatabaseContext();
        var company = model.Companies.FirstOrDefault(x => x.INN.ToString() == inn);

        if (company != null)
        {
            var companyName = company.Name;
            var companyDescription = company.Description;
            var companyContactInformation = company.ContactInformation;
            var companyINN = company.INN;

            return $"Название компании: {companyName}\nОписание компании: {companyDescription}\nКонтактная информация компании: {companyContactInformation}\nИНН компании: {companyINN}";
        }
        else
        {
            return "Компании с таким ИНН нет";
        }
    }
}