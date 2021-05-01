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

            //既にデータがあれば作成しない
            if (db.VideoInfos.Any())
                return;

            db.VideoInfos.AddRange(
                new VideoInfo() { ID = Guid.NewGuid().ToString(), Title = "キズナアイ" },
                new VideoInfo() { ID = Guid.NewGuid().ToString(), Title = "新人Vtuber ヒトリコ"}
            );;

            //遅延実行
            db.SaveChanges();
        }
    }
}
