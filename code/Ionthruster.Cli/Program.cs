using Ionthruster.Middleware.Build;
using System;

namespace Ionthruster.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Thruster.Start<BuildMiddleware>().Wait();

            Console.ReadKey();
        }
    }
}
