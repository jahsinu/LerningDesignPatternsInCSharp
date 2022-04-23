using pagemaker;

// 構成上Facadeというネームスペースにしたが、こいつはClientであって
// FacadeパターンでのFacade役はPageMaker.cs
namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
          PageMaker.makeWelcomePage("hyuki@hyuki.com", "welcome.html");
        }
    }
}
