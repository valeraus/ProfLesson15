using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static async Task CalculateFactorialAsync(int number)
        {
            Console.WriteLine($"Before await - Thread ID: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");

            int factorial = await Task.Run(() =>
            {
                int result = 1;
                for (int i = 1; i <= number; i++)
                {
                    result *= i;
                    Thread.Sleep(1000);
                }
                return result;
            });

            Console.WriteLine($"After await - Thread ID: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Factorial of {number} is {factorial}");
        }

        static async Task Main(string[] args)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            Console.WriteLine($"Main Thread ID: {Thread.CurrentThread.ManagedThreadId}, Name: {Thread.CurrentThread.Name}, IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");

            await CalculateFactorialAsync(5);

            Console.ReadKey();
        }
    }
}
