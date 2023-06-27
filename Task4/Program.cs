using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Створення контексту синхронізації з обробкою помилок
            var synchronizationContext = new CustomSynchronizationContext();

            // Встановлення створеного контексту синхронізації
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);

            // Виклик асинхронного методу з типом void
            await MyVoidAsyncMethod();

            // Очікування завершення обробки помилок
            synchronizationContext.Complete();

            Console.ReadKey();
        }

        static async Task MyVoidAsyncMethod()
        {
            await Task.Delay(1000);

            // Генерація помилки
            throw new Exception("Помилка в асинхронному методі з типом void");
        }
    }

    // Клас контексту синхронізації з обробкою помилок
    class CustomSynchronizationContext : SynchronizationContext
    {
        // Очередь для зберігання помилок
        private readonly Queue<Exception> exceptionQueue = new Queue<Exception>();

        // Перевизначений метод Post, який обробляє помилки
        public override void Post(SendOrPostCallback d, object state)
        {
            try
            {
                d(state);
            }
            catch (Exception ex)
            {
                exceptionQueue.Enqueue(ex);
            }
        }

        // Метод для завершення обробки помилок
        public void Complete()
        {
            while (exceptionQueue.Count > 0)
            {
                var exception = exceptionQueue.Dequeue();
                Console.WriteLine($"Помилка: {exception.Message}");
            }
        }
    }
}
