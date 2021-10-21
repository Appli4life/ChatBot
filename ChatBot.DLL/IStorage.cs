namespace Chatbot.DLL
{
    /// <summary>
    /// Interface ILoad
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Aktualisieren
        /// </summary>
        void Aktualisieren();

        /// <summary>
        /// Hinzufügen
        /// </summary>
        /// <param name="s">Schlüsselwort</param>
        /// <param name="a">Antwort</param>
        void Hinzufügen(string s, string a);

    }
}
