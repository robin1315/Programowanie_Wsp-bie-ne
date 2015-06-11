using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


namespace PW
{
    class Program
    {

///////////////////////////////////////////////////  zad3
        static List<String> buffor = new List<string>();
        static int sizeOfpassword = 1;
        static Thread Producent;
        static Thread Konsument;
        static string haselkoKon = "f";
////////////////////////////////////////////////////// zad3.2

        static List<String> buffor2 = new List<string>();
        static Thread KonsumentProducent;

//////////////////////////////////////////////////// zad 2 
        static int wielkoscTab = 100000000;
        static int[] tab;

///////////////////////////////////////////////////zad 1
        static int numberOfEdge;
        static int[,] graph;
        static int wierzcholki = 100;
        static int numberOfThreads = 100;
        
        static void Main(string[] args)
        {
            
            //CreateGraph(wierzcholki, graph);
            //ViewGraph(graph);
            //zad1_1();
            //zad1_3();
            //zad2_2();
            zad3_1();


        }
        private static void zad3_2()
        {
            Producent = new Thread(new ThreadStart(Tworz));
            Konsument = new Thread(new ThreadStart(Konsumuj));
            KonsumentProducent = new Thread(new ThreadStart(KonsumujProdukuj));
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

        private static void KonsumujProdukuj()
        {
            while (true)
            {
                try
                {

                    if (buffor.First() != haselkoKon)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("[Konsument] nie pasuje: " + buffor.First());
                        Console.ResetColor();
                        string tmp;
                        tmp = (Reverse(buffor.First()));
                        buffor2.Add()
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("[konsument] wrzucam do bufora2: " + tmp);
                        buffor.Remove(buffor.First());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("[Konsument] znalazl haslo: " + buffor.First());
                        Console.ResetColor();
                        Producent.Abort();
                    }
                }
                catch
                {
                    Thread.Sleep(1);
                }
            }
        }




        private static void zad3_1() {
            Producent = new Thread(new ThreadStart(Tworz));
            Konsument = new Thread(new ThreadStart(Konsumuj));

            buffor.Add("G");
            Producent.Start();
            Konsument.Start();

            do{
                if(!Producent.IsAlive)
                {
                    Konsument.Abort();
                    Producent.Abort();
                    break;
                }
            }while(true);
            Console.Read();
        }

        private static void Konsumuj()
        {
            
            while (true) {
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
            while (true) {
                haselko = GeneratePassword(sizeOfpassword);
                buffor.Add(haselko);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[Producent] generuje" + haselko);
                Console.ResetColor();

                Thread.Sleep(1);
               
            }
        }

        private static string GeneratePassword(int size) {
            Random ch = new Random();
            char[] password = new char[size];

            for (int i = 0; i < size; i++)
                password[i] =(char)  (ch.Next(0,2) == 1?  (ch.Next(97,122)) : ch.Next(41,90)  );
            return new String(password);
        }

        ///////////////////////////////////////////////////////////// zad2
        private static void zad2_2()
        {
            CreateTable(wielkoscTab);
            SekSumTab(tab);
            ParSumTab(tab);
            Console.Read();
        }
        private static void ParSumTab(int[] tab) {
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
        private static void SekSumTab(int[] tab) {
            int sum = 0;
            Stopwatch Time = new Stopwatch();
            Time.Start();

            for (int i = 0; i < tab.Length; i++)
                sum += tab[i];
            Time.Stop();

            Console.WriteLine("Suma: " + sum);
            Console.WriteLine("Czas: " + Time.ElapsedMilliseconds);
        }
        private static void CreateTable(int size) {
            Stopwatch Time = new Stopwatch();
            Time.Start();

            tab = new int[size];

            Random rand = new Random();

            for (int i = 0; i < wielkoscTab; i++)
                tab[i] = rand.Next(0, 1000);

            Time.Stop();
            Console.WriteLine("Czas generowania tablicy:" + Time.ElapsedMilliseconds);
        }

//////////////////////////////////////////////////////////////  zad1
        private static void zad1_3() {
            CreateGraph();
            //ViewGraph(graph);

            Stopwatch ParTime = new Stopwatch();

            CountEdge();

            numberOfEdge = 0;

            ParTime.Start();
            Thread[] tabThreads = new Thread[wierzcholki];

            for (int i = 0; i < wierzcholki; i++) {
                tabThreads[i] = new Thread(new ThreadStart(CountEdgeThreads));
                tabThreads[i].Name = i.ToString();
            }

            for (int i = 0; i < wierzcholki; i++){
                tabThreads[i].Start();
                tabThreads[i].Join();
            }
            

            Console.WriteLine("Liczba krawedzi watkowo: " + numberOfEdge);
            ParTime.Stop();


            Console.WriteLine("Czas rownolegle:" + ParTime.ElapsedMilliseconds);

            Console.Read();

        }
        private static void CountEdgeThreads() {
            
            int nameOfThread = int.Parse(Thread.CurrentThread.Name);
            for (int i = 0; i < wierzcholki; i++)
                if (graph[i, nameOfThread] == 1)
                    numberOfEdge += 1;
        }
        private static void zad1_2()
        {
            int numberOfThreads = 0;
            Thread thread; 
            try
            {
                while (true) {
                    numberOfThreads++;
                    thread = new Thread(new ThreadStart(CreateGraph));
                    thread.Start();
                    Console.WriteLine(numberOfThreads);
                }
            }
            catch {
                Console.WriteLine("Error!!");
                Console.WriteLine("Liczba watków:" + (numberOfThreads-1));
                Console.ReadKey();
                return;
            }
        }
        private static void CountEdge() {
            Stopwatch SekTime = new Stopwatch();
            SekTime.Start();
            numberOfEdge = 0;

            for(int i=0;i<wierzcholki;i++)
                for (int j = 0; j < wierzcholki; j++) {
                    if (graph[i, j] == 1)
                        numberOfEdge++;
                }
            Console.WriteLine("Liczba krawedzi: " + numberOfEdge);
            SekTime.Stop();
            Console.WriteLine("Czas sekwencyjnie:" + SekTime.ElapsedMilliseconds);
            
        }
        private static void zad1_1() {

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
