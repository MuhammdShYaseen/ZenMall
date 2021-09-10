using System;
using System.Collections.Generic;
using System.Text;
using ZenMall.Models.SqlServerModels;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;

namespace ZenMall.ViewModels.SqlServerViewModels
{
    public class SQLdata
    {
      
        
        public SQLdata()
        {

            


        }
       

        public async Task <IEnumerable<ProdactsM>> Datasourcee(string Saction)
        {

           
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Add("ApiKey", "ReadAllProdactsAndNotificationAndPostClientRec");
            var resultJson = await httpclient.GetStringAsync("https://zainmall.sy/api/prodacts");
            List<ProdactsM> resultforecast;
            resultforecast = JsonConvert.DeserializeObject<List<ProdactsM>>(resultJson);
            if (Saction=="" ||Saction== null)
            return   resultforecast;
            else
            {
                List<ProdactsM> AssList;
                AssList = resultforecast;
                var AssList1 = AssList.Where(c => c.PSaction.Contains(Saction));
                return AssList1;
            }    
           
            

        }

        public async Task<IEnumerable<ProdactsM>> AdvetismentSource(string Saction,string Discraption)
        {


            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Add("ApiKey", "ReadAllProdactsAndNotificationAndPostClientRec");
            var resultJson = await httpclient.GetStringAsync("https://zainmall.sy/api/prodacts");
            List<ProdactsM> resultforecast;
            resultforecast = JsonConvert.DeserializeObject<List<ProdactsM>>(resultJson);
            if (Saction == "" || Saction == null)
                return resultforecast;
            else
            {
                List<ProdactsM> AssList;
                AssList = resultforecast;
                var AssList1 = AssList.Where(c => c.PSaction.Contains(Saction)&& c.PDiscraption.Contains(Discraption));
                return AssList1;
            }



        }
        public async Task<ObservableCollection<ProdactsM>> DsSearchResults(string SearchWord)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Add("ApiKey", "ReadAllProdactsAndNotificationAndPostClientRec");
            var resultJson = await httpclient.GetStringAsync("https://zainmall.sy/api/prodacts");
            List<ProdactsM> resultforecast;
            resultforecast = JsonConvert.DeserializeObject<List<ProdactsM>>(resultJson);
            
                List<ProdactsM> AssList;
                AssList = resultforecast;
                var AssList1 = AssList.Where(c => c.PName.Contains(SearchWord)|| c.PMarks.Contains(SearchWord)||c.PDiscraption.Contains(SearchWord));
            ObservableCollection<ProdactsM> collection = new ObservableCollection<ProdactsM>(AssList1);
            return collection;
           
        }

       async public Task<int>ItemCount(string SearchWord)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Add("ApiKey", "ReadAllProdactsAndNotificationAndPostClientRec");
            var resultJson = await httpclient.GetStringAsync("https://zainmall.sy/api/prodacts");
            List<ProdactsM> resultforecast;
            resultforecast = JsonConvert.DeserializeObject<List<ProdactsM>>(resultJson);

            List<ProdactsM> AssList;
            AssList = resultforecast;
            var AssList1 = AssList.Where(c => c.PName.Contains(SearchWord) || c.PMarks.Contains(SearchWord) || c.PDiscraption.Contains(SearchWord));
            ObservableCollection<ProdactsM> collection = new ObservableCollection<ProdactsM>(AssList1);
            return collection.Count;
        }


    }
 }

