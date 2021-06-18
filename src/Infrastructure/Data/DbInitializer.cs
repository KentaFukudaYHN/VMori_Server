﻿using ApplicationCore.Entities;
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

            db.Accounts.Add(new Account()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "ant",
                Mail = "vmori.test2@gmail.com",
                Password = "test",
                Icon = "aaa.png",
                Gender = ApplicationCore.Enum.GenderKinds.Male,
                Birthday = "20000101",
                RegistDateTime = new DateTime(1994,4,15)
            }); ;

            //遅延実行
            db.SaveChanges();
        }
    }
}
