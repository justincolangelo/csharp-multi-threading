using System;
using System.Threading;

namespace MultiThreadingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread th = Thread.CurrentThread;
            th.Name = "MainThread";

            Console.WriteLine($"This is {th.Name}");

            ThreadStart childRef = new ThreadStart(CallToChildThread);
            Console.WriteLine("\nIn Main: Creating the Child thread");
            Thread childThread = new Thread(childRef);
            childThread.Start();

            //stop the main thread for some time
            Thread.Sleep(2000);

            //now abort the child
            Console.WriteLine("\n\nIn Main: Aborting the Child thread");


            try
            {
                // this is a bad idea, just seeing if exception catches
                childThread.Abort();
            }
            catch(PlatformNotSupportedException ex)
            {
                Console.WriteLine($"\nPlatformNotSupportedException: {ex.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }

        public static void CallToChildThread()
        {
            try
            {
                Console.WriteLine("Child thread starts");

                // count to 20
                for (int counter = 0; counter <= 20; counter++)
                {
                    Thread.Sleep(200);
                    Console.Write(counter + (counter == 20 ? "\n\n" : ", "));
                }
                Console.WriteLine("Child Thread Completed");
            }
            catch(ThreadAbortException ex)
            {
                Console.WriteLine($"\nThread Abort Exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Couldn't catch the Thread Exception");
            }

            int sleepFor = 1000;

            Console.WriteLine($"Child Thread Paused for {sleepFor / 1000} seconds");
            Thread.Sleep(sleepFor);
            Console.WriteLine("Child thread resumes");
        }
    }
}
