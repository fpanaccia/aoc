using System.Reflection;
using System.Windows.Input;

namespace _2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var days = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
                .Select((t, i) => Activator.CreateInstance(t) as IDay);

            foreach (var day in days)
            {
                var description = $"{day.ToString().Split(".").Last().Replace("_", " ")}: ";
                Console.WriteLine(description + day.Run());
            }

            Console.ReadKey();
        }
    }
}