using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using ZenMall.Models.SQLite.ConnectString;
using ZenMall.Models.SQLite.Tables;

namespace ZenMall.ViewModels.SQLite
{
    public class SQLQuery
    {
       
        
        public string DbFileName = "ZainMall.db3";
        public string DBpath
        {
            get
            {
                string DBfolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(DBfolder, DbFileName);

            }
        }
        public ObservableCollection<BasketTB> BasketSource { get; set; }
        public ObservableCollection<NotificationM> NotiSource { get; set; }

        public void Insert(string prodactname,  int price,int totalprice,string pimage ,int numberofbrodacts)
        {
            var db = new SQLiteConnection(DBpath);
            if (!File.Exists(DBpath))
            {
                db.CreateTable<BasketTB>();
                db.CreateTable<LoginTB>();
                db.CreateTable <NotificationM>();
            }
          
            var newBasket = new BasketTB();
            newBasket.ProdactName = prodactname;
            newBasket.Prise = price;
            newBasket.TotalPrice = totalprice;
            newBasket.Pimage = pimage;
            newBasket.NumberOfProdacts = numberofbrodacts;
            db.Insert(newBasket);
        }
        public void Delete(int id)
        {
            var db = new SQLiteConnection(DBpath);
            var newBasketTB = new BasketTB
            {
                ID = id
            };
            db.Delete(newBasketTB);
            // db.Execute(""); 
        }
        public string SelectAll()
        {

            string data = "";
            var db = new SQLiteConnection(DBpath);
            var table = db.Table<BasketTB>();
            foreach (var s in table)
            {
                data += s.ID + " " + s.Prise.ToString() + "  " + s.ProdactName +  "  " + s.NumberOfProdacts.ToString() + "\n";
            }
            return data;
        }
        public bool Select(string Pname)
        {
            
            string data = "";
            var db = new SQLiteConnection(DBpath);

            var FinProdact = db.Query<BasketTB>("SELECT * FROM BasketTB WHERE ProdactName = ?", Pname);
            foreach (var s in FinProdact)
            {
                data += s.ProdactName;
            }
            if (data != "")
            {
                
               return false;
                
            }
             
            else
            {
                return true ;
            }
            
        }
        public void DeleteAllRows()
        {
            var db = new SQLiteConnection(DBpath);

            db.Query<NotificationM>("DELETE FROM NotificationM");
        }
        public void DeleteAllRowsBasket()
        {
            var db = new SQLiteConnection(DBpath);

            db.Query<BasketTB>("DELETE FROM BasketTB");
        }

        public SQLQuery()

        {
           if (!File.Exists(DBpath))
            {

                var db = new SQLiteConnection(DBpath);
                db.CreateTable<NotificationM>();
                db.CreateTable<BasketTB>();
                db.CreateTable<LoginTB>();
            }
            BasketSource = new ObservableCollection<BasketTB>();
            
            var db1 = new SQLiteConnection(DBpath);
            var table = db1.Table<BasketTB>();

            foreach (var s in table)
            {
                BasketSource.Add(new BasketTB
                {
                    ProdactName = s.ProdactName,
                    ID = s.ID,
                    Pimage=s.Pimage,
                    NumberOfProdacts = s.NumberOfProdacts,
                    Prise = s.Prise,
                    TotalPrice=s.TotalPrice
                    
                });

            }
            NotiSource = new ObservableCollection<NotificationM>();

            var db2 = new SQLiteConnection(DBpath);
            var table2 = db2.Table<NotificationM>();

            foreach (var s in table2)
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
        public void Update(string prodactname, int price, int totalprice, string pimage, int numberofbrodacts, int id)
        {
            var db = new SQLiteConnection(DBpath);
            var newBasket = new BasketTB();
            newBasket.ProdactName = prodactname;
            newBasket.Prise = price;
            newBasket.TotalPrice = totalprice;
            newBasket.Pimage = pimage;
            newBasket.NumberOfProdacts = numberofbrodacts;
            newBasket.ID = id;
            db.Update(newBasket);

        }
    }
}
