using System;

namespace ConvertMoneyToTextLira
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ürünün Fiyatı Sadece :) " + "1250348.82".ConvertMoneyToTextLira());
        }
    }

    public static class Extensions
    {
        /*public static string ConvertMoneyToTextLira(this String money)
        {
            decimal decCost = Convert.ToDecimal(money);
            string strCost = decCost.ToString("F2").Replace('.', ',');    // Replace('.',',') ondalık ayraç ayracı           
            string lira = strCost.Substring(0, strCost.IndexOf(',')); //tutarın lira kısmı
            string kurus = strCost.Substring(strCost.IndexOf(',') + 1, 2);
            string text = "";
            string[] ones = { "", "BİR", "İKİ", "ÜÇ", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
            string[] tens = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
            string[] thousands = { "KATİRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.
            int groupCount = 6;
            lira = lira.PadLeft(groupCount * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.
            string groupValue;
            for (int i = 0; i < groupCount * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                groupValue = "";
                if (lira.Substring(i, 1) != "0")
                    groupValue += ones[Convert.ToInt32(lira.Substring(i, 1))] + "YÜZ"; //yüzler
                if (groupValue == "BİRYÜZ") //biryüz düzeltiliyor.
                    groupValue = "YÜZ";
                groupValue += tens[Convert.ToInt32(lira.Substring(i + 1, 1))]; //onlar
                groupValue += ones[Convert.ToInt32(lira.Substring(i + 2, 1))]; //birler
                if (groupValue != "") //binler
                    groupValue += "" + thousands[i / 3];
                if (groupValue == "BİRBİN") //birbin düzeltiliyor.
                    groupValue = "BİN";
                text += groupValue;
            }
            if (text != "")
                text += " LİRA ";
            int yaziUzunlugu = text.Length;
            if (kurus.Substring(0, 1) != "0") //kuruş onlar
                text += tens[Convert.ToInt32(kurus.Substring(0, 1))];
            if (kurus.Substring(1, 1) != "0") //kuruş birler
                text += ones[Convert.ToInt32(kurus.Substring(1, 1))];
            if (text.Length > yaziUzunlugu)
                text += " KURUŞ";
            else
                text += "";
            return text;
        }*/
        public static string ConvertMoneyToTextLira(this String money)
        {
            string text = "";
            int groupCount = 6;
            string groupValue;
            (string lira, string penny) = money.FormatString(groupCount);

            for (int i = 0; i < groupCount * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                groupValue = "";
                Context _context = new Context(groupValue, i);
                lira.HunderedInterpret(_context).TenInterpret(_context).OneInterpret(_context).ThousendInterpret(_context);
                text += _context.Formula;
            }
            return text.PennyInterpret(penny);
        }
    }
}
