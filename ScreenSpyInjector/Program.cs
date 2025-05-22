using EasyHook;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ScreenSpyInjector
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter process name to inject (e.g., chrome, zoom): ");
                string processName = Console.ReadLine();

                var target = Process.GetProcessesByName(processName).FirstOrDefault();
                if (target == null)
                {
                    Console.WriteLine($"Process '{processName}' not found.");
                    return;
                }

                string hookPath = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "ScreenSpyHook.dll");

                RemoteHooking.Inject(
                    target.Id,
                    InjectionOptions.Default,
                    hookPath,
                    hookPath,
                    null);

                Console.WriteLine($"Successfully injected into {processName} (PID: {target.Id})");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Injection failed: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
