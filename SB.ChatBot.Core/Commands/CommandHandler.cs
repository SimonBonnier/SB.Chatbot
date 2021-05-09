namespace SB.ChatBot.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class CommandHandler
    {
        public abstract Task<string> HandleCommand(CommandArguments argument);
    }

    public class SelectGameHandler : CommandHandler
    {
        public const string CommandWord = "-bot selectGame";
        private readonly List<string> _games;

        public SelectGameHandler()
        {
            _games = new List<string>
            {
                "CS GO",
                "Apex",
                "Valorant"
            };
        }

        public override Task<string> HandleCommand(CommandArguments argument)
        {
            if (argument.GetType() != typeof(SelectGameArguments))
                throw new ArgumentException($"Can't call SelectGameHandler with {argument.GetType()} argument type");

            var rnd = new Random();

            var rndIndex = rnd.Next(_games.Count());
            return Task.FromResult(_games.ElementAt(rndIndex));
        }
    }

    public class HelpHandler : CommandHandler
    {
        public const string CommandWord = "-bot help";
        public override Task<string> HandleCommand(CommandArguments argument)
        {
            if (argument.GetType() != typeof(SelectGameArguments))
                throw new ArgumentException($"Can't call SelectGameHandler with {argument.GetType()} argument type");

            var msg = "Hello from bot";

            return Task.FromResult(msg);
        }
    }
}