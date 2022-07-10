using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyWeightPattern
{
    public class BigString
    {
        /// <summary>
        /// 大きな文字の配列
        /// </summary>
        private BigChar[] bigchars;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="str"></param>
        public BigString(string str)
        {
            bigchars = new BigChar[str.Length];
            BigCharFactory factory = BigCharFactory.GetInstance();
            for (int i = 0; i < bigchars.Length; i++)
            {
                bigchars[i] = factory.GetBigChar(str[i]);
            }
        }

        /// <summary>
        /// 表示
        /// </summary>
        public void Print()
        {
            for(int i = 0; i < bigchars.Length; i++)
            {
                bigchars[i].Print();
            }
        }
    }
}
