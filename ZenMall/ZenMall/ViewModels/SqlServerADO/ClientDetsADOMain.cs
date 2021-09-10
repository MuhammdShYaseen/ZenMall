using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Xamarin.Essentials;
using ZenMall.Models.SqlServerModels;

namespace ZenMall.ViewModels.SqlServerADO
{
   public class ClientDetsADOMain
    {
        public static string SqlConnectString = Preferences.Get("Conn", null);
        public List<ClientRec> ListCRec()
        {
            
            using (SqlConnection con = new SqlConnection(SqlConnectString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT* FROM ClientRecuest", con))
                {
                    List<ClientRec> ClientRss = new List<ClientRec>();
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ClientRss.Add(new ClientRec
                            {
                                RecuestID = Convert.ToInt32(sdr["RecuestId"]),
                                FullName = sdr["FullName"].ToString(),
                                PhoneNumber = sdr["PhoneNumber"].ToString(),
                                Adress = sdr["Adress"].ToString(),
                                Prodacts = sdr["Prodacts"].ToString(),
                                Long = sdr["Long"].ToString(),
                                litit = sdr["litit"].ToString(),
                                RecDateTime = (DateTime)sdr["RecDateTime"],
                            });
                        }
                    }
                    con.Close();
                    return ClientRss;
                }
            }
        }
        public void DeleteClientRec(int RecuestID)
        {

            string sql = "DELETE FROM ClientRecuest WHERE RecuestID=" + RecuestID.ToString();

            using (SqlConnection con = new SqlConnection(SqlConnectString))
            {
                con.Open();

                using (SqlCommand comando = new SqlCommand(sql, con))
                {
                    comando.ExecuteNonQuery();
                }

                con.Close();
            }


        }

    }
}
