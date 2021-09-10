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
using Newtonsoft.Json;
using SQLite;

namespace ZenMall.Droid
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