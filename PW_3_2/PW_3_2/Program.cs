using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PW_3_2
{
    class Program
    {
        private static List<String> buffor = new List<string>();
        private static List<String> buffor2 = new List<string>();
        private static int sizeOfpassword = 1;
        private static Thread Producent;
        private static Thread Konsument;
        private static Thread KonsumentProducent;
        private static string haselkoKon = "f";
        private static string haselkoKonProd = " h";

        static void Main(string[] args){
            Producent = new Thread(new ThreadStart(Tworz));
            Konsument = new Thread(new ThreadStart(Konsumuj));
            KonsumentProducent = new Thread(new ThreadStart(KonsumujProdukuj));
            
            buffor.Add("G");
            buffor2.Add("e");
            
            Producent.Start();
            Konsument.Start();
            KonsumentProducent.Start();

            do{
                if (!Producent.IsAlive){
                    Konsument.Abort();
                    Producent.Abort();
                    KonsumentProducent.Abort();
                    break;
                }
            } while (true);
            Console.Read();
        }

        private static void KonsumujProdukuj(){
            while (true){
                try{
                    if (buffor.First() != haselkoKonProd){
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("[Konsument] nie pasuje: " + buffor.First());
                        Console.ResetColor();
                        string tmp;
                        tmp = (Reverse(buffor.First()));
                        buffor2.Add(tmp);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("[konsument] wrzucam do bufora2: " + tmp);
                        buffor.Remove(buffor.First());
                    }
                    else{
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("[Konsument] znalazl haslo: " + buffor.First());
                        Console.ResetColor();
                        Producent.Abort();
                    }
                }
                catch{
                    Thread.Sleep(1);
                }
            }
        }

        private static string Reverse(string napis)
        {
            if (napis == null)
                return null;
            char[] charlist = napis.ToCharArray();
            int length = napis.Length - 1;
            for (int i = 0; i < length; i++) {
                charlist[i] ^= charlist[length];
                charlist[length] ^= charlist[i];
                charlist[i] ^= charlist[length];
            }
            return new string(charlist);
        }

        private static void Konsumuj(){
            while (true){
                try{
                    if (buffor2.First() != haselkoKon){
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("[Konsument2] nie pasuje: " + buffor2.First());
                        Console.ResetColor();
                        buffor2.Remove(buffor2.First());
                    }
                    else{
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("[Konsument2] znalazl haslo: " + buffor2.First());
                        Console.ResetColor();
                        Producent.Abort();
                    }
                }
                catch{
                    Thread.Sleep(1);
                }
            }
        }

        private static void Tworz(){
            string haselko;
            while (true){
                haselko = GeneratePassword(sizeOfpassword);
                buffor.Add(haselko);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[Producent] generuje" + haselko);
                Console.ResetColor();
                Thread.Sleep(1);
            }
        }

        private static string GeneratePassword(int size){
            Random ch = new Random();
            char[] password = new char[size];

            for (int i = 0; i < size; i++)
                password[i] = (char)(ch.Next(0, 2) == 1 ? (ch.Next(97, 122)) : ch.Next(41, 90));
            return new String(password);
        }


    }
}
