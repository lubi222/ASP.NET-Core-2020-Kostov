using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeNumbersCounter
{
    class Program
    {
        private static int Count = 0;
        private static object lockObj = new object();
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            PrintPrimeCount(1, 10_000_000);
            Console.WriteLine(sw.Elapsed);

            //for (int i = 1; i <= 20; i++)
            //{
            //    var url = $"https://vicove.com/vic-{i}";
            //    var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
            //    var vic = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            //    Console.WriteLine(vic.Length);
            //}
            //List<Task> tasks = new List<Task>();
            //for (int i = 1; i <= 100; i++)
            //{
            //    var task = Task.Run(async () =>
            //    {
            //        HttpClient httpClient = new HttpClient();
            //        var url = $"https://vicove.com/vic-{i}";
            //        var response = await httpClient.GetAsync(url);
            //        var vic = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine(vic.Length);
            //    });
            //}


            //new Thread(() =>
            //{
            //    try
            //    {
            //        throw new Exception();
            //    }
            //    catch (Exception e)
            //    {

            //    }
            //});
            //List<int> numbers = Enumerable.Range(0, 10000).ToList();
            //for (int i = 0; i < 4; i++)
            //{
            //    new Thread(() =>
            //    {
            //        while (numbers.Count > 0)
            //        {
            //            numbers.RemoveAt(numbers.Count - 1);
            //        }
            //    }).Start();
            //}

            //return; 

            ////Task.Run(PrintPrimeCount);
            //Thread thread = new Thread(PrintPrimeCount); // PrintPrimeCount will be the main method of this new thread // we can also use lambda expressions 
            //thread.Start();
            ////thread.Join();
            //while (true)
            //{
            //    var input = Console.ReadLine();
            //    Console.WriteLine(input.ToUpper());
            //}
        }

        static async Task DownloadAsync(int i)
        {
            HttpClient httpClient = new HttpClient();
            var url = $"https://vicove.com/vic-{i}";
            var response = await httpClient.GetAsync(url);
            var vic = await response.Content.ReadAsStringAsync();
            Console.WriteLine(vic.Length);

        }
        static void PrintPrimeCount(int min, int max)
        {
            //for (int i = min; i <= max; i++)
            Parallel.For(min, max + 1, i =>
            {
                bool isPrime = true;
                for (int j = 2; j < Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    lock (lockObj)
                    {
                        Count++;
                    }
                }
            });

            Console.WriteLine(Count);
        }
    }
}
