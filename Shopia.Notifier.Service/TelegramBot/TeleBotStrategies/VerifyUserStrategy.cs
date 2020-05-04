using Elk.Core;
using Elk.Http;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Shopia.Notifier.Service
{
    public class VerifyUserStrategy : ITeleBotStrategy
    {
        public void ProcessRequest(TelegramBotClient botClient, object sender, MessageEventArgs eventArgs)
        {
            var TeleBotUser = new
            {
                MobileNumber = long.Parse(eventArgs.Message.Contact.PhoneNumber),
                ChatId = eventArgs.Message.From.Id
            };

            HttpRequestTools.PostJson<IResponse<bool>>("http://shopia.me/notifier/verifyTelebotuser", TeleBotUser);
        }
    }
}
