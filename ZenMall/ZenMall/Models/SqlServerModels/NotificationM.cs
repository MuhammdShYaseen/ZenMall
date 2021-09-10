using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMall.Models.SqlServerModels
{
   public class NotificationM
    {
        [AutoIncrement, PrimaryKey]
        public long NotifiId { get; set; }


        public string Notititel { get; set; }

        public string NotiDiscraption { get; set; }


        public string NotiImage { get; set; }
    }
}
