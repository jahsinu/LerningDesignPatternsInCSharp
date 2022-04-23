using System;
using System.IO;
using System.Collections.Generic;

namespace pagemaker
{
  // Fcade役
  // シンプルなAPIだけを外部に提供し、その他大勢の役をコントロールする
  public class PageMaker
  {
    private PageMaker() {}    // インスタンスは作らないのでprivate宣言する
    public static void makeWelcomePage(string mailaddr, string filename)
    {
      try
      {
        IDictionary<string, string> mailprop = DataBase.getProperties("maildata");
        string username = mailprop[mailaddr];
        HtmlWriter writer = new HtmlWriter(new StreamWriter(filename));
        writer.title($"Welcome to {username}'s page!");
        writer.paragraph($"{username}のページへようこそ。");
        writer.paragraph("メール待っていますね。");
        writer.mailto(mailaddr, username);
        writer.close();
        Console.WriteLine($"{filename} is created for {mailaddr}({username})");
      }
      catch (IOException e)
      {
        Console.WriteLine(e.StackTrace);
      }
    }
  }
}