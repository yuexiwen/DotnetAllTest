using System.Globalization;

double value = 12345.67891;
var cultureLst = CultureInfo.GetCultures(CultureTypes.AllCultures);
foreach (var item in cultureLst)
{
    Console.WriteLine($"{item.Name}");
}