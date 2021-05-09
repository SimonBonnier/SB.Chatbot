namespace SB.ChatBot.Core.Commands
{
    using System;

    public class CommandHandlerFactory
    {
        public CommandHandler CreateHandler(CommandType commandType)
        {
            if (commandType == CommandType.SelectGame)
            {
                return new SelectGameHandler();
            }
            throw new ArgumentException("Command handler not found");
        }

        public CommandHandler CreateHandler(string msg)
        {
            CommandType commandType = GetCommandType(msg);
            return CreateHandler(commandType);
        }

        private CommandType GetCommandType(string msg)
        {
            if (msg.Contains(SelectGameHandler.CommandWord))
            {
                return CommandType.SelectGame;
            }

            return CommandType.NotFound;
        }
    }
}