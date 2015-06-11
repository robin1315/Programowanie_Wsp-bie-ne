using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PW_2_2
{
    class Program
    {
        private static int wielkoscTab = 100000;
        private static int[] tab;

        static void Main(string[] args)
        {
            CreateTable(wielkoscTab);
            SekSumTab(tab);
            ParSumTab(tab);
            Console.Read();

        }

        private static void ParSumTab(int[] tab)
        {
            int sum = 0;
            Stopwatch Time = new Stopwatch();
            Time.Start();

            Parallel.For(0, tab.Length, () => 0, (j, loop, subtotal) =>
            {
                subtotal += tab[j];
                return subtotal;
            },
                (x) => Interlocked.Add(ref sum, x));
            Time.Stop();
            Console.WriteLine("suma:" + sum);
            Console.WriteLine("Czas:" + Time.ElapsedMilliseconds);
        }
        private static void SekSumTab(int[] tab)
        {
            int sum = 0;
            Stopwatch Time = new Stopwatch();
            Time.Start();

            for (int i = 0; i < tab.Length; i++)
                sum += tab[i];
            Time.Stop();

            Console.WriteLine("Suma: " + sum);
            Console.WriteLine("Czas: " + Time.ElapsedMilliseconds);
        }
        private static void CreateTable(int size)
        {
            Stopwatch Time = new Stopwatch();
            Time.Start();

            tab = new int[size];

            Random rand = new Random();

            for (int i = 0; i < wielkoscTab; i++)
                tab[i] = rand.Next(0, 1000);

            Time.Stop();
            Console.WriteLine("Czas generowania tablicy:" + Time.ElapsedMilliseconds);
        }

    }
}
