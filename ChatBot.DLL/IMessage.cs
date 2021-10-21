namespace Chatbot.DLL
{
    /// <summary>
    /// Interface IMessage
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Schlüsselwort
        /// </summary>
        string Schlüsselwort
        {
            get;
        }

        /// <summary>
        /// Antwort
        /// </summary>
        string Antwort
        {
            get;
        }
    }
}
