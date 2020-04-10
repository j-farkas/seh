using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Powerpoint;
using Microsoft.AspNetCore.Mvc;

namespace seh.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        [HttpGet("[action]/{words}")]
        public List<Images> ImageList(string words)
        {
          string rawData = Images.GetImages(words);
          // return rawData;
         return Images.GetImageList(rawData);
        }

        [HttpPost("[action]/")]
        public void CreatePpt(String data)
        {
          // Console.WriteLine(data);
          using (var reader = new StreamReader(Request.Body))
          {
            PptSlide ppt = new PptSlide();
            var body = reader.ReadToEnd();
            Console.WriteLine(body);
            String splitTheBody = body;
            ppt.title=splitTheBody.Split('=')[1].Split('&')[0];
            ppt.text=splitTheBody.Split('=')[2].Split('&')[0];
            ppt.imageList = PptSlide.getImages(splitTheBody);
            //Console.WriteLine(splitTheBody.Split('=')[1]);
            //PptSlide ppt = JsonConvert.DeserializeObject<PptSlide>(body);
            Console.WriteLine("AHHHHHHH");
            Console.WriteLine(ppt.title);
            Console.WriteLine(ppt.text);
          }

        }

        public class PptSlide
        {
          public string title { get; set; }
          public string text { get; set; }
          public List<Images> imageList { get; set; }

          public PptSlide(){
            imageList = new List<Images>();
            title = "";
            text = "";
          }

          public static List<Images> getImages(String html)
          {

            List<Images> images = new List<Images>();
              int i = html.IndexOf("imageList", StringComparison.Ordinal);
              i = html.IndexOf("imageURI", i, StringComparison.Ordinal);
             Console.WriteLine(html);
             int loops = 0;
            while( i >= 0 && loops < 20)
            {
              i = html.IndexOf("=", i, StringComparison.Ordinal);
              i+=5;
              int j = html.IndexOf("&",i,StringComparison.Ordinal);
              string uri = html.Substring(i, j - i);
              Console.WriteLine(uri);
              images.Add(new Images(uri));
              i = html.IndexOf("imageURI", i, StringComparison.Ordinal);
              loops++;

            }
            return images;
          }

        }

        public class Images
        {
            public string ImageURI { get; set; }
            public bool Selected { get; set; }

            public Images(string URI)
            {
              ImageURI = URI;
              Selected = false;
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

            List<Images> images = new List<Images>();
              int i = html.IndexOf("heirloom", StringComparison.Ordinal);
              i = html.IndexOf("<img", i, StringComparison.Ordinal);
             Console.WriteLine(html);
             int loops = 0;
            while( i >= 0 && loops < 20)
            {
              i = html.IndexOf("src=\"", i, StringComparison.Ordinal);
              i+=5;
              int j = html.IndexOf("\"",i,StringComparison.Ordinal);
              string uri = html.Substring(i, j - i);
              Console.WriteLine(uri);
              images.Add(new Images(uri));
              i = html.IndexOf("<img", i, StringComparison.Ordinal);
              loops++;

            }
            return images;
          }
        }
    }
}
