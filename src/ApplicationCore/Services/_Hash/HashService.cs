using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ApplicationCore.Services
{
    /// <summary>
    /// ハッシュService
    /// </summary>
    public class HashService : IHashService
    {
        /// <summary>
        /// ハッシュ化
        /// </summary>
        /// <param name="val"></param>
        /// <returns>ソルト付きのハッシュ値</returns>
        public string Hashing(string val)
        {
            var hash = _Hashing(val, null);
            return (BitConverter.ToString(hash.Item1) + "/" + BitConverter.ToString(hash.Item2)).Replace("-", string.Empty);

        }

        /// <summary>
        /// ハッシュ化
        /// </summary>
        /// <param name="val"></param>
        /// <returns>ソルト付きのハッシュ値</returns>
        public string Hashing(string val, byte[] salt)
        {
            var hash = _Hashing(val, salt);

            return (BitConverter.ToString(hash.Item1) + "/" + BitConverter.ToString(salt)).Replace("-", string.Empty);
        }

        /// <summary>
        ///　ハッシュ値を生成
        /// </summary>
        /// <param name="val"></param>
        /// <param name="salt"></param>
        /// <returns>  </returns>
        private (byte[], byte[]) _Hashing(string val, byte[]? salt)
        {
            if (salt == null)
            {
                //16bitのソルトを生成
                salt = new byte[128 / 8];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
            }

            //詳しくはリンク参照
            //https://docs.microsoft.com/ja-jp/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.0
            return
            (
                KeyDerivation.Pbkdf2(
                    val,
                    salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000, //反復回数
                    numBytesRequested: 256 / 8 //ハッシュの長さは32bitに設定
                ),
                salt
            );
        }

        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Verify(string hash, string target)
        {
            //ハッシュ値/ソルトの形になってるので、/で分解
            var hashSalt = hash.Split("/");
            var hashVal = hashSalt[0];
            var salt = hashSalt[1];

            // 文字列の文字数(半角)が奇数の場合、頭に「0」を付ける
            int length = salt.Length;
            if (length % 2 == 1)
            {
                salt = "0" + salt;
                length++;
            }

            //ソルト16進数をbyte配列に変換
            var byteList = new List<byte>();
            for(int i = 0; i < salt.Length - 1; i+=2)
            {
                byteList.Add(Convert.ToByte(salt.Substring(i , 2), 16));
            }

            //ターゲットの値をハッシュ化して確認
            var hashTarget = this.Hashing(target, byteList.ToArray());

            return hashVal == hashTarget.Split('/')[0];
        }
    }
}
