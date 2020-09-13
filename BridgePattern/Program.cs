using System;
using System.Text;

namespace BridgePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Display d1 = new Display(new StringDisplayImpl("Hello, Japan."));
            Display d2 = new CountDisplay(new StringDisplayImpl("Hello, World."));
            CountDisplay d3 = new CountDisplay(new StringDisplayImpl("Hello, Universe."));
            d1.Show();
            d2.Show();
            d3.Show();
            d3.MultiDisplay(5);
        }
    }

    /// <summary>
    /// Abstraction役
    /// </summary>
    /// <remarks>
    /// 「機能のクラス階層」の最上位。
    /// Implementor役のメソッドを使って機能を実現する。
    /// </remarks>
    public class Display
    {
        // 開く、表示する、閉じる
        // という「機能」を定義したクラス

        // implフィールドが 「機能のクラス階層」と「実装のクラス階層」を
        // 橋渡しする
        private DisplayImpl impl;
        public Display(DisplayImpl impl)
        {
            this.impl = impl;
        }
        public void Open()
        {
            impl.RawOpen();
        }
        public void Print()
        {
            impl.RawPrint();
        }
        public void Close()
        {
            impl.RawClose();
        }
        public void Show()
        {
            Open();
            Print();
            Close();
        }
    }

    /// <summary>
    /// RefinedAbstraction役
    /// </summary>
    /// <remarks>
    /// Abstraction役に機能追加したクラス
    /// </remarks>
    public class CountDisplay : Display
    {
        // Displayの「機能」の1つ、表示する を
        // 複数回実行できるよう改良したクラス

        public CountDisplay(DisplayImpl impl) : base(impl) { }
        public void MultiDisplay(int times)
        {
            Open();
            for(int i = 0; i < times; i++)
            {
                Print();
            }
            Close();
        }
    }



    /// <summary>
    /// Implementor役
    /// </summary>
    /// <remarks>
    /// 「実装のクラス階層」の最上位。
    /// Abstraction役が使用するAPIを定める。
    /// </remarks>
    public abstract class DisplayImpl
    {
        // 開く、表示する、閉じる
        // で使用するAPIを定義した抽象クラス

        public abstract void RawOpen();
        public abstract void RawPrint();
        public abstract void RawClose();
    }

    /// <summary>
    /// ConcreteImplementor役
    /// </summary>
    /// <remarks>
    /// Implementor役が定めたAPIを実装するクラス
    /// </remarks>
    public class StringDisplayImpl : DisplayImpl
    {
        // 開く、表示する、閉じる
        // の具体的実装を行うクラス

        private string str;
        private int width;
        public StringDisplayImpl(String str)
        {
            this.str = str;
            // .NET Coreは標準ではShift_JISに対応していない
            // パッケージマネージャで System.Text.Encoding.CodePages を追加し、
            // 以下の1文でエンコードプロバイダを登録する必要がある。
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            width = sjisEnc.GetByteCount(str);
        }
        public override void RawOpen()
        {
            PrintLine();
        }
        public override void RawPrint()
        {
            Console.WriteLine($"|{str}|");
        }
        public override void RawClose()
        {
            PrintLine();
        }
        private void PrintLine()
        {
            Console.Write("+");
            for(int i = 0; i < width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }
    }
}
