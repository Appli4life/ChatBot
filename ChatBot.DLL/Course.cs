using System;
using System.Diagnostics;
using System.IO;

namespace Chatbot.DLL
{
    /// <summary>
    /// Verlauf Klasse
    /// </summary>
    public static class Course
    {
        #region Eigenschaften

        /// <summary>
        /// String für den Pfad des Verlaufs
        /// </summary>
        public static string pfad = "Course.txt";

        /// <summary>
        /// Letzte Eingaben
        /// </summary>
        public static string[] verlauf = new string[10];

        #endregion

        #region Methoden
        /// <summary>
        ///  Verlauf Textdatei erneuern
        /// </summary>
        /// <param name="v">Verlauf</param>
        /// <exception cref="Exception"></exception>
        public static void Verlaufschreiben(string v)
        {
        againw:
            try
            {
                // Schreiben in die Verlaufdatei (true = nicht überschreiben)
                StreamWriter streamw = new StreamWriter(pfad, true);
                streamw.Write(v+"\n");
                streamw.Close();
            }
            catch (FileNotFoundException)
            {
                // Falls nicht gefunden
                File.Create(pfad);
                goto againw;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Verlauflöschen
        /// </summary>
        public static void Verlauflöschen()
        {
            StreamWriter stream = new StreamWriter(pfad);
            stream.WriteLine("");
            stream.Close();
        }

        /// <summary>
        /// Verlauf anzeigen (Verlaufdatei öffnen)
        /// </summary>
        public static void Verlaufanzeigen()
        {
            Process.Start(Course.pfad);
        }

        /// <summary>
        /// Jedes Stellen in dem Array eins nach hinten Verschieben
        /// </summary>
        /// <param name="eingabe">Neuste Eingabe</param>
        public static void VerlaufArraySchieben(ref string eingabe)
        {
            for (int i = 0; i <= 8; i++)
            {
                verlauf[i] = verlauf[i + 1];
            }
            verlauf[9] = eingabe;
        }

        #endregion
    }
}
