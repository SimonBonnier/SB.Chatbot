namespace SB.ChatBot.Core
{
    using System;

    public class ChatBotLogMessage
    {
        public readonly LogType LogType;
        public readonly string Message;
        public readonly Exception Exception;

        public ChatBotLogMessage(LogType logType, string message, Exception exception = null)
        {
            LogType = logType;
            Message = message;
            Exception = exception;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}