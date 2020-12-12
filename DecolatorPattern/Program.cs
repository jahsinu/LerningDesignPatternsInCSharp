using System;
using System.Text;

namespace DecolatorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Display b1 = new StringDisplay("Hello, world.");
            Display b2 = new SideBorder(b1, '#');
            Display b3 = new FullBorder(b2);
            b1.Show();
            b2.Show();
            b3.Show();

            Display b4 =
                new SideBorder(
                    new FullBorder(
                        new FullBorder(
                            new SideBorder(
                                new FullBorder(
                                    new StringDisplay("こんにちは")
                                ),
                                '*'
                            )
                        )
                    ),
                    '/'
                );
            b4.Show();
        }
    }

    /// <summary>
    /// Component役
    /// </summary>
    /// <remarks>
    /// 被装飾者のインターフェースを規定する抽象クラス
    /// </remarks>
    public abstract class Display
    {
        public abstract int GetColumns();
        public abstract int GetRows();
        public abstract string GetRowText(int row);
        public void Show()
        {
            for (int i = 0; i < GetRows(); i++)
            {
                Console.WriteLine(GetRowText(i));
            }
        }
    }

    /// <summary>
    /// ConcreteComponent役
    /// </summary>
    /// <remarks>
    /// Componentのインターフェースを実装した、具体的被装飾者
    /// </remarks>
    public class StringDisplay : Display
    {
        private string Str { get; set; }
        public StringDisplay(string str)
        {
            this.Str = str;
        }

        public override int GetColumns()
        {
            // .NET Coreは標準ではShift_JISに対応していない
            // パッケージマネージャで System.Text.Encoding.CodePages を追加し、
            // 以下の1文でエンコードプロバイダを登録する必要がある。
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            return sjisEnc.GetByteCount(Str);
        }

        public override int GetRows()
        {
            return 1;
        }

        public override string GetRowText(int row)
        {
            if (row == 0)
            {
                return Str;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Decolator役
    /// </summary>
    /// <remarks>
    /// 装飾を行う対象(被装飾者)を持っている、装飾者の抽象クラス
    /// 装飾者だが被装飾者と同じインターフェースを持つので
    /// 自身も被装飾者になれる
    /// </remarks>
    public abstract class Border : Display
    {
        protected Display display;
        protected Border(Display display)
        {
            this.display = display;
        }
    }

    /// <summary>
    /// ConcreteDecolator役1
    /// </summary>
    /// <remarks>
    /// 具体的Decolator
    /// </remarks>
    public class SideBorder : Border
    {
        private char BorderChar { get; set; }
        public SideBorder(Display display, char chr) : base(display)
        {
            this.BorderChar = chr;
        }

        public override int GetColumns()
        {
            return 1 + display.GetColumns() + 1;
        }

        public override int GetRows()
        {
            return display.GetRows();
        }

        public override string GetRowText(int row)
        {
            return BorderChar + display.GetRowText(row) + BorderChar;
        }
    }

    /// <summary>
    /// ConcreteDecolator役2
    /// </summary>
    /// <remarks>
    /// 具体的Decolator
    /// </remarks>
    public class FullBorder : Border
    {
        public FullBorder(Display display) : base(display) { }

        public override int GetColumns()
        {
            return 1 + display.GetColumns() + 1;
        }

        public override int GetRows()
        {
            return 1 + display.GetRows() + 1;
        }

        public override string GetRowText(int row)
        {
            if (row == 0 || row == display.GetRows() + 1)
            {
                return $"+{MakeLine('-', display.GetColumns())}+";
            }
            else
            {
                return $"|{display.GetRowText(row - 1)}|";
            }
        }

        private string MakeLine(char chr, int count)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < count; i++)
            {
                sb.Append(chr);
            }
            return sb.ToString();
        }
    }
}
