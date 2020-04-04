using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace lean_tech_backend_test.Controllers
{
    public class ApiEiaController : ApiController
    {
        private const string URL = "https://api.eia.gov/series/";
        private string urlParameters = "?api_key=6d8b1b5a4021cffaa3104e9a5775e390&series_id=PET.EMM_EPMRU_PTE_NUS_DPG.W";

        [HttpGet]
        [Route("api/eia")]
        public string GetTxt()
        {
            var request = (HttpWebRequest)WebRequest.Create(URL + urlParameters);

            request.Method = "GET";
            request.ContentType = "application/json";
            
            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            //// Create the response
            //var mt = new MediaTypeWithQualityHeaderValue("application/json");
            //var res = Request.CreateResponse(HttpStatusCode.OK, content, mt);

            return content;
        }
    }
}
