
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using WebPageAge.Models;

namespace WebPageAge
{
    public class WebSraping
    {       

        public static string? getDateFromJArray(JToken jToken, String datePublishedOrModified)
        {
            JArray jArray = (JArray)jToken;
            string? date = null;
            foreach (var json in jArray)
            {
                if (json[datePublishedOrModified] != null)
                {
                    //Console.WriteLine($"datePublished {json["datePublished"]}");
                    date = json[datePublishedOrModified]?.ToString();
                }
            }
            return date;
        }

        public static string? getDateFromJObject(JToken jToken, String datePublishedOrModified)
        {

            string? date = null;
            JObject jObject = (JObject)jToken;
            foreach (var property in jObject)
            {
                if (property.Key.Contains(datePublishedOrModified))
                {
                    date = property.Value?.ToString();
                   
                }
                 else if (jObject.ContainsKey("@graph"))
                {

                    int? size = jObject["@graph"]?.Count();

                    for (int i = 0; i < size; i++)
                    {
                        JToken? firstElement = jObject["@graph"]?.ElementAt(i);
                      
                        if (firstElement is JObject firstObject)
                        {
                            if (firstObject.ContainsKey(datePublishedOrModified))
                            {
                                date = firstObject[datePublishedOrModified]?.ToString();
                               
                                break;
                            }

                        }

                    }


                }

            }
            return date;
        }
    }
}
