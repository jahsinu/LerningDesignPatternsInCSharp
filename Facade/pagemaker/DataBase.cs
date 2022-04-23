
using System;
using System.IO;
using System.Collections.Generic;
// JavaのPropertiesファイルを使用するため、NuGetパッケージ Authlete.Authleteを使用
using Authlete.Util;

namespace pagemaker
{
  public class DataBase
  {
    private DataBase() { }   // newでインスタンス生成させないためにprivate宣言
    public static IDictionary<string, string> getProperties(string dbname)
    {
      string filename = dbname + ".txt";
      IDictionary<string, string> prop = null;
      try
      {
        using (TextReader reader = new StreamReader(filename))
        {
          prop = PropertiesLoader.Load(reader);
        }
      }
      catch (IOException e)
      {
        Console.WriteLine("Warning: " + filename + " is not found.");
        Console.WriteLine(e);
      }
      return prop;
    }
  }
}