namespace SB.ChatBot.Core
{
    using System.Collections.Generic;

    public class CommandRecivedEventArgs
    {
        public string CommandWord { get; set; }
        public string DisplayName { get; set; }
        public List<string> Arguments { get; set; }
    }
}