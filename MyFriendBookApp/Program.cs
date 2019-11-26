using System;
using System.Runtime.CompilerServices;

namespace MyFriendBookApp
{
    class Program
    {
        #region Set up strings

        const string windowTitle = "My Friend Book";
        const string appTitle = "******* " + windowTitle + " *******";
        const string actionPrompt = "Suchtext, Befehl oder ?: ";
        const string enterFirstName = "Vornamen eingeben: ";
        const string enterLastName = "Nachnamen eingeben: ";

        #endregion
        static void Main(string[] args)
        {
            #region Set up window parameters

            Console.Title = windowTitle;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WindowWidth = 50;
            Console.WindowHeight = 50;
            Console.SetBufferSize(50,50);
            Console.Clear();

            #endregion

            Console.WriteLine(appTitle);

            string action;
            do
            {
                Console.Write(actionPrompt);
                action = Console.ReadLine();

                if (action == "?")
                {
                    ShowHelp();
                }

                if (action == "+")
                {
                    ContactAdd();
                }
            } while (action != "-v");

            Console.Write("Auf wiedersehen.");
            
            Console.ResetColor();
        }

        static void ShowHelp()
        {
            int version = 1;
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

            Console.Write(enterFirstName);
            firstName = Console.ReadLine();
            Console.Write(enterLastName);
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
