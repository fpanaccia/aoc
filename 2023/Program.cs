using _2023.Days;
using System.Reflection;

namespace _2023
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var days = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
                .Select((t, i) => Activator.CreateInstance(t) as IDay);

            foreach (var day in days)
            {
                var description = $"{day.ToString().Split(".").Last().Replace("_", " ")}: ";
                Console.WriteLine(description);
                await day.Run();
            }

            Console.ReadKey();
        }
    }
}