using System;
using System.Text;

namespace TemplatePattern
{
    /// <summary>
    /// AbstractClass役
    /// </summary>
    public abstract class AbstaractDisplay
    {
        // テンプレートメソッドで使用するメソッドを
        // 抽象メソッドとして規定。
        // 詳細な実装は派生クラスに任せる。
        public abstract void Open();
        public abstract void Print();
        public abstract void Close();

        // テンプレートメソッド
        // 共通のロジックを決める
        public void Display()
        {
            Open();
            for(int i = 0; i < 5; i++)
            {
                Print();
            }
            Close();
        }
    }

    /// <summary>
    /// ConcreteClass役1
    /// </summary>
    public class CharDisplay : AbstaractDisplay
    {
        private char Ch { get; set; } = (char)0;
        public CharDisplay(char ch)
        {
            Ch = ch;
        }

        // スーパークラスの抽象メソッドを実装する
        public override void Open()
        {
            Console.Write("<<");
        }
        public override void Print()
        {
            Console.Write(Ch);
        }
        public override void Close()
        {
            Console.WriteLine(">>");
        }
    }

    /// <summary>
    /// ConcreteClass役2
    /// </summary>
    public class StringDisplay : AbstaractDisplay
    {
        private string Str { get; set; } = "";
        private int Width { get; set; } = 0;
        public StringDisplay(string str)
        {
            this.Str = str;
            // .NET Coreは標準ではShift_JISに対応していない
            // パッケージマネージャで System.Text.Encoding.CodePages を追加し、
            // 以下の1文でエンコードプロバイダを登録する必要がある。
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            Width = sjisEnc.GetByteCount(Str);
        }

        // スーパークラスの抽象メソッドを実装する
        public override void Open()
        {
            PrintLine();
        }
        public override void Print()
        {
            Console.WriteLine($"|{Str}|");
        }
        public override void Close()
        {
            PrintLine();
        }
        private void PrintLine()
        {
            Console.Write("+");
            for(int i = 0; i < Width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AbstaractDisplay d1 = new CharDisplay('H');
            AbstaractDisplay d2 = new StringDisplay("Hello, World.");
            AbstaractDisplay d3 = new StringDisplay("こんにちは");

            d1.Display();
            //<<HHHHH>>

            d2.Display();
            //+-------------+
            //|Hello, World.|
            //|Hello, World.|
            //|Hello, World.|
            //|Hello, World.|
            //|Hello, World.|
            //+-------------+

            d3.Display();
            //+----------+
            //|こんにちは|
            //|こんにちは|
            //|こんにちは|
            //|こんにちは|
            //|こんにちは|
            //+----------+
        }
    }
}
