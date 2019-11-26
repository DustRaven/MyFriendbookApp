using System;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

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
        private static int count = 0;

        #endregion
        static void Main(string[] args)
        {
            #region Set up window parameters

            Console.Title = WindowTitle;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WindowWidth = 50;
            Console.WindowHeight = 50;
            Console.SetBufferSize(50,50);
            Console.Clear();

            #endregion

            string action;
            do
            {
                WriteTitle();
                ClearLine(1);
                Console.SetCursorPosition(Console.CursorLeft, 1); 
                Console.Write(ActionPrompt);
                action = Console.ReadLine();

                switch (action)
                {
                    case "?":
                        ShowHelp();
                        break;
                    case "+":
                        ContactAdd();
                        break;
                    default:
                        break;
                }
                Console.Clear();
            } while (action != "-v");

            Console.Write("Auf wiedersehen.");
            
            Console.ResetColor();
        }

        static void ShowHelp()
        { 
            const int version = 1;
            string infoText = "\n  Ersteller: Wilhelmy, Wulf\n" +
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
            string[] newContact;
            do
            {
                newContact = ContactRead();
            } while (newContact == null);

            contacts[count++] = newContact;
        }

        static string[] ContactRead()
        {
            string firstName;
            string lastName;
            string additionalInfo

            Console.Write(EnterFirstName);
            firstName = Console.ReadLine();
            Console.Write(EnterLastName);
            lastName = Console.ReadLine();
            Console.WriteLine(EnterAdditional);

            if (lastName == "" && firstName == "")
            {
                ShowError("Bitte mindestens ein Feld füllen!");
                return null;
            }
            
            ClearError();
            return new string[] {firstName, lastName, additionalInfo};
        }

        static void ContactSearch(string pattern)
        {
            Console.WriteLine("Noch nicht fertig");
        }

        static void ContactEdit(int index)
        {
            Console.WriteLine("Noch nicht fertig");
        }

        static void ContactRemove(int index)
        {
            Console.WriteLine("Noch nicht fertig");
        }

        static void ExcelImport(string path)
        {
            Console.WriteLine("Noch nicht fertig");
        }

        static void ExcelExport(string path)
        {
            Console.WriteLine("Noch nicht fertig");
        }

        static void SaveAll()
        {
            Console.WriteLine("Noch nicht fertig");
        }

        static void ShowError(string message)
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.WindowHeight -2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.SetCursorPosition(0, 2);
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        static void ClearError()
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.WindowHeight -2);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 2);
        }

        static void ClearLine(int line)
        {
            int lastLine = Console.CursorTop;
            Console.SetCursorPosition(Console.CursorLeft, line);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, lastLine);
        }

        static void WriteTitle()
        {
            int left = Console.WindowWidth / 2 - (AppTitle.Length / 2);
            Console.SetCursorPosition(left, 0);
            Console.WriteLine(AppTitle);
        }
    }
}
