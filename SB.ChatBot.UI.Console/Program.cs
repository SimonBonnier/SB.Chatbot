namespace SB.ChatBot.UI.Console
{
    using SB.ChatBot.Core.Commands;
    using SB.ChatBot.Infra.Discord;
    using SB.ChatBot.Core;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
            => await new Program().MainAsync();
        private async Task MainAsync()
        {
            var commandHandlerFactory = new CommandHandlerFactory();
            var clients = new IChatClient[]
            {
                new DiscordChatClient(commandHandlerFactory)
            };

            foreach (var client in clients)
            {
                await client.Connect();
            }

            await Task.Delay(-1);
        }
    }
}
