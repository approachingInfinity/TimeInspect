using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using WebPageAge.Models;

namespace WebPageAge.Controllers
{
    public class HomeController : Controller
    {
        //WebSraping webScraping;
        private readonly ILogger<HomeController> _logger;
          string? datePublished = null;
          string? dateModified = null;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async  Task<IActionResult> Index(WebpageInfo model)
        {
           string url = model.url;
            try
            {
                string? htmlContent = null;
                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        htmlContent = await client.GetStringAsync(url);
                    }

                }
                HtmlDocument? doc = null;
                if (htmlContent != null)
                {
                    doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);


                    HtmlNodeCollection? scriptNodes = null;
                    if (doc != null)
                    {
                        scriptNodes = doc.DocumentNode.SelectNodes("//script[@type='application/ld+json']");
                        if (scriptNodes != null)
                        {
                            foreach (var scriptNode in scriptNodes)
                            {
                                string scriptContent = scriptNode.InnerHtml;
                                Console.WriteLine(scriptContent);
                                JToken jToken = JToken.Parse(scriptContent);
                                // Parse as JArray instead of JObject
                                if (jToken.Type == JTokenType.Object)
                                {


                                    string? dp = WebSraping.getDateFromJObject(jToken, "datePublished");
                                    string? dm = WebSraping.getDateFromJObject(jToken, "dateModified");
                                    if (dp != null)
                                        datePublished = dp;
                                    if (dm != null)
                                        dateModified = dm;
                                    //Console.WriteLine(datePublished + " " + dateModified);


                                }
                                else if (jToken.Type == JTokenType.Array)
                                {


                                    string? dp = WebSraping.getDateFromJArray(jToken, "datePublished");
                                    string? dm = WebSraping.getDateFromJArray(jToken, "dateModified");
                                    if (dp != null)
                                        datePublished = dp;
                                    if (dm != null)
                                        dateModified = dm;
                                    //Console.WriteLine(datePublished + "|" + dateModified);


                                }
                                else
                                {
                                    Console.WriteLine("neither Jobject nor JArray");
                                }

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occurred may be provided url is not incorrect or access denied");
            }
            if(datePublished!=null)model.datePublished= datePublished;
            if(dateModified!=null)model.dateModified= dateModified;   
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}