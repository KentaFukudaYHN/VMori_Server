using ApplicationCore.Entities;
using System;
using System.Linq;

namespace Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(VMoriContext db)
        {
            //データベースにテーブルがない場合は作成
            db.Database.EnsureCreated();

            //遅延実行
            db.SaveChanges();
        }
    }
}
