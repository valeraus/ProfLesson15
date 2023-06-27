using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class CustomSynchronizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            Thread thread = new Thread(() => d(state));
            thread.Name = "CustomThread";
            thread.Start();
        }
    }

    class Program
    {
        static async Task CalculateFactorialAsync(int number)
        {
            Console.WriteLine($"Before await - Thread ID: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");

            int factorial = 1;
            for (int i = 1; i <= number; i++)
            {
                factorial *= i;
                await Task.Delay(1000);
            }

            Console.WriteLine($"After await - Thread ID: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Factorial of {number} is {factorial}");
        }

        static async Task Main(string[] args)
        {
            SynchronizationContext.SetSynchronizationContext(new CustomSynchronizationContext());

            Console.WriteLine($"Main Thread ID: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");

            await CalculateFactorialAsync(5);

            Console.ReadKey();
        }
    }
}
