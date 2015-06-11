using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace PW_1_1
{
    class Program
    {
        private static int numberOfThreads=100;
        private static int[,] graph;
        private static int wierzcholki=100;
        

        static void Main(string[] args)
        {
            Stopwatch CreateTime = new Stopwatch();
            Stopwatch DestroyTime = new Stopwatch();

            int AvrCreate = 0;
            int AvrDestroy = 0;

            for (int i = 0; i < numberOfThreads; i++)
            {
                Thread thread = new Thread(new ThreadStart(CreateGraph));

                CreateTime.Start();
                thread.Start();
                CreateTime.Stop();

                DestroyTime.Start();
                thread.Abort();
                DestroyTime.Stop();

                AvrCreate += (int)CreateTime.ElapsedMilliseconds;
                AvrDestroy += (int)DestroyTime.ElapsedMilliseconds;

                //Console.WriteLine("sekundy:"+ AvrCreate.Seconds+ ", milisekundy:" + AvrCreate.Milliseconds);
                //Console.WriteLine("sekundy:" + AvrDestroy.Seconds + ", milisekundy:" + AvrDestroy.Milliseconds);
            }

            Console.WriteLine("Sredni czas utworzenia watku:");
            Console.WriteLine("milisekundy:" + (AvrCreate / numberOfThreads));

            Console.WriteLine("Sredni czas  zwolnienia watku::");
            Console.WriteLine("milisekundy:" + (AvrDestroy / numberOfThreads));
            Console.ReadKey();
     
        }
        private static void CreateGraph()
        {

            graph = new int[wierzcholki, wierzcholki];
            Random ran = new Random();

            for (int i = 0; i < wierzcholki; i++)
            {
                graph[i, i] = 0;
            }

            for (int i = 0; i < wierzcholki; i++)
            {
                for (int j = 0; j < wierzcholki; j++)
                {
                    graph[i, j] = ran.Next(0, 2);
                    graph[j, i] = graph[i, j];
                }
            }
        }
    }
}
