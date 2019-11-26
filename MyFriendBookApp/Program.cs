using System;
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

            Console.WriteLine(AppTitle);

            string action;
            do
            {
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
            string[] newContact = ContactRead();
        }

        static string[] ContactRead()
        {
            string firstName;
            string lastName;
            string[] newContact;

            Console.Write(EnterFirstName);
            firstName = Console.ReadLine();
            Console.Write(EnterLastName);
            lastName = Console.ReadLine();
            
            return new string[] {firstName, lastName};
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
    }
}
