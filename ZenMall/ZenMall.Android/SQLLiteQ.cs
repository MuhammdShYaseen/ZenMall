using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace ZenMall.Droid
{
   public class SQLLiteQ
    {
        public string DbFileName = "ZainMall.db3";
        public string DBpath
        {
            get
            {
                string DBfolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(DBfolder, DbFileName);

            }
        }
        public ObservableCollection<NotificationM> NotiSource { get; set; }


        public void Insert(string Titel, string DisNoti, string imageNoti)
        {
            var db = new SQLiteConnection(DBpath);
            if (!File.Exists(DBpath))
            {
                db.CreateTable<BasketTB>();
                db.CreateTable<LoginTB>();
                db.CreateTable<NotificationM>();
            }

            var newNoti = new NotificationM();
            newNoti.Notititel = Titel;
            newNoti.NotiDiscraption = DisNoti;
            
            newNoti.NotiImage = imageNoti;
            
            db.Insert(newNoti);
        }
       
       
        public bool Select(string Notidis)
        {

            string data = "";
            var db = new SQLiteConnection(DBpath);

            var FinNoti = db.Query<NotificationM>("SELECT * FROM NotificationM WHERE NotiDiscraption = ?", Notidis);
            foreach (var s in FinNoti)
            {
                data += s.NotiDiscraption;
            }
            if (data != "")
            {

                return false;

            }

            else
            {
                return true;
            }

        }
        public SQLLiteQ()

        {
            if (!File.Exists(DBpath))
            {

                var db = new SQLiteConnection(DBpath);

                db.CreateTable<BasketTB>();
                db.CreateTable<LoginTB>();
                db.CreateTable<NotificationM>();
            }
            NotiSource = new ObservableCollection<NotificationM>();

            var db1 = new SQLiteConnection(DBpath);
            var table = db1.Table<NotificationM>();

            foreach (var s in table)
            {
                NotiSource.Add(new NotificationM
                {
                    NotiDiscraption = s.NotiDiscraption,
                    NotifiId = s.NotifiId,
                    NotiImage = s.NotiImage,
                    Notititel = s.Notititel

                });

            }



        }
       
    }
}