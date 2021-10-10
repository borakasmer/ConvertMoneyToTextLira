using System;

public static class ExpressionExtensions
{
    static string[] ones = { "", "BİR", "İKİ", "ÜÇ", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
    static string[] tens = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
    static string[] thousands = { "KATİRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

    public static (string, string) FormatString(this string numberText, int groupCount)
    {
        decimal decCost = Convert.ToDecimal(numberText);
        string strCost = decCost.ToString("F2").Replace('.', ',');    // Replace('.',',') ondalık ayraç ayracı           
        string lira = strCost.Substring(0, strCost.IndexOf(',')); //tutarın lira kısmı
        return (lira.PadLeft(groupCount * 3, '0'), strCost.Substring(strCost.IndexOf(',') + 1, 2)); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.
    }

    public static string HunderedInterpret(this string numberText, Context context)
    {
        if (numberText.Substring(context.Index, 1) != "0")
        {
            context.Formula += ones[Convert.ToInt32(numberText.Substring(context.Index, 1))] + "YÜZ"; //yüzler
            if (context.Formula == "BİRYÜZ") //biryüz düzeltiliyor.
                context.Formula = "YÜZ";
        }
        return numberText;
    }

    public static string TenInterpret(this string numberText, Context context)
    {
        context.Formula += tens[Convert.ToInt32(numberText.Substring(context.Index + 1, 1))];
        return numberText;
    }

    public static string OneInterpret(this string numberText, Context context)
    {
        context.Formula += ones[Convert.ToInt32(numberText.Substring(context.Index + 2, 1))]; //birler
        return numberText;
    }

    public static string ThousendInterpret(this string numberText, Context context)
    {
        if (context.Formula != "")
        {
            context.Formula += "" + thousands[context.Index / 3];
            if (context.Formula == "BİRBİN") //birbin düzeltiliyor.
                context.Formula = "BİN";
        }
        return numberText;
    }

    public static string PennyInterpret(this string numberText, string penny)
    {
        if (numberText != "")
            numberText += " LİRA ";
        int yaziUzunlugu = numberText.Length;
        if (penny.Substring(0, 1) != "0") //kuruş onlar
            numberText += tens[Convert.ToInt32(penny.Substring(0, 1))];
        if (penny.Substring(1, 1) != "0") //kuruş birler
            numberText += ones[Convert.ToInt32(penny.Substring(1, 1))];
        if (numberText.Length > yaziUzunlugu)
            numberText += " KURUŞ";
        else
            numberText += "";
        return numberText;
    }
}