namespace SB.ChatBot.UI.Console
{
    using SB.ChatBot.Core.Commands;
    using SB.ChatBot.Infra.Discord;
    using SB.ChatBot.Core;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using System;

    class Program
    {
        static async Task Main(string[] args)
            => await new Program().MainAsync();
        private async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var commandHandlerFactory = new CommandHandlerFactory();
            var clients = new IChatClient[]
            {
                new DiscordChatClient(config.GetSection("DiscordToken").Value, commandHandlerFactory)
            };

            foreach (var client in clients)
            {
                await client.Connect();
            }

            await Task.Delay(-1);
        }
    }
}
