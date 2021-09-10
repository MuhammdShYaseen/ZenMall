using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMall.Models.SQLite.Tables
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
