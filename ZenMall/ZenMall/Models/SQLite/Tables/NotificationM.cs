using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMall.Models.SQLite.Tables
{
    public class NotificationM
    {
        

        [AutoIncrement, PrimaryKey]
        public int NotifiId { get; set; }

      

        public string Notititel { get; set; }

        
        public string NotiDiscraption { get; set; }

       
        public string NotiImage { get; set; }
    }
}
