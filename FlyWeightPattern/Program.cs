
namespace FlyWeightPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet FlyWeightPattern.exe digits");
                Console.WriteLine("Example: dotnet FlyWeightPattern.exe 1212123");
                Environment.Exit(0);
            }

            BigString bs = new BigString(args[0]);
            bs.Print();
        }

    }
}
