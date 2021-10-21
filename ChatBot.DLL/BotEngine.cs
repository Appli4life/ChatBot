using System.Collections.Generic;

namespace Chatbot.DLL
{
    /// <summary>
    /// BotEngine Klasse
    /// </summary>
    public static class BotEngine 
    {
        #region Eigenschaften

        /// <summary>
        /// Speicher
        /// </summary>
        public static Storage storage = new Storage();

        /// <summary>
        /// Liste Aller Messages
        /// </summary>
        public static List<Message> Messages = new List<Message>();

        #endregion

        #region Methoden

        /// <summary>
        /// Antwort anhand des Schlüsselwortes herausfinden, mit der Storage.txt Datei
        /// </summary>
        /// <returns>Die Antwort zum Aktuellen Schlüsselwort</returns>
        /// <exception cref="SWNotFoundException"></exception>
        public static string GetAntwort(ref string eingabe)
        {
            // Für jedes 2. Feld im Speicher
            foreach (var m in Messages)
            {
                if(eingabe.ToLower() == m.Schlüsselwort.ToLower())
                {
                    return m.Antwort;
                }

            }
            // Falls nicht gefunden, eigenen Exception werfen
            throw new SWNotFoundException();
        }

        #endregion
    }
}
