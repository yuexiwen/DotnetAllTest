using System.Globalization;
using System.Text;

double value = 12345.67891;
var cultureLst = CultureInfo.GetCultures(CultureTypes.AllCultures);

Dictionary<string, List<string>> ValueCultureDict = new Dictionary<string, List<string>>();

foreach (var culture in cultureLst)
{
    string key = value.ToString(culture);
    if (!ValueCultureDict.ContainsKey(key))
    {
        ValueCultureDict[key] = new List<string>();
    }

    ValueCultureDict[key].Add(culture.Name);
}


foreach (var culturePair in ValueCultureDict)
{
    string cultures = string.Join(", ", culturePair.Value);
    byte[] ucBytes = Encoding.Unicode.GetBytes(culturePair.Key);
    for (int i = 0; i < ucBytes.Length; i++)
    {
        Console.Write($"{ucBytes[i]} ");
    }
    Console.WriteLine($"   key  = {culturePair.Key} | value = {cultures}\n\n");
}



var test = Convert.ToDouble("1,1");
Console.WriteLine($"test = {test}");