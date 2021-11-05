using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Chatbot.DLL;
using ChatBot.DLL;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace Chatbot2
{
    /// <summary>
    /// Main Window
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variablen, MainWindow, Delegate
        // Festlegen von Variablen und Objekten
        Boolean darkmode = false;
        Boolean hinzufügen = false;
        Boolean antwort = false;
        Boolean first = true;
        Boolean löschen = false;
        MediaPlayer mp = new MediaPlayer();
        string a = "";
        string s = "";
        int i = 0;
        int l = 0;
        string passwort;
        const string dateierstellt = "Eine Speicherdatei wurde erstellt.";
        const string dateigelöscht = "Die Speicherdatei wurde falls vorhanden gelöscht!";
        Storage.DelHinzufügen hinzu = BotEngine.storage.Hinzufügen;
        Storage.DelAktualisieren aktu = BotEngine.storage.Aktualisieren;
        //Storage.DelLöschen lösch = BotEngine.storage.Löschen;

        public MainWindow()
        { 
            InitializeComponent();
        }
        

        #endregion

        #region Methoden und Buttonklicks
        // Senden Button
        private void senbut_Click(object sender, RoutedEventArgs e)
        {
        
            // Für den Verlauf
            string aktuellekonversation;
            string eingabe = writebox.Text;

            try
            {
                l = i;
                if (first)
                {
                    aktu();
                    first = false;
                }
                // Wenn alle Schlüsselwörter angezeigt werden sollen
                if (eingabe == "ALLE" && antwort == false)
                {
                    löschen = true;
                    Chatbox.Text += $"Bot: Alle Schlüsselworter:\n\n";

                    foreach (var item in BotEngine.Messages)
                    {
                        Chatbox.Text += $"SW: {item.Schlüsselwort} \t AW: {item.Antwort}\n";
                    }

                    Chatbox.Text += $"\nAnzahl: {BotEngine.Messages.Count}\n\n";

                    writebox.Clear();
                    //Chatbox.Text += $"Geben Sie ein Schlüsselwort ein, welches Sie löschen möchten.\nOder geben Sie 'n' ein.\n";
                    //löschen = true;
                }
                //Wenn Schlüsselwort gelöscht werden soll
                //else if (löschen && eingabe.ToLower() != "n")
                //{
                //    lösch(eingabe);

                //    Chatbox.Text += $"Bot: Schlüsselwort wurde gelöscht!\n";
                //    löschen = false;
                //    writebox.Clear();
                //}

                // Wenn das Schlüsselwort hinzugefügt werden soll
                else if (eingabe.ToLower() == "y" && hinzufügen)
                {
                    writebox.Clear();
                    Chatbox.Text += $"Bot: Wie soll die Antwort auf das Schlüsselwort sein?\n\n";
                    antwort = true;
                    hinzufügen = false;
                }
                // Wenn das Schlüsselwort nicht hinzugefügt werden soll
                else if (eingabe.ToLower() == "n" && antwort == false)
                {
                    Chatbox.Text += $"Bot: Ok!\n\n";
                    hinzufügen = false;
                    löschen = false;
                    writebox.Clear();
                }
                // Wenn das Schlüsselwort hinzugefügt worden ist
                else if (antwort)
                {
                    a = eingabe;
                    hinzu(s, a);
                    antwort = false;
                    hinzufügen = false;
                    löschen = false;
                    Chatbox.Text += $"Bot: Schlüsselwort wurde Hinzugefügt!\nSchlüsselwort: {s} Antwort: {a}\n\n";
                    writebox.Clear();
                }
                // Wenn es eine Normale Eingabe ist
                else
                {
                    if (i != 0)
                    {
                        if (eingabe == Course.verlauf[i -1])
                        {
                            goto skip;
                        }
                    }


                    if (i == 10)
                    {
                        Course.VerlaufArraySchieben(ref eingabe);
                    }
                    else
                    {
                        Course.verlauf[i] = eingabe;
                        i++;
                        l++;
                    }

                    skip:

                    hinzufügen = false;
                    antwort = false;
                    löschen = false;

                    // Wenn TextBox nicht leer ist
                    if (eingabe != "" && eingabe != " " && eingabe != ". . . ")
                    {

                        // Die eingegebene Nachricht anzeigen
                        aktuellekonversation = $"Du: {eingabe} \t {DateTime.Now.ToString("d")}, {DateTime.Now.ToString("HH:mm")}\n";
                        Chatbox.Text += $"Du: {eingabe} \t {DateTime.Now.ToString("HH:mm")} \n \a";

                        string antwort = BotEngine.GetAntwort(ref eingabe);

                        // Bot schreibt Antwort
                        aktuellekonversation += $"Bot: {antwort}\n";
                        Chatbox.Text += $"Bot: {antwort}\n\n";
                        writebox.Clear();
                        // Verlauf führen
                        Course.Verlaufschreiben(aktuellekonversation);
                    }
                }
            }
            // Wenn Speicherdatei nicht gefunden worden ist
            catch (FileNotFoundException fnfe)
            {
                // Methode aufrufen
                FileNotFound(fnfe);
            }
            // Wenn Schlüsselwort nicht gefunden worden ist
            catch (SWNotFoundException)
            {
                // Bot schreibt er hat es nicht verstanden
                Chatbox.Text += "Bot: Dieses Schlüsselwort kenne ich nicht.\nWollen Sie dieses Hinzufügen? (y/n)\n\n";
                writebox.Text = "";
                hinzufügen = true;
                löschen = false;
                s = eingabe;

            }
            // Jede andere Exception die auftreten könnte
            catch (Exception ex)
            {
                // Default Fehlermeldung
                Fehler(ex);
            }
            finally
            {
                Chatbox.ScrollToEnd();
            }
        }

        // Top Secret
        private void writebox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (writebox.Text == "")
            {
                l = i;
            }

        }

        // Darkmode aktivieren/deaktivieren
        private void switchmode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Wenn File da ist ton abspielen
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Dark-mode-switch-sound.mp3"))
                {
                    mp.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\Dark-mode-switch-sound.mp3", UriKind.RelativeOrAbsolute));
                    // Lautstärke einstellen und abspielen
                    mp.Volume = 0.5;
                    mp.Play();
                }
                else
                {


                }
                Thread.Sleep(500);

            }
            catch (Exception ex)
            {
                Fehler(ex);
            }

            // Wenn Darkmode an/ab ist
            if (darkmode)
            {
                // Hintergründe
                writebox.Background = Brushes.White;
                Chatbox.Background = Brushes.White;
                senbut.Background = Brushes.LightGreen;
                Main.Background = Brushes.White;
                speicherdateiae.Background = Brushes.LightBlue;
                speicherdateidel.Background = Brushes.LightCyan;
                speicherdateiers.Background = Brushes.GreenYellow;
                verlaufanzeigen.Background = Brushes.LightSalmon;
                verlauflöschen.Background = Brushes.Orange;
                neustart.Background = Brushes.Yellow;
                sync.Background = Brushes.Transparent;

                // Schriftfarben
                writebox.Foreground = Brushes.Black;
                Chatbox.Foreground = Brushes.Black;
                senbut.Foreground = Brushes.Black;
                darkmode = false;
            }
            else
            {
                // Hintergründe
                writebox.Background = Brushes.Black;
                Chatbox.Background = Brushes.Black;
                senbut.Background = Brushes.DarkGreen;
                Main.Background = Brushes.Black;
                speicherdateiae.Background = Brushes.DarkBlue;
                speicherdateidel.Background = Brushes.DarkCyan;
                speicherdateiers.Background = Brushes.DarkGreen;
                verlaufanzeigen.Background = Brushes.DarkSalmon;
                verlauflöschen.Background = Brushes.DarkOrange;
                neustart.Background = Brushes.OrangeRed;
                sync.Background = Brushes.White;

                // Schriftfarben
                writebox.Foreground = Brushes.White;
                Chatbox.Foreground = Brushes.White;
                senbut.Foreground = Brushes.White;
                darkmode = true;
            }
        }

        // Neustart Button
        private void neustart_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        // Speicherdatei erstellen Button
        private void speicherdateiers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Methode zur Dateierstellung
                BotEngine.storage.Dateierstellen();

                // MessageBox anzeigen
                DateiErstelltGelöschtMB(dateierstellt, "Erfolgreich erstellt!");
            }
            // Fehler abfangen
            catch (Exception ec)
            {
                Fehler(ec);
            }

        }

        // Speicherdatei löschen Button
        private void speicherdateidel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sind Sie Sicher, dass Sie diese Datei löschen wollen?", "Achtung!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    // Methode zur löschung der Datei
                    BotEngine.storage.Dateilöschen();

                    // Löschung Aller Schlüsselwörter
                    BotEngine.Messages.Clear();

                    // MessageBox anzeigen
                    DateiErstelltGelöschtMB(dateigelöscht, "Erfolgreich gelöscht!");
                }
                catch (Exception ex)
                {
                    Fehler(ex);
                }
            }
        }

        // Speicherdatei bearbeiten Button
        private void speicherdateiae_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Versuchen die Datei zu öffnen
                Process.Start(BotEngine.storage.pfad);
            }
            // Fehler abfangen
            catch (FileNotFoundException fnfe)
            {
                FileNotFound(fnfe);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                FileNotFound(ex);
            }
            catch (Exception ex)
            {
                Fehler(ex);
            }
        }

        // Beenden Button
        private void beenden_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Methode für anzeigen einer MessageBox wenn Speicherdatei nicht gefunden wurde
        public static void FileNotFound(Exception fnfe)
        {
            // Falls Antowort auf MessageBox == Yes ist, eine neue Speicherdatei erstellen
            if (MessageBox.Show("Es wurde keine Speicherdatei für die Schlüsselwörter Gefunden!\nWollen Sie eine Erstellen?", "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BotEngine.storage.Dateierstellen();
                    DateiErstelltGelöschtMB(dateierstellt, "Erfolgreich erstellt!");
                }
                catch (Exception et)
                {
                    Fehler(et);
                }
            }
        }

        // Unbekannte Fehler, MessageBox anzeigen mit Fehlermeldung
        public static void Fehler(Exception ex)
        {
            MessageBox.Show(ex.Message, "Fehler!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Methode zur anzeigen einer MessageBox, wenn Speicherdatei erstellt oder gelöscht wurde
        public static void DateiErstelltGelöschtMB(string Text, string Titel)
        {
            MessageBox.Show(Text, Titel, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Verlauf löschen Button
        private void verlauflöschen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Course.Verlauflöschen();
                Chatbox.Clear();
                hinzufügen = false;
                antwort = false;
                MessageBox.Show("Verlauf wurde gelöscht", "Erfolgreich gelöscht", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Fehler(ex);
            }
        }

        // Verlauf anzeigen Button
        private void verlaufanzeigen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Course.Verlaufanzeigen();
            }
            catch (Exception ex)
            {
                Fehler(ex);
            }
        }

        // Enterdrücken bei Eingabe
        private void writebox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                senbut_Click(sender, e);

            }

        }

        // Sync Button
        private void sync_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                aktu();
            }
            catch (FileNotFoundException ex)
            {
                FileNotFound(ex);
            }
            catch (Exception ex)
            {
                Fehler(ex);
            }
        }

        // Verlauf der Eingaben Anzeigen
        private void writebox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Down && writebox.Text != "" && l <= 9)
                {
                    l++;
                    if (l == 10)
                    {
                        writebox.Clear();
                    }
                    else
                    {
                        writebox.Text = Course.verlauf[l];
                    }

                }
                if (e.Key == Key.Up && l > 0)
                {
                    l--;
                    writebox.Text = Course.verlauf[l];
                }
            }
            catch (Exception ex)
            {
                Fehler(ex);
            }


        }

        #endregion
    }
}
