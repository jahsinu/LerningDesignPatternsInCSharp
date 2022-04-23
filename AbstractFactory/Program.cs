using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;

namespace AbstractFactory
{
    using Factory;

    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Usage: dotnet AbstractFactory.dll class.name.of.ConcreteFactory");
                Console.WriteLine("Example1: dotnet AbstractFacroty.dll ListFactory.ListFactory");
                Console.WriteLine("Example2: dotnet AbstractFacroty.dll TableFactory.TableFactory");
                Environment.Exit(-1);
            }

            // AbstractFactoryのAPIだけを使って仕事をする

            Factory factory = Factory.GetFactory(args[0]);

            Link asahi = factory.CreateLink("朝日新聞", "http://www.asahi.com/");
            Link yomiuri = factory.CreateLink("読売新聞", "http://www.yomiuri.co.jp/");

            Link us_yahoo = factory.CreateLink("Yahoo!", "http://www.yahoo.com/");
            Link jp_yahoo = factory.CreateLink("Yahoo!Japan", "http://www.yahoo.co.jp/");
            Link excite = factory.CreateLink("Excite", "http://www.excite.com");
            Link google = factory.CreateLink("Google", "http://www.google.com");

            Tray traynews = factory.CreateTray("新聞");
            traynews.Add(asahi);
            traynews.Add(yomiuri);

            Tray trayYahoo = factory.CreateTray("Yahoo!");
            trayYahoo.Add(us_yahoo);
            trayYahoo.Add(jp_yahoo);

            Tray traySearch = factory.CreateTray("サーチエンジン");
            traySearch.Add(trayYahoo);
            traySearch.Add(excite);
            traySearch.Add(google);

            Page page = factory.CreatePage("LinkPage", "結城 浩");
            page.Add(traynews);
            page.Add(traySearch);
            page.Outoput();
        }
    }
}

namespace Factory
{
    /// <summary>
    /// AbstractProduct役0
    /// </summary>
    /// <remarks>
    /// ページ内の項目を表し、LinkとTrayを同一視するためのもの
    /// </remarks>
    public abstract class Item
    {
        protected string Caption { get; set; } = "";
        public Item(string caption)
        {
            Caption = caption;
        }
        public abstract string MakeHTML();
    }

    /// <summary>
    /// AbstractProduct役1
    /// </summary>
    /// <remarks>
    /// 抽象的な「部品」
    /// </remarks>
    public abstract class Link : Item
    {
        protected string Url { get; set; } = "";
        public Link(string caption, string url) : base(caption)
        {
            Url = url;
        }
    }

    /// <summary>
    /// AbstractProduct役2
    /// </summary>
    /// <remarks>
    /// 抽象的な「部品」
    /// </remarks>
    public abstract class Tray : Item
    {
        protected List<Item> tray = new List<Item>();
        public Tray(string caption) : base(caption) { }
        public void Add(Item item)
        {
            tray.Add(item);
        }
    }

    /// <summary>
    /// AbstractProduct役3
    /// </summary>
    /// <remarks>
    /// 抽象的な「製品」
    /// </remarks>
    public abstract class Page
    {
        protected string Title { get; set; } = "";
        protected string Author { get; set; } = "";
        protected List<Item> content = new List<Item>();
        public Page(string title, string author)
        {
            Title = title;
            Author = author;
        }
        public void Add(Item item)
        {
            content.Add(item);
        }
        public void Outoput()
        {
            // .NET Coreは標準ではShift_JISに対応していない
            // パッケージマネージャで System.Text.Encoding.CodePages を追加し、
            // 以下の1文でエンコードプロバイダを登録する必要がある。
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encodiing = Encoding.GetEncoding("Shift_JIS");

            string filename = Title + ".html";
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, false, encodiing))
                {
                    writer.WriteLine(this.MakeHTML());
                    Console.WriteLine($"{filename}を作成しました。");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public abstract string MakeHTML();
    }

    /// <summary>
    /// AbstractFactory役
    /// </summary>
    /// <remarks>
    /// 抽象的な「工場」
    /// </remarks>
    public abstract class Factory
    {
        public static Factory GetFactory(string classname)
        {
            Factory factory = null;
            try
            {
                factory = (Factory)Type.GetType(classname).
                    InvokeMember(null
                               , BindingFlags.CreateInstance
                               , null
                               , null
                               , new object[] { }
                               );
            }
            catch (TargetInvocationException)
            {
                Console.WriteLine($"クラス {classname} が見つかりません。");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return factory;
        }
        public abstract Link CreateLink(string caption, string url);
        public abstract Tray CreateTray(string caption);
        public abstract Page CreatePage(string title, string author);
    }
}

namespace ListFactory
{
    using Factory;

