using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMuVoter.Utilities
{
    public static class PasswordMasker
    {
        private const char MASKING_CHAR = '*';

        public static string ReadPasswordFromConsole()
        {
            string password = "";

            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write(MASKING_CHAR);

                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (string.IsNullOrEmpty(password) == false)
                    {
                        password = password.Substring(0, password.Length - 1);                  // remove one character from the list of password characters

                        int pos = Console.CursorLeft;

                        // Removes last typed character by replacing it with space
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }

                info = Console.ReadKey(true);
            }

            Console.WriteLine();                            // add a new line because user pressed enter at the end of their password

            return password;
        }
    }
}
