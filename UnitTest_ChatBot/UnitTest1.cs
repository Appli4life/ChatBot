using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Chatbot.DLL;
using System.IO;

namespace Chatbot2.Test
{
    /// <summary>
    /// Test Klasse
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Antwort Testen
        /// </summary>
        [TestMethod]
        public void GetAntwort()
        {
            string a = "Hallo";
            BotEngine.storage.Dateilöschen();
            BotEngine.storage.Dateierstellen();
            Assert.AreEqual("Hi!", BotEngine.GetAntwort(ref a));
        }

        /// <summary>
        /// Aktualisieren löschen
        /// </summary>
        [TestMethod]
        public void Aktualisieren()
        {
            BotEngine.storage.Dateilöschen();
            BotEngine.storage.Dateierstellen();

            BotEngine.storage.Aktualisieren();

            Assert.AreEqual(2, BotEngine.Messages.Count);

        }

        /// <summary>
        /// Verlaufschreiben
        /// </summary>
        [TestMethod]
        public void Verlaufschreiben()
        {
            File.Delete(Course.pfad);
            
            string aktuellekonversation = "Du: HalloBot: Hi!";
            
            Course.Verlaufschreiben(aktuellekonversation);

            StreamReader stream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\Course.txt");
            string alle = stream.ReadToEnd();
            stream.Close();

            Assert.AreEqual("Du: HalloBot: Hi!\n", alle);
           
        }

        /// <summary>
        /// Schlüsselwort Hinzufügen
        /// </summary>
        [TestMethod]
        public void Hinzufügen()
        {
            Storage s = new Storage();
            StreamWriter ss = new StreamWriter(s.pfad);
            ss.Write("");
            ss.Close();

            s.Hinzufügen("Moin", "Hallo");
            StreamReader st = new StreamReader(s.pfad);
            Assert.AreEqual("Moin,Hallo", st.ReadToEnd());
        }
    }
}
