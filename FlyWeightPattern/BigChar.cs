using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyWeightPattern
{
    public class BigChar
    {
        /// <summary>
        /// 文字の名前
        /// </summary>
        private char charname;

        /// <summary>
        /// 大きな文字列を表現する文字列
        /// </summary>
        private string fontdata;

        public BigChar(char charname)
        {
            this.charname = charname;
            try
            {
                using (var reader = new StreamReader($"BigFont/big{charname}.txt"))
                {
                    string? line;
                    StringBuilder sb = new StringBuilder();
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.AppendLine(line + "\n");
                    }
                    this.fontdata = sb.ToString();
                }
            }
            catch (IOException)
            {
                this.fontdata = charname + "?";
            }
        }

        /// <summary>
        /// 大きな文字を表示する
        /// </summary>
        public void Print()
        {
            Console.WriteLine(this.fontdata);
        }
    }
}
