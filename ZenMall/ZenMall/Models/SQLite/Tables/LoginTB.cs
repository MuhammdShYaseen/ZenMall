using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMall.Models.SQLite.Tables
{
    [Table("LoginTB")]
    public class LoginTB
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string UserName { get; set; }
        public int PassWord { get; set; }
        
    }
}
