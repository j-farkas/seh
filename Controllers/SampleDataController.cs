using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace seh.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        [HttpGet("[action]/{words}")]
        public string ImageList(string words)
        {
          string rawData = Images.GetImages(words);
          return rawData;
        //  return Images.GetImageList(rawData);
        }

        public class Images
        {

            public string ImageURI { get; set; }
            public Images(string URI)
            {
              ImageURI = URI;
            }

          public static string GetImages(string Words)
          {
            string url = "https://www.google.com/search?q=" + Words + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if(dataStream == null)
                  return "";
                using (var sr = new StreamReader(dataStream))
                {
                  data = sr.ReadToEnd();
                }
            }
            return data;

          }
          public static List<Images> GetImageList(string html)
          {
            var images = new List<Images>();
            int i = html.IndexOf("class=\"images_table\"", StringComparison.Ordinal);
            i = html.IndexOf("<img", i, StringComparison.Ordinal);

            while( i >= 0)
            {
              i = html.IndexOf("src=\"", i, StringComparison.Ordinal);
              i+=5;
              int j = html.IndexOf("\"",i,StringComparison.Ordinal);
              string uri = html.Substring(i, j - i);
              images.Add(new Images(uri));
              i = html.IndexOf("<img", i, StringComparison.Ordinal);

            }
            return images;
          }
        }
    }
}
