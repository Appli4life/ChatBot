using System;
using System.IO;

namespace Chatbot.DLL
{
    /// <summary>
    /// Storage Klasse
    /// </summary>
    public class Storage : IStorage
    {
        #region Eigenschaften

        /// <summary>
        /// String für den Pfad des Speichers
        /// </summary>
        public string pfad = AppDomain.CurrentDomain.BaseDirectory + @"\Storage.txt";

        /// <summary>
        /// String Array für alle Schlüsselwörter und Antworten
        /// </summary>
        private string[] Messages = new string[1];

        #endregion

        #region Delegates

        public delegate void DelAktualisieren();
        public delegate void DelHinzufügen(string s, string a);
        public delegate void DelLöschen(string sw);

        #endregion

        #region Methoden
        /// <summary>
        /// Speicherdatei suchen und falls vorhanden in ein Array speichern
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        public void Aktualisieren()
        {
            // Damit Sie nicht doppelt sind
            BotEngine.Messages.Clear();

            // Falls Pfad existiert
            if (File.Exists(pfad))
            {
                StreamReader stream = new StreamReader(pfad);
                // Datei bis zum Ende lesen
                string alle = stream.ReadToEnd();
                stream.Close();

                // In ein Array Speichern
                Messages = alle.Split(',');

                if (Messages.Length % 2 != 0)
                {
                    throw new Exception(message: "Antwort zum Schlüsselwort oder Schlüsselwort zur Antowort,\nfehlt in der Speicherdatei!");
                }

                // Für jedes Schlüsselwort eine neue Message bilden
                for (int i = 0; i < Messages.Length; i += 2)
                { 
                    // Message erstellen 
                    Message m = new Message(ref Messages[i], ref Messages[i + 1]);

                    // Message hinzufügen
                    BotEngine.Messages.Add(m);
                }
            }
            // Sonst eine Exception werfen
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Schlüsselwort Hinzufügen
        /// </summary>
        /// <param name="s">Schlüsselwort</param>
        /// <param name="a">Antwort</param>
        public void Hinzufügen(string s, string a)
        {
            StreamReader sr = new StreamReader(pfad);

            if (sr.ReadToEnd() == "")
            {
                sr.Close();
                StreamWriter stream = new StreamWriter(pfad, true);
                stream.Write($"{s},{a}");
                stream.Close();
            }
            else
            {
                sr.Close();
                StreamWriter stream = new StreamWriter(pfad, true);
                stream.Write($",{s},{a}");
                stream.Close();
            }
            BotEngine.Messages.Add(new Message(ref s, ref a));
        }

        ///// <summary>
        ///// Schlüsselwort löschen
        ///// </summary>
        ///// <param name="sw">Das zu löschende Schlüsselwort</param>
        //public void Löschen(string sw)
        //{
           
           
        //}

        #region Dateimethoden

        /// <summary>
        /// Speicherdatei erstellen oder falls vorhanden überschreiben
        /// </summary>
        public void Dateierstellen()
        {
            if (File.Exists(pfad))
            {
                throw new Exception("Datei ist bereits vorhanden!");
            }
            File.WriteAllText(pfad, "Hallo,Hi!,Guten Tag,Servus");
            Aktualisieren();
        }

        /// <summary>
        /// Speicherdatei falls vorhanden löschen
        /// </summary>
        public void Dateilöschen()
        {
            if (File.Exists(pfad))
            {
                File.Delete(pfad);
            }
        }

        #endregion

        #endregion
    }
}
