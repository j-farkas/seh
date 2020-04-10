using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace seh.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {

        [HttpGet("[action]")]
        public IEnumerable<Images> ImageList()
        {

        }

        public class Images
        {
            public string ImageURI { get; set; }
        }

        public static string GetImages(string Words)
        {
          string url = "https://www.google.com/search?q=" + topics + "&tbm=isch";
          string data = "";

          var request = (HttpWebRequest)WebRequest.Create(url);
          var response = (HttpWebResponse)request.GetResponse();

          using (Stream dataStream = response.GetResponseSteam())
          {
              if(dataStream == null)
                return "";
              using (var sr = newStreamReader(dataStream))
              {
                data = sr.ReadToEnd();
              }
          }
          return data;

        }
        public static FormatImageText(string RawHTML)
        {
          
        }
    }
}
