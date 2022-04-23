using System.IO;

namespace pagemaker
{
  public class HtmlWriter
  {
    private StreamWriter writer;
    public HtmlWriter(StreamWriter writer)    // コンストラクタ
    {
      this.writer = writer;
    }
    public void title(string title)   // タイトルの出力
    {
      writer.WriteLine("<html>");
      writer.WriteLine("  <head>");
      writer.WriteLine($"    <title>{title}</title>");
      writer.WriteLine("  </head>");
      writer.WriteLine("  <body>");
      writer.WriteLine($"    <h1>{title}</h1>");
    }
    public void paragraph(string msg)   // 段落の出力
    {
      writer.WriteLine("    <p>" + msg + "</p>");
    }
    public void link(string href, string caption)   // リンクの出力
    {
      paragraph($"<a href=\"{href}\">{caption}</a>");
    }
    public void mailto(string mailaddr, string username)    // メールアドレスの出力
    {
      link($"mailto:{mailaddr}", username);
    }
    public void close()   // 閉じる
    {
      writer.WriteLine("  </body>");
      writer.WriteLine("</html>");
      writer.Close();
    }
  }
}