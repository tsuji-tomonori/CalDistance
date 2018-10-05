using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalDistance
{
    class Program
    {
        /// <summary>
        /// Main
        /// bmp2vec にて作成したベクトルの距離(2点間の距離まで)を求める 
        /// </summary>
        /// <param name="args">ファイルの絶対パス</param>
        static void Main(string[] args)
        {
            double[] a = null;
            double[] b = null;

            if (args.Length > 0)
            {
                a = readFile(args[0]);
                if(args.Length >= 2)
                    b = readFile(args[1]);
            }

            if(args.Length == 1 & a!=null)
                Console.WriteLine("距離 : " + calculationDistance(a));

            if (a != null & b != null)
            {
                if (a.Length == b.Length)
                {
                    Console.WriteLine("距離 : " + calculationDistance(a, b));
                }
                    
            }
           
        }

        /// <summary>
        /// bmp2vec にて作成したファイル(タブ区切り)を読み込む
        /// 対応するデータはn行1列
        /// これをdouble[]型に格納して返す
        /// ファイルが読み込めないなどエラーがあった場合, その旨を表示する.
        /// 何かしらの理由でファイルを読み込めなかった or doubleに変換できなかった場合, null を返す
        /// </summary>
        /// <param name="filePath">ファイルパス(絶対パス, 拡張子も必要) 特にエラーチェック等はしない</param>
        /// <returns>読み込んだデータをdouble型１次元配列に格納したもの errorが発生した場合nullを返す</returns>
        private static double[] readFile(string filePath)
        {
            /* 宣言 */
            StreamReader sr = null;
            double[] distance = null;

            try
            {
                sr = new StreamReader(filePath, Encoding.GetEncoding("Shift_JIS")); //ファイルの読み込み
                while (sr.EndOfStream == false) // 今回は1週だけ
                {
                    /* 宣言 */
                    string line = sr.ReadLine();
                    string[] fields = line.Split('\t'); //TSVファイルの場合
                    distance = new double[fields.Length];

                    for (int i = 0; i < fields.Length; i++)
                    {
                        double.TryParse(fields[i], out distance[i]);
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("ファイルが見つかりません\n" + filePath );
            }
            finally
            {
                if(sr != null) sr.Close();
            }

            return distance;
        }

        /// <summary>
        /// 距離を求めるメソッド(1つのベクトルだけ)
        /// 引数のチェックはしていない
        /// </summary>
        /// <param name="a">double[]で作られたベクトル</param>
        /// <returns>距離</returns>
        private static double calculationDistance(double[] a)
        {
            double distance = 0;
            for (int i = 0; i < a.Length; i++)
            {
                distance += a[i] * a[i];
            }
            return Math.Sqrt(distance);
        }

        /// <summary>
        /// 距離を求めるメソッド(２つのベクトル)
        /// 引数のチェックはしていない
        /// sqrt(sum(ai-bi)^2)にて計算
        /// aとbの配列の大きさが等しいこと
        /// </summary>
        /// <param name="a">double[]で作られたベクトル</param>
        /// <param name="b">double[]で作られたベクトル</param>
        /// <returns></returns>
        private static double calculationDistance(double[] a, double[] b)
        {
            double distance = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double buff = a[i] - b[i];
                distance += buff * buff;
            }
            return Math.Sqrt(distance);
        }
    }
}
