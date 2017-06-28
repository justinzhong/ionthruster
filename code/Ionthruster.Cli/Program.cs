using Ionthruster.Middleware;
using System;
using System.Threading.Tasks;

namespace Ionthruster.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => Thruster.Start<BuildMiddleware>()).Wait();

            Console.ReadKey();
        }
    }
}
