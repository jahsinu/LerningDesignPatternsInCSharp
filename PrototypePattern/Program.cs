using System;
using System.Collections;

namespace PrototypePattern
{
    using Framework;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            // 準備
            Manager manager = new Manager();
            UnderlinePen upen = new UnderlinePen('~');
            MessageBox mbox = new MessageBox('*');
            MessageBox sbox = new MessageBox('/');
            manager.Register("strong message", upen);
            manager.Register("warning box", mbox);
            manager.Register("slash box", sbox);

            // 生成
            IProduct p1 = manager.Create("strong message");
            p1.Use("Hello World.");
            // "Hello World."
            //  ~~~~~~~~~~~~

            IProduct p2 = manager.Create("warning box");
            p2.Use("Hello World.");
            // ****************
            // * Hello World. *
            // ****************

            IProduct p3 = manager.Create("slash box");
            p3.Use("Hello World.");
            // ////////////////
            // / Hello World. /
            // ////////////////
        }
    }

    /// <summary>
    /// TemplatePatternのAbstractClass
    /// </summary>
    /// <remarks>
    /// TemplatePatternを利用して、Productが具備する共通メソッドを実装
    /// </remarks>
    public abstract class ProductTemplate : IProduct
    {
        public abstract void Use(string s);
        public IProduct CreateClone()
        {
            return (IProduct)this.Clone();
        }
        public object Clone()
        {
            // シャローコピー
            // メンバーにオブジェクトを含む場合、ディープコピーメソッドの
            // 定義が必要
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// ConcretePrototype役1
    /// </summary>
    /// <remarks>
    /// インスタンスをコピーして新しいインスタンスを作るメソッドを実装する
    /// </remarks>
    public class MessageBox : ProductTemplate
    {
        private char DecoChar { get; set; }
        public MessageBox(char decochar)
        {
            DecoChar = decochar;
        }
        public override void Use(string s)
        {
            // .NET Coreは標準ではShift_JISに対応していない
            // パッケージマネージャで System.Text.Encoding.CodePages を追加し、
            // 以下の1文でエンコードプロバイダを登録する必要がある。
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int length = sjisEnc.GetByteCount(s);

            // decocharで指定された文字で文字列sを囲って表示
            for(int i = 0; i < length + 4; i++)
            {
                Console.Write(DecoChar);
            }
            Console.WriteLine("");
            Console.WriteLine($"{DecoChar} {s} {DecoChar}");
            for(int i = 0; i < length + 4; i++)
            {
                Console.Write(DecoChar);
            }
            Console.WriteLine("");
        }
    }

    /// <summary>
    /// ConcretePrototype役2
    /// </summary>
    /// <remarks>
    /// インスタンスをコピーして新しいインスタンスを作るメソッドを実装する
    /// </remarks>
    public class UnderlinePen : ProductTemplate
    {
        private char UlChar { get; set; }
        public UnderlinePen(char ulchar)
        {
            UlChar = ulchar;
        }
        public override void Use(string s)
        {
            // .NET Coreは標準ではShift_JISに対応していない
            // パッケージマネージャで System.Text.Encoding.CodePages を追加し、
            // 以下の1文でエンコードプロバイダを登録する必要がある。
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int length = sjisEnc.GetByteCount(s);

            // ulcharで指定された文字で下線を引いて表示
            Console.WriteLine($"\"{s}\"");
            Console.Write(" ");
            for(int i = 0; i < length; i++)
            {
                Console.Write(UlChar);
            }
            Console.WriteLine(" ");
        }
    }
}

namespace Framework
{
    // Frameworkのクラスには具象クラスを登場させない。
    // インタフェースや抽象クラスを具象クラスとの架け橋にすることで
    // 密結合を避ける。

    /// <summary>
    /// Prototype役
    /// </summary>
    /// <remarks>
    /// インスタンスをコピーして新しいインスタンスを作るメソッドを定める
    /// </remarks>
    public interface IProduct : ICloneable
    {
        // 「使う」ためのメソッド
        // 「使う」がどんな処理かは実装を行う派生クラスが決める
        void Use(string s);

        // Cloneメソッドを使ってインスタンス複製を行うメソッド
        // ICloneableから継承したCloneメソッドは派生クラスに実装を任せる
        IProduct CreateClone();
    }

    /// <summary>
    /// Client役
    /// </summary>
    /// <remarks>
    /// インスタンスをコピーするメソッドを利用して新しいインスタンスを作る
    /// </remarks>
    public class Manager
    {
        private Hashtable Showcase = new Hashtable();

        // 登録メソッド
        // protoの実際のクラスは関知せず、IProductを実装した物とする
        public void Register(string name, IProduct proto)
        {
            Showcase.Add(name, proto);
        }

        // インスタンス作成メソッド
        public IProduct Create(string protoname)
        {
            IProduct p = (IProduct)Showcase[protoname];
            return p.CreateClone();
        }
    }
}
