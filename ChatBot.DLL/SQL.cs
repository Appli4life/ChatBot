using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.DLL
{
    /// <summary>
    /// SQL Klasse
    /// Verbindung zur Alarmanlage
    /// </summary>
    public static class SQL
    {
        /// <summary>
        /// IP Adresse der Internetbox
        /// </summary>
        public static string path { get; } = "192.168.1.125";

        /// <summary>
        /// Fragt den Boolean in der Tabelle AnAus ab
        /// </summary>
        /// <returns>Den Boolean in der Tabelle AnAus</returns>
        public static bool getAnAus()
        {
            bool AnAus = false;

            return AnAus;
        }
    }
}
