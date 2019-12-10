using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MyFriendBookApp
{
    class Program
    {
        #region Set up strings

        private const string WindowTitle = "My Friend Book";
        private const string AppTitle = "******* " + WindowTitle + " *******";
        private const string ActionPrompt = "Suchtext, Befehl oder ?: ";
        private const string EnterFirstName = "Vornamen eingeben: ";
        private const string EnterLastName = "Nachnamen eingeben: ";
        private const string EnterAdditional = "Infotext: ";

        private static string[][] contacts = new string[100][];
        private static int _count = 0;

        private enum MessageType
        {
            Info = 6,
            Error = 12,
            Success = 10
        }

        #endregion

        static void Main(string[] args)
        {
            #region Set up window parameters

            Console.Title = WindowTitle;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WindowWidth = 50;
            Console.WindowHeight = 40;
            Console.SetBufferSize(50, 40);
            LoadAll();

            #endregion

            string action;
            do
            {
                DrawInterface();
                Console.Write(ActionPrompt);
                action = Console.ReadLine();

                if (action == "?")
                {
                    ShowHelp();
                }

                else if (action == "+")
                {
                    ContactAdd();
                }

                else if (action.StartsWith("* "))
                {
                    if (int.TryParse(action.Substring(2), out int index))
                    {
                        ContactEdit(index - 1);
                    }
                }
                else if (action.StartsWith("- "))
                {
                    if (int.TryParse(action.Substring(2), out int index)
                        && index - 1 <= contacts.Length
                        && index - 1 >= 0)
                    {
                        ContactRemove(index);
                    }
                }
                else if (action == "-s")
                {
                    SaveAll();
                    break;
                }
                else if (action.StartsWith("-e "))
                {
                    string path = action.Substring(2);
                    if (File.Exists(path))
                    {
                        ShowMessage("Die Datei existiert bereits. Bitte anderen Pfad angeben!", MessageType.Error);
                        break;
                    }
                    ExcelExport(path);
                    
                }
                else
                {
                    ContactSearch(action);
                }
            } while (action != "-v");

            Console.Clear();
            Console.Write("Auf wiedersehen.");

            Console.ResetColor();
        }

        static void ShowHelp()
        {
            Console.Clear();
            DrawInterface();
            const int version = 1;
            string infoText = "\n\n  Ersteller: Wilhelmy, Wulf\n" +
                              "  Version: " + version + "\n\n" +
                              "  ?               : Diese Hilfe anzeigen\n" +
                              "  +               : Kontakt zufügen\n" +
                              "  * <Index>       : Kontakt bearbeiten\n" +
                              "  - <Index>       : Kontakt löschen\n" +
                              "  -i <Dateipfad>  : Excel-CSV Import\n" +
                              "  -e <Dateipfad>  : Excel-CSV Export\n" +
                              "  -v              : Verwerfen und beenden\n" +
                              "  -s              : Speichern und beenden\n" +
                              "  (Leerzeichen ist ggf. optional)\n\n";
            Console.Write(infoText);
        }

        static void ContactAdd()
        {
            Console.Clear();
            DrawInterface();
            string[] newContact;
            do
            {
                newContact = ContactRead();
            } while (newContact == null);

            try
            {
                contacts[_count++] = newContact;
            }
            catch (IndexOutOfRangeException e)
            {
                ShowMessage("Das Programm unterstützt maximal " + contacts.Length + " Einträge! Fehler: " + e.Message, MessageType.Error);
            }
            finally{ Console.Clear(); }
        }

        static string[] ContactRead()
        {
            Console.Clear();
            string firstName;
            string lastName;
            string[] additionalInfo = new string[100];

            DrawInterface();
            Console.Write(EnterFirstName);
            firstName = Console.ReadLine();
            Console.Write(EnterLastName);
            lastName = Console.ReadLine();

            for (int index = 0; index < additionalInfo.Length; index++)
            {
                Console.Write(EnterAdditional);
                additionalInfo[index] = Console.ReadLine();
                if (additionalInfo[index] == "")
                {
                    additionalInfo[index] = null;
                    break;
                }
            }

            if (lastName == "" && firstName == "")
            {
                ShowMessage("Bitte mindestens ein Feld füllen!", MessageType.Error);
                return null;
            }

            string[] returnValue = new string[additionalInfo.Length + 2];
            returnValue[0] = firstName;
            returnValue[1] = lastName;
            additionalInfo.CopyTo(returnValue, 2);
            return returnValue;
        }

        static void ContactSearch(string pattern)
        {
            Console.Clear();
            DrawInterface();

            Console.WriteLine("\nSuchergebnis:");

            if (pattern == "")
            {
                foreach (string[] contact in contacts)
                {
                    if (contact == null) break;
                    Console.Write("{0,3}: {1}, {2}", (Array.IndexOf(contacts, contact)+1).ToString(), contact[0], contact[1]);
                    for (int index = 2; index < contact.Length; index++)
                    {
                        if (contact[index] != null)
                        {
                            Console.Write(", {0}", contact[index]);
                        }
                    }
                    Console.Write("\n");
                }
            }

            foreach (string[] contact in contacts)
            {
                if (contact == null) break;
                if (contact.Contains(pattern))
                {
                    Console.Write("{0,3}: {1}, {2}", (Array.IndexOf(contacts, contact)+1).ToString(), contact[0], contact[1]);
                    for(int index = 2; index < contact.Length; index++)
                    {
                        if (contact[index] != null)
                        {
                            Console.Write(", {0}", contact[index]);
                        }
                    }
                    Console.Write("\n");
                }
            }
        }

        static string ContactToString(string[] contact, string separator)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string attribute in contact)
            {
                if (attribute != null)
                {
                    stringBuilder.Append(attribute + separator);
                }
            }

            return stringBuilder.ToString().Trim();
        }

        static string[] StringToContact(string contact)
        {
            return contact.Split("\t");
        }

        static void ContactEdit(int index)
        {
            contacts[index] = ContactRead();
            ShowMessage("Kontakt #" + (index + 1) + " geändert!", MessageType.Success);
        }

        static void ContactRemove(int index)
        {
            Console.WriteLine("Noch nicht fertig"); // TODO: Remove Contact
        }

        static void ExcelImport(string path)
        {
            Console.WriteLine("Noch nicht fertig"); // TODO: CSV import
        }

        static void ExcelExport(string path)
        {
            string[] csvHeader = {"Index", "Vorname", "Nachname", "Infotext"};
            using (StreamWriter streamWriter = new StreamWriter(path, false, new UTF8Encoding(true)))
            {
                streamWriter.WriteLine(ContactToString(csvHeader, ","));
                foreach (string[] contact in contacts)
                {
                    if(contact != null)
                    {
                        streamWriter.WriteLine(ContactToString(contact, ","));
                    }
                }
            }
            ShowMessage("Export erfolgreich!", MessageType.Success);
        }

        static void SaveAll()
        {
            ClearMessage();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
            string file = Path.Combine(desktop, "daten.mcb");

            using (StreamWriter streamWriter = new StreamWriter(file, false, new UTF8Encoding(true)))
            {
                streamWriter.WriteLine("MyContactBookApp");
                foreach (string[] contact in contacts)
                {
                    if (contact != null)
                    {
                        streamWriter.WriteLine(ContactToString(contact, "\t"));
                    }
                }
            }
            ShowMessage("Speicherung erfolgreich.", MessageType.Success);
        }

        static void LoadAll()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
            string file = Path.Combine(desktop, "daten.mcb");

            if(File.Exists(file))
            {
                using(StreamReader streamReader = new StreamReader(file, new UTF8Encoding(true)))
                {
                    string header = streamReader.ReadLine(); // "Header einlesen
                    if (header == null)
                    {
                        ShowMessage("Die Datei konnte nicht geladen werden!", MessageType.Info);
                        return;
                    }

                    if (header.Trim() != "MyContactBookApp") return;
                    while (!streamReader.EndOfStream)
                    {
                        for (int index = 0; index <= contacts.Length; index++)
                        {
                            string contact = streamReader.ReadLine();
                            if (contact != null)
                            {
                                contacts[index] = StringToContact(contact);
                            }
                        }
                    }
                }
            }
            else
            {
                File.Create(file);
            }
        }

        static void ShowMessage(string message, Enum type)
        {
            int leftPosition = Console.WindowWidth / 2 - (message.Length / 2);
            Console.SetCursorPosition(leftPosition, Console.WindowHeight - 2);
            Console.ForegroundColor = (ConsoleColor)type;
            Console.Write(message);
            Console.SetCursorPosition(0, 2);
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        static void ClearMessage()
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.WindowHeight - 2);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 2);
        }

        static void DrawInterface()
        {
            WriteTitle();
            ClearLine(3);
            Console.SetCursorPosition(Console.CursorLeft, 3);
        }

        static void ClearLine(int line)
        {
            int lastLine = Console.CursorTop;
            Console.SetCursorPosition(Console.CursorLeft, line);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, lastLine);
        }

        static void ClearLine(int start, int stop)
        {
            int lastLine = Console.CursorTop;
            for (int index = start; index < stop; index++)
            {
                Console.SetCursorPosition(Console.CursorLeft, index);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, lastLine);
        }

        static void WriteTitle()
        {
            int left = Console.WindowWidth / 2 - (AppTitle.Length / 2);
            Console.SetCursorPosition(left, 1);
            Console.WriteLine(AppTitle);
        }
    }
}