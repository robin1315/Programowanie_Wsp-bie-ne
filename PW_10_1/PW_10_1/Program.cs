using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PW_10_1
{
    class Program
    {
        private static int[,] graph;
        private static int wierzcholki = 4;
        private static int[] tsum = new int[wierzcholki];
        static void Main(string[] args)
        {
            CreateGraph();
            //ViewGraph(graph);
            int ile = 0;
            Thread[] threads = new Thread[wierzcholki];

            for (int i = 0; i < wierzcholki; i++) {
                threads[i] = new Thread(new ThreadStart(triangles));
                threads[i].Name = i.ToString();
            }

            for (int i = 0; i < wierzcholki; i++) {
                threads[i].Start();
                threads[i].Join();
            }
            for (int i = 0; i < wierzcholki; i++) {
                ile += tsum[i];
            }
            Console.WriteLine(ile);
            Console.Read();
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
        private static void triangles()
        {
            int i = int.Parse(Thread.CurrentThread.Name), j, k, t=0;
                for (j = 0; j < wierzcholki; j++)
                {
                    if (graph[i, j] != 0)
                        for (k = 0; k < wierzcholki; k++) { 
                            if (graph[i, k] != 0 && graph[j, k] != 0) t++; }
                }
                    int x =(int.Parse(Thread.CurrentThread.Name));
                     tsum[x] = t / 3;
        }

        private static void ViewGraph(int[,] graph)
        {
            for (int i = 0; i < wierzcholki; i++)
            {
                for (int j = 0; j < wierzcholki; j++)
                {
                    Console.Write(graph[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
