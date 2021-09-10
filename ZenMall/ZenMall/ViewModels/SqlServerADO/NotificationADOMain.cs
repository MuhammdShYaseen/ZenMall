using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZenMall.Models.SQLite.Tables;
using Xamarin.Essentials;

namespace ZenMall.ViewModels.SqlServerADO
{
  public  class NotificationADOMain
    {
        public static string SqlConnectString = Preferences.Get("Conn", null);
        public void UpdateNoti( NotificationM NotifsMss)
        {
                string sql = "UPDATE Notifacation SET Notititel=@Notititel,NotiDiscraption=@NotiDiscraption,NotiImage=@NotiImage WHERE NotifiId=" + "1";

                using (SqlConnection con = new SqlConnection(SqlConnectString))
                {
                    con.Open();

                    using (SqlCommand comando = new SqlCommand(sql, con))
                    {
                        comando.Parameters.Add("@Notititel", SqlDbType.NVarChar).Value = NotifsMss.Notititel;
                        comando.Parameters.Add("@NotiDiscraption", SqlDbType.NVarChar).Value = NotifsMss.NotiDiscraption;
                        comando.Parameters.Add("@NotiImage", SqlDbType.NVarChar).Value = NotifsMss.NotiImage;
                        

                        comando.ExecuteNonQuery();
                    }

                    con.Close();
                }

           
        }
    }
}