    /// <summary>
    /// ConcreteProduct役1
    /// </summary>
    public class ListLink : Link
    {
        public ListLink(string caption, string url) : base(caption, url) { }
        public override string MakeHTML()
        {
            return $"  <li><a href=\"{Url}\">{Caption}</a></li>\n";
        }
    }

    /// <summary>
    /// ConcreteProduct役2
    /// </summary>
    public class ListTray : Tray
    {
        public ListTray(string caption) : base(caption) { }
        public override string MakeHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>\n");
            sb.Append($"{Caption}\n");
            sb.Append($"<ul>\n");
            IEnumerator<Item> it = tray.GetEnumerator();
            while(it.MoveNext())
            {
                sb.Append(it.Current.MakeHTML());
            }
            sb.Append($"</ul>\n");
            sb.Append("</li>\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// ConcreteProduct役3
    /// </summary>
    public class ListPage : Page
    {
        public ListPage(string title, string author) : base(title, author) { }
        public override string MakeHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<html><head><title>{Title}</title></head>\n");
            sb.Append("<body>\n");
            sb.Append($"<h1>{Title}</h1>\n");
            sb.Append("<ul>\n");
            IEnumerator<Item> it = content.GetEnumerator();
            while(it.MoveNext())
            {
                sb.Append(it.Current.MakeHTML());
            }
            sb.Append("</ul>\n");
            sb.Append($"<hr><address>{Author}</address>\n");
            sb.Append("</body></html>");
            return sb.ToString();
        }
    }

    /// <summary>
    /// ConcreteFactory役
    /// </summary>
    public class ListFactory : Factory
    {
        public override Link CreateLink(string caption, string url)
        {
            return new ListLink(caption, url);
        }
        public override Tray CreateTray(string caption)
        {
            return new ListTray(caption);
        }
        public override Page CreatePage(string title, string author)
        {
            return new ListPage(title, author);
        }
    }
}

namespace TableFactory
{
    using Factory;

    /// <summary>
    /// ConcreteProduct役1
    /// </summary>
    public class TableLink : Link
    {
        public TableLink(string caption, string url) : base(caption, url) { }
        public override string MakeHTML()
        {
            return $"  <td><a href=\"{Url}\">{Caption}</a></td>\n";
        }
    }

    /// <summary>
    /// ConcreteProduct役2
    /// </summary>
    public class TableTray : Tray
    {
        public TableTray(string caption) : base(caption) { }
        public override string MakeHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td>");
            sb.Append("<table width=\"100%\" border=\"1\"><tr>");
            sb.Append($"<td bgcolor=\"#cccccc\" align=\"center\" colspan=\"{tray.Count}\"><b>{Caption}</b></td>");
            sb.Append("</tr>\n");
            sb.Append("<tr>\n");
            IEnumerator<Item> it = tray.GetEnumerator();
            while(it.MoveNext())
            {
                sb.Append(it.Current.MakeHTML());
            }
            sb.Append("</tr></table>");
            sb.Append("</td>");
            return sb.ToString();
        }
    }

    /// <summary>
    /// ConcreteProduct役3
    /// </summary>
    public class TablePage : Page
    {
        public TablePage(string title, string author) : base(title, author) { }
        public override string MakeHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<html><head><title>{Title}</title></head>\n");
            sb.Append("<body>\n");
            sb.Append($"<h1>{Title}</h1>\n");
            sb.Append("<table width=\"80%\" border=\"3\">\n");
            IEnumerator<Item> it = content.GetEnumerator();
            while(it.MoveNext())
            {
                sb.Append($"<tr>{it.Current.MakeHTML()}</tr>");
            }
            sb.Append("</table>\n");
            sb.Append($"<hr><address>{Author}</address>");
            sb.Append("</body></html>\n");
            return sb.ToString();
        }
    }

    /// <summary>
    /// ConcreteFactory役
    /// </summary>
    public class TableFactory : Factory
    {
        public override Link CreateLink(string caption, string url)
        {
            return new TableLink(caption, url);
        }
        public override Tray CreateTray(string caption)
        {
            return new TableTray(caption);
        }
        public override Page CreatePage(string title, string author)
        {
            return new TablePage(title, author);
        }
    }
}
