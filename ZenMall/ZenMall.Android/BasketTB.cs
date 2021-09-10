using System;
using System.Collections.Generic;
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
    [Table("BasketTB")]
    public class BasketTB
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string ProdactName { get; set; }
        public int Prise { get; set; }
        public int TotalPrice { get; set; }
        public string Pimage { get; set; }
        public int NumberOfProdacts { get; set; }
    }
}