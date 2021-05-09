namespace SB.ChatBot.Core
{
    using System;
    using System.Threading.Tasks;

    public interface IChatClient
    {
        Task Connect();
        Task SendMessage(string message);

        event EventHandler<CommandRecivedEventArgs> OnCommandReceived;
    }
}