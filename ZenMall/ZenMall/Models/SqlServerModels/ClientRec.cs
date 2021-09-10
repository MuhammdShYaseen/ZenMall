using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMall.Models.SqlServerModels
{

    public class ClientRec
    {
       

        public int RecuestID { get; set; }
       
        public string FullName { get; set; }
       
        public string PhoneNumber { get; set; }
     
        public string Adress { get; set; }
        public string Prodacts { get; set; }
      
        public string Long { get; set; }
 

        public string litit { get; set; }
  
        public DateTime RecDateTime { get; set; }
    }
}
