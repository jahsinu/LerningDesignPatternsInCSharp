using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlyWeightPattern
{
    public class BigCharFactory
    {
        /// <summary>
        /// 作成済みBigCahrインスタンス管理用
        /// </summary>
        private Dictionary<char, BigChar> pool = new Dictionary<char, BigChar>();

        /// <summary>
        /// Singletonパターン
        /// </summary>
        private static BigCharFactory instance = new BigCharFactory();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private BigCharFactory() { }

        /// <summary>
        /// 唯一のインスタンスを得る
        /// </summary>
        /// <returns>BigCharFactoryインスタンス</returns>
        public static BigCharFactory GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// BigCharインスタンスの生成(共有)
        /// </summary>
        /// <param name="charname"></param>
        /// <returns>BigCharインスタンス</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BigChar GetBigChar(char charname)
        {
            BigChar bc;
            if(pool.ContainsKey(charname))
            {
                bc = pool[charname];
            }
            else
            {
                bc = new BigChar(charname);
                pool[charname] = bc;
            }
            return bc;
        }
    }
}
