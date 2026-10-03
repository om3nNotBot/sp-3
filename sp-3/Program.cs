using System;
using System.Threading;

namespace systemprogram3
{
    internal class Program
    {
        static object lockObject = new object();

        static string cardNumber = "0000 0000 1111 1111";
        static string fullName = "Mahmud Fatmaniyova";
        static decimal balance = 500.0m;

        static void ProcessTransaction(string source, decimal amount)
        {
            lock (lockObject)
            {
                Console.WriteLine($"\n=== {source} Muracieti ===");
                Console.WriteLine($"Cart No: {cardNumber} \n" +
                    $"Card User: {fullName}");
                Console.WriteLine($"Money need: {amount} AZN \n" +
                    $"Your balance: {balance} AZN");

                if (balance >= amount)
                {
                    balance -= amount;
                    Console.WriteLine($"===SUCCESSFULL===\n" +
                        $"From:{source} \n" +
                        $"-{amount} AZN \n" +
                        $"Your balance: {balance} AZN");
                }
                else
                {
                    Console.WriteLine($"===ERROR=== \n" +
                        $"{source} Yeterli qeder pul yoxdur! \n" +
                        $"Your balance: {balance} AZN");
                }
            }
        }

        static void Main(string[] args)
        {
            Thread marketThread = new Thread(() => ProcessTransaction("Market", 200.0m));
            Thread atmThread = new Thread(() => ProcessTransaction("ATM", 250.0m));
            Thread marketThread2 = new Thread(() => ProcessTransaction("Market", 110.0m));

            marketThread.Start();
            atmThread.Start();
            marketThread2.Start();

            marketThread.Join();
            atmThread.Join();
            marketThread2.Join();

            Console.WriteLine($"\nEmeliyyatlar bitdi. Yekun balans: {balance} AZN");
        }
    }
}