using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace PW1_3
{
    class Program
    {
        private static int numberOfEdge;
        private static int[,] graph;
        private static int wierzcholki = 100;
        
        static void Main(string[] args)
        {
            CreateGraph();
            Stopwatch ParTime = new Stopwatch();
            CountEdge();
            numberOfEdge = 0;

            ParTime.Start();
            Thread[] tabThreads = new Thread[wierzcholki];

            for (int i = 0; i < wierzcholki; i++)
            {
                tabThreads[i] = new Thread(new ThreadStart(CountEdgeThreads));
                tabThreads[i].Name = i.ToString();
            }

            for (int i = 0; i < wierzcholki; i++)
            {
                tabThreads[i].Start();
                tabThreads[i].Join();
            }


            Console.WriteLine("Liczba krawedzi watkowo: " + numberOfEdge);
            ParTime.Stop();


            Console.WriteLine("Czas rownolegle:" + ParTime.ElapsedMilliseconds);

            Console.Read();

        }

        private static void CountEdgeThreads()
        {

            int nameOfThread = int.Parse(Thread.CurrentThread.Name);
            for (int i = 0; i < wierzcholki; i++)
                if (graph[i, nameOfThread] == 1)
                    numberOfEdge += 1;
        }

        private static void CountEdge()
        {
            Stopwatch SekTime = new Stopwatch();
            SekTime.Start();
            numberOfEdge = 0;

            for (int i = 0; i < wierzcholki; i++)
                for (int j = 0; j < wierzcholki; j++)
                {
                    if (graph[i, j] == 1)
                        numberOfEdge++;
                }
            Console.WriteLine("Liczba krawedzi: " + numberOfEdge);
            SekTime.Stop();
            Console.WriteLine("Czas sekwencyjnie:" + SekTime.ElapsedMilliseconds);

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
