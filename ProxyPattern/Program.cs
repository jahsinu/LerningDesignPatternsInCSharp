namespace ProxyPattern
{
    /// <summary>
    /// Client(依頼人)の役
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IPrintable p = new PrinterProxy("Alice");
            Console.WriteLine($"名前は現在{p.getPrinterName()}です。");
            p.setPrinterName("Bob");
            Console.WriteLine($"名前は現在{p.getPrinterName()}です。");
            p.print("Hello, world.");
        }
    }
}