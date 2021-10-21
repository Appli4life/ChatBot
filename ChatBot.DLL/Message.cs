namespace Chatbot.DLL
{
    /// <summary>
    /// Message Klasse
    /// </summary>
    public class Message : IMessage
    {
        #region Eigenschaften
        /// <summary>
        /// Eingegebenes Schlüsselwort
        /// </summary>
        private string schlüsselwort;

        public string Schlüsselwort
        {
            get { return schlüsselwort; } 
        }


        /// <summary>
        /// Antwort basierend auf dem Schlüsselwort
        /// </summary>
        private string antwort;

        public string Antwort
        {
            get { return antwort; }
        }

        #endregion

        #region Konstruktoren
        /// <summary>
        /// Ctor
        /// </summary>
        public Message()
        {

        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="sw">Schlüsselwort</param>
        /// <param name="an">Antwort</param>
        public Message(ref string sw, ref string an)
        {
            schlüsselwort = sw;
            antwort = an;
        }   

        #endregion
    }
}
