namespace SB.ChatBot.Infra.Discord
{
    using global::Discord;
    using global::Discord.WebSocket;
    using SB.ChatBot.Core;
    using SB.ChatBot.Core.Commands;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DiscordChatClient : IChatClient
    {
        private DiscordSocketClient _client;

        private SocketGuild _channelGuild;
        private SocketTextChannel _commonTextChannel;
        private readonly CommandHandlerFactory _commandHandlerFactory;

        public event EventHandler<ChatBotLogMessage> Log;
        public event EventHandler<CommandRecivedEventArgs> OnCommandReceived;

        public DiscordChatClient(CommandHandlerFactory commandHandlerFactory)
        {
            _client = new DiscordSocketClient();
            _commandHandlerFactory = commandHandlerFactory;
        }

        public async Task Connect()
        {
            await WireUpEventHandlers();

            // Todo: move token
            var token = "";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }

        private Task WireUpEventHandlers()
        {
            _client.Log += LogMessage;

            _client.MessageUpdated += MessageUpdate;

            _client.Ready += async () =>
            {
                Console.WriteLine("Bot is connected");
                _channelGuild = _client.Guilds.FirstOrDefault(x => x.Name == DiscordChatClientConstants.GuildName);
                _commonTextChannel = _channelGuild.TextChannels.FirstOrDefault(x => x.Name == DiscordChatClientConstants.CommonChannelName);

                await _commonTextChannel.SendMessageAsync("Bot connected");
            };

            _client.MessageReceived += MessageRecivedHandler;

            return Task.CompletedTask;
        }

        private async Task MessageRecivedHandler(SocketMessage msg)
        {

            var commandHandler = _commandHandlerFactory.CreateHandler(msg.Content);

            var result = await commandHandler.HandleCommand(new SelectGameArguments());

            await _commonTextChannel.SendMessageAsync(result.ToString());
        }

        private async Task MessageUpdate(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message}, -> {after}");
        }

        public Task SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        private Task LogMessage(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
