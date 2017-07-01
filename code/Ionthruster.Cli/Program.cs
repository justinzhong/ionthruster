using Ionthruster.Middleware.Build;
using System;

namespace Ionthruster.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfig.Configure();
            Thruster.Start<BuildMiddleware>(container).Wait();

            Console.ReadKey();
        }
    }
}
