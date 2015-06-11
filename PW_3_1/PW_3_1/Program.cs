using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PW_3_1
{
    class Program
    {
        static List<String> buffor = new List<string>();
        static int sizeOfpassword = 1;
        static Thread Producent;
        static Thread Konsument;
        static string haselkoKon = "f";

        static void Main(string[] args)
        {
            Producent = new Thread(new ThreadStart(Tworz));
            Konsument = new Thread(new ThreadStart(Konsumuj));

            buffor.Add("G");
            Producent.Start();
            Konsument.Start();

            do
            {
                if (!Producent.IsAlive)
                {
                    Konsument.Abort();
                    Producent.Abort();
                    break;
                }
            } while (true);
            Console.Read();
        }
        private static void Konsumuj()
        {

            while (true)
            {
                if (buffor.First() != haselkoKon)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("[Konsument] nie pasuje: " + buffor.First());
                    Console.ResetColor();
                    buffor.Remove(buffor.First());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("[Konsument] znalazl haslo: " + buffor.First());
                    Console.ResetColor();
                    Producent.Abort();
                }
                Thread.Sleep(2);
            }
        }

        private static void Tworz()
        {
            string haselko;
            while (true)
            {
                haselko = GeneratePassword(sizeOfpassword);
                buffor.Add(haselko);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[Producent] generuje" + haselko);
                Console.ResetColor();

                Thread.Sleep(1);

            }
        }

        private static string GeneratePassword(int size)
        {
            Random ch = new Random();
            char[] password = new char[size];

            for (int i = 0; i < size; i++)
                password[i] = (char)(ch.Next(0, 2) == 1 ? (ch.Next(97, 122)) : ch.Next(41, 90));
            return new String(password);
        }

    }
}
