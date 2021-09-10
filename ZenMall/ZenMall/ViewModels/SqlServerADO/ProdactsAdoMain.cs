using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.Data;
using System.Data.SqlClient;
using ZenMall.Models.SqlServerModels;

namespace ZenMall.ViewModels.SqlServerADO
{
   public class ProdactsAdoMain
    {
       public static string SqlConnectString= Preferences.Get("Conn", null);
       public  void AddProdact(ProdactsM prodactsMss)
        {
            
            string sql = "INSERT INTO Prodacts (Pname,Pmarks,Psaction,Pprice,Pdiscount,Pdiscraption,Pimage) VALUES(@Pname, @Pmarks,@Psaction,@Pprice,@Pdiscount,@Pdiscraption,@Pimage)";

            using (SqlConnection con = new SqlConnection(SqlConnectString))
            {
                con.Open();

                using (SqlCommand comando = new SqlCommand(sql, con))
                {   
                    //comando.Parameters.Add("@ProdactID", SqlDbType.NVarChar).Value = prodactsMss.ProdactID;
                    comando.Parameters.Add("@Pname", SqlDbType.NVarChar).Value = prodactsMss.PName;
                    comando.Parameters.Add("@Pmarks", SqlDbType.NVarChar).Value = prodactsMss.PMarks;
                    comando.Parameters.Add("@Psaction", SqlDbType.NVarChar).Value = prodactsMss.PSaction;
                    comando.Parameters.Add("@Pprice", SqlDbType.Int).Value = prodactsMss.PPrice;
                    comando.Parameters.Add("@Pdiscount", SqlDbType.Int).Value = prodactsMss.PDiscount;
                    comando.Parameters.Add("@Pdiscraption", SqlDbType.NVarChar).Value = prodactsMss.PDiscraption;
                    comando.Parameters.Add("@Pimage", SqlDbType.NVarChar).Value = prodactsMss.PImage;
                    
                    comando.ExecuteNonQuery();
                }

                con.Close();
            }

            
            
        }
        public void UpdateProdact(int ProID,ProdactsM prodactsMss)
        {
            
                string sql = "UPDATE Prodacts SET Pname=@Pname,Pmarks=@Pmarks,Psaction=@Psaction,Pprice=@Pprice,Pdiscount=@Pdiscount,Pdiscraption=@Pdiscraption,Pimage=@Pimage WHERE ProdactId="+ProID.ToString();

                using (SqlConnection con = new SqlConnection(SqlConnectString))
                {
                    con.Open();

                    using (SqlCommand comando = new SqlCommand(sql, con))
                    {
                        comando.Parameters.Add("@Pname", SqlDbType.NVarChar).Value = prodactsMss.PName;
                        comando.Parameters.Add("@Pmarks", SqlDbType.NVarChar).Value = prodactsMss.PMarks;
                        comando.Parameters.Add("@Psaction", SqlDbType.NVarChar).Value = prodactsMss.PSaction;
                        comando.Parameters.Add("@Pprice", SqlDbType.Int).Value = prodactsMss.PPrice;
                        comando.Parameters.Add("@Pdiscount", SqlDbType.Int).Value = prodactsMss.PDiscount;
                        comando.Parameters.Add("@Pdiscraption", SqlDbType.NVarChar).Value = prodactsMss.PDiscraption;
                        comando.Parameters.Add("@Pimage", SqlDbType.NVarChar).Value = prodactsMss.PImage;

                        comando.ExecuteNonQuery();
                    }

                    con.Close();
                }

            
        }
        public void DeleteProdacts(int ProID)
        {
           
                string sql = "DELETE FROM Prodacts WHERE ProdactId=" + ProID.ToString();

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
