using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMall.ViewModels.SqlServerADO
{
    public class SqlADOMain
    {
        public List<string> LstSactionFoods = new List<string>();
        public List<string> LstSactionAss = new List<string>();
        public List<string> LstSactionCloths = new List<string>();
        public List<string> LstSactionCleans = new List<string>();
        public List<string> LstSactionHTools = new List<string>();
        public List<string> LstSactionBeauty = new List<string>();
        public List<string> LstSactionCar = new List<string>();
        public List<string> LstSactionGifts = new List<string>();
        public List<string> LstSactionProfiom = new List<string>();
        public List<string> LstSactionSmoke = new List<string>();
        public List<string> LstSactionStationery = new List<string>();
        public List<string> LstSactionAll = new List<string>();
        public List<string> LstMainSaction = new List<string>();
        public SqlADOMain()
        {
            AddItemToSactionList();
        }
        void AddItemToSactionList()
        {
            string[] SactionFoods = { "دجاج", "يومي", "لحوم", "خضار", "توابل", "البان", "مخبوزات", "معلبات", "حلويات", "مشروبات", "حبوب", "مفرزات", "نقرشات", "فوط" };
            string[] SactionClean = { "صابون", "معقمات", "سائل جلي", "مساحيق غسيل", "مساحيق الارضيات" };
            string[] SactionAss = { "كمبيوتر", "موبايل" };
            string[] SactionCloths = { "احذية رجالي", "احذية نسائي", "احذية ولادي", "البسة رجالي", "البسة نسائي", "البسة ولادي" };
            string[] SactionCar = { "سيارة" };
            string[] SactionBeauty = { "الشعر", "الجسم", "طلاء الاظافر", "مكياج", "اكسسوارات" };
            string[] SactionHtools = { "تحف", "المطبخ", "كهربائيات" };
            string[] SactionGifts = { "هدايا" };
            string[] SactionSmoke = { "سجائر", "اركيلة" };
            string[] SactionProfiom = { "عطر رجالي", "عطر نسائي" };
            string[] SactionStat = { "قرطاسية" };
            string[] SactionMain = { "السوبر ماركت", "المنظفات", "الهدايا", "تجميل", "ادوات منزلية", "الملابس", "عطورات", "المدخنين", "قرطاسية", "اكسسوارات", "السيارة" };
            LstSactionFoods.AddRange(SactionFoods);
            LstSactionAss.AddRange(SactionAss);
            LstSactionCloths.AddRange(SactionCloths);
            LstSactionCleans.AddRange(SactionClean);
            LstSactionHTools.AddRange(SactionHtools);
            LstSactionBeauty.AddRange(SactionBeauty);
            LstSactionGifts.AddRange(SactionGifts);
            LstSactionCar.AddRange(SactionCar);
            LstSactionProfiom.AddRange(SactionProfiom);
            LstSactionSmoke.AddRange(SactionSmoke);
            LstSactionStationery.AddRange(SactionStat);
            LstMainSaction.AddRange(SactionMain);
            LstSactionAll.AddRange(SactionStat);
            LstSactionAll.AddRange(SactionProfiom); 
            LstSactionAll.AddRange(SactionSmoke);
            LstSactionAll.AddRange(SactionGifts);
            LstSactionAll.AddRange(SactionAss);
            LstSactionAll.AddRange(SactionBeauty); 
            LstSactionAll.AddRange(SactionCar);
            LstSactionAll.AddRange(SactionCloths);
            LstSactionAll.AddRange(SactionFoods); 
            LstSactionAll.AddRange(SactionHtools);
            LstSactionAll.AddRange(SactionClean);
        }
       
    }
}
