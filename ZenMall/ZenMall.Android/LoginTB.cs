﻿using System;
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
    [Table("LoginTB")]
    public class LoginTB
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string UserName { get; set; }
        public int PassWord { get; set; }

    }
}