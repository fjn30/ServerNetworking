using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientExample
{
    public class Profiling
    {
        private static Stopwatch watch;

        public static void StartProfile()
        {

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            String warmup = ""; ;
            for (int i = 0; i < 10; i++)
            {
                warmup += i;
            }

            watch = Stopwatch.StartNew();
            
        }

        public static void EndProfile()
        {
            watch.Stop();
            Console.WriteLine(" Time Elapsed {0} ms", watch.Elapsed.TotalMilliseconds);
        }

    }
}
