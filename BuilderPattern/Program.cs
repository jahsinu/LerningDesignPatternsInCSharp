using System;
using System.IO;
using System.Text;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Usage();
                Environment.Exit(1);
            }
            if(args[0].Equals("plain"))
            {
                TextBuilder textBuilder = new TextBuilder();
                Director director = new Director(textBuilder);
                director.Construct();
                Console.WriteLine(textBuilder.GetResult());
            }
            else if(args[0].Equals("html"))
            {
                HtmlBuilder htmlBuilder = new HtmlBuilder();
                Director director = new Director(htmlBuilder);
                director.Construct();
                Console.WriteLine($"{htmlBuilder.GetResult()}が作成されました。");
            }
            else
            {
                Usage();
                Environment.Exit(1);
            }
        }
        private static void Usage()
        {
            Console.WriteLine("Usage: dotnet BuilderPattern.dll plain     プレーンテキストで文書作成");
            Console.WriteLine("Usage: dotnet BuilderPattern.dll html      HTMLファイルで文書作成");
        }
    }

    /// <summary>
    /// Builder役
    /// </summary>
    /// <remarks>
    /// インスタンスを作るためのメソッドを規定した抽象クラス
    /// </remarks>
    public abstract class Builder
    {
        // 文書のタイトルを作成する
        public abstract void MakeTitle(string title);
        // 文書の文字列を作成する
        public abstract void MakeString(string str);
        // 文書の箇条書きアイテムを作成する
        public abstract void MakeItems(string[] items);
        // 文書を完成させる
        public abstract void Close();
    }

    /// <summary>
    /// Director役
    /// </summary>
    /// <remarks>
    /// Builderクラスで規定されたメソッドだけを使用してインスタンスを作る
    /// </remarks>
    public class Director
    {
        private Builder builder;
        public Director(Builder builder)
        {
            // Builderクラスの派生クラスのインスタンスが与えられるので
            // builderフィールドに保持
            this.builder = builder;
        }
        public void Construct()
        {
            // Builderを使用したインスタンス作成処理を規定
            // TemplatePatternと似ているが、TemplatePatternは
            // スーパークラスが派生クラスをコントロールする。
            // BuilderPatternはDirectorがBuilderをコントロールする。

            builder.MakeTitle("Greeting");
            builder.MakeString("朝から昼にかけて");
            builder.MakeItems(new string[]
            {
                "おはようございます",
                "こんにちは"
            });
            builder.MakeString("夜に");
            builder.MakeItems(new string[]
            {
                "こんばんは",
                "おやすみなさい",
                "さようなら"
            });
            builder.Close();
        }
    }

    /// <summary>
    /// ConcreteBuilder役1
    /// </summary>
    /// <remarks>
    /// Builder派生クラス プレーンテキストで文書化する
    /// </remarks>
    public class TextBuilder : Builder
    {
        private StringBuilder sb = new StringBuilder();
        public override void MakeTitle(string title)
        {
            sb.Append("==============================\n");
            sb.Append($"「{title}」\n");
            sb.Append("\n");
        }

        public override void MakeString(string str)
        {
            sb.Append($"■{str}\n");
            sb.Append("\n");
        }

        public override void MakeItems(string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                sb.Append($"・{items[i]}\n");
            }
            sb.Append("\n");
        }

        public override void Close()
        {
            sb.Append("==============================\n");
        }

        public string GetResult()
        {
            return sb.ToString();
        }
    }

    /// <summary>
    /// ConcreteBuilder役2
    /// </summary>
    /// <remarks>
    /// Builder派生クラス HTML形式で文書化する
    /// </remarks>
    public class HtmlBuilder : Builder
    {
        private string FileName { get; set; } = "";
        private Encoding encodiing;
        public override void MakeTitle(string title)
        {
            FileName = title + ".html";
            // .NET Coreは標準ではShift_JISに対応していない
            // パッケージマネージャで System.Text.Encoding.CodePages を追加し、
            // 以下の1文でエンコードプロバイダを登録する必要がある。
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            encodiing = Encoding.GetEncoding("Shift_JIS");
            try
            {
                using (StreamWriter writer = new StreamWriter(FileName, false, encodiing))
                {
                    writer.WriteLine($"<html><head><title>{title}</title></head><body>");
                    writer.WriteLine($"<h1>{title}</h1>");
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public override void MakeString(string str)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FileName, true, encodiing))
                {
                    sw.WriteLine($"<p>{str}</p>");
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public override void MakeItems(string[] items)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FileName, true, encodiing))
                {
                    sw.WriteLine("<ul>");
                    for(int i = 0; i < items.Length; i++)
                    {
                        sw.WriteLine($"<li>{items[i]}</li>");
                    }
                    sw.WriteLine("</ul>");
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public override void Close()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FileName, true, encodiing))
                {
                    sw.WriteLine("</body></html>");
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public string GetResult() => FileName;
    }
}
