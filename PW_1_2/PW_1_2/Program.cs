using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PW_1_2
{
    class Program
    {
        private static int[,] graph;
        private static int wierzcholki = 100;

        static void Main(string[] args)
        {
            int numberOfThreads = 0;
            
            try
            {
                while (true){
                    Thread thread = new Thread(new ThreadStart(CreateGraph));
                    numberOfThreads++;
                    thread.Start();
                    Console.WriteLine(numberOfThreads);
                }
            }
            catch{
                Console.WriteLine("Error!!");
                Console.WriteLine("Liczba watków:" + (numberOfThreads - 1));
                Console.ReadKey();
            }

        }

        private static void CreateGraph(){
            graph = new int[wierzcholki, wierzcholki];
            Random ran = new Random();

            for (int i = 0; i < wierzcholki; i++){
                graph[i, i] = 0;
            }

            for (int i = 0; i < wierzcholki; i++){
                for (int j = 0; j < wierzcholki; j++){
                    graph[i, j] = ran.Next(0, 2);
                    graph[j, i] = graph[i, j];
                }
            }
        }
    }
}
