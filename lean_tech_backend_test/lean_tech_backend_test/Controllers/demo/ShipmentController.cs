using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ExcelDataReader;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.demo
{
    public class ShipmentController : ApiController
    {
        // GET: api/Shipment
        [Authorize(Roles= "admin, readonly")]
        [Route("demo/shipment")]
        public IEnumerable<Shipment> Get()
        {
            var ret = new List<Shipment>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT c.NAME , c.dot, c.MC, s.date, s.ORIGIN_COUNTRY, s.ORIGIN_CITY, s.ORIGIN_STATE, s.DESTINATION_COUNTRY, s.DESTINATION_STATE, s.DESTINATION_CITY, s.PICKUP_DATE, s.DELIVERY_DATE, s.STATUS, s.CARRIER_RATE FROM lean_tech_backend_test.shipment s inner join lean_tech_backend_test.carrier c on c.id = s.CARRIER_ID order by s.id desc; ";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Shipment obj = null;
            while (reader.Read())
            {
                obj = new Shipment();
                obj.carrier_name = reader.GetValue(0).ToString();
                obj.dot = reader.GetValue(1).ToString();
                obj.mc = reader.GetValue(2).ToString();
                obj.date = reader.GetValue(3).ToString();
                obj.origin_country = reader.GetValue(4).ToString();
                obj.origin_city = reader.GetValue(5).ToString();
                obj.origin_state = reader.GetValue(6).ToString();
                obj.destination_country = reader.GetValue(7).ToString();
                obj.destination_state = reader.GetValue(8).ToString();
                obj.destination_city = reader.GetValue(9).ToString();
                obj.pickeup = reader.GetValue(10).ToString();
                obj.delivery = reader.GetValue(11).ToString();
                obj.status = reader.GetValue(12).ToString();
                obj.price = Convert.ToDouble(reader.GetValue(13));
                ret.Add(obj);
            }

            myConnection.Close();

            return ret;
        }

        // GET: api/Shipment/5
        [Authorize(Roles = "admin, readonly")]
        [Route("demo/shipment/{search}")]
        public IEnumerable<Shipment> Get(string search)
        {

            var ret = new List<Shipment>();

            string filter = "";
            if (!string.IsNullOrEmpty(search))
            {
                var critera = search.Split(' ');

                foreach (string element in critera)
                {
                    filter = filter + (string.IsNullOrEmpty(filter) ? " where " : " or ") + " c.mc like '%" + element + "%' or c.dot like '%" + element + "%' or c.NAME like '%" + element + "%' or ORIGIN_CITY like '%" + element + "%' or DESTINATION_CITY like '%" + element + "%' ";
                }
            }


            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT c.NAME carriername, c.dot, c.MC, s.date, s.ORIGIN_COUNTRY, s.ORIGIN_CITY, s.ORIGIN_STATE, s.DESTINATION_COUNTRY, s.DESTINATION_STATE, s.DESTINATION_CITY, s.PICKUP_DATE, s.DELIVERY_DATE, s.STATUS, s.CARRIER_RATE FROM lean_tech_backend_test.shipment s inner join lean_tech_backend_test.carrier c on c.id = s.CARRIER_ID " + filter + " order by s.id desc; ";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Shipment obj = null;
            while (reader.Read())
            {
                obj = new Shipment();
                obj.carrier_name = reader.GetValue(0).ToString();
                obj.dot = reader.GetValue(1).ToString();
                obj.mc = reader.GetValue(2).ToString();
                obj.date = reader.GetValue(3).ToString();
                obj.origin_country = reader.GetValue(4).ToString();
                obj.origin_city = reader.GetValue(5).ToString();
                obj.origin_state = reader.GetValue(6).ToString();
                obj.destination_country = reader.GetValue(7).ToString();
                obj.destination_state = reader.GetValue(8).ToString();
                obj.destination_city = reader.GetValue(9).ToString();
                obj.pickeup = reader.GetValue(10).ToString();
                obj.delivery = reader.GetValue(11).ToString();
                obj.status = reader.GetValue(12).ToString();
                obj.price = Convert.ToDouble(reader.GetValue(13));
                ret.Add(obj);
            }

            myConnection.Close();

            return ret;
        }

        [HttpPost]
        [Route("demo/export/{type}")]
        public HttpResponseMessage Export(string type)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write("Hello, World!");
            writer.Flush();
            stream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };

            return result;
        }

        
        [Route("demo/import")]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            var filename = "";
            string[] type = null;
            List<Shipment> obj = new List<Shipment>();

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    type = file.Headers.ContentDisposition.FileName.Split('.');
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                    filename = file.LocalFileName;
                }

                string ext = type[type.Length - 1].ToString().ToLower();
                if (ext.Contains("xls"))
                {
                    FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read);

                    IExcelDataReader excelReader = ext == "xls" ? ExcelReaderFactory.CreateBinaryReader(stream)
                    : ExcelReaderFactory.CreateOpenXmlReader(stream);

                    //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                    DataSet result = excelReader.AsDataSet();
                    DataTable workSheet = result.Tables[2];

                    foreach (DataRow row in workSheet.Rows)
                    {
                        obj.Add(new Shipment
                        {
                            carrier_id = row[0].ToString(),
                            date = row[1].ToString(),
                            origin_country = row[2].ToString(),
                            origin_state = row[3].ToString(),
                            origin_city = row[4].ToString(),
                            destination_country = row[5].ToString(),
                            destination_state = row[6].ToString(),
                            destination_city = row[7].ToString(),
                            pickeup = row[8].ToString(),
                            delivery = row[9].ToString(),
                            status = row[10].ToString(),
                            carrier_price = row[11].ToString()
                        });
                    }

                    excelReader.Close();

                }
                else
                {
                    string csvData = System.IO.File.ReadAllText(filename);

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            obj.Add(new Shipment
                            {
                                carrier_id = row.Split(',')[0],
                                date = row.Split(',')[1],
                                origin_country = row.Split(',')[2],
                                origin_state = row.Split(',')[3],
                                origin_city = row.Split(',')[4],
                                destination_country = row.Split(',')[5],
                                destination_state = row.Split(',')[6],
                                destination_city = row.Split(',')[7],
                                pickeup = row.Split(',')[8],
                                delivery = row.Split(',')[9],
                                status = row.Split(',')[10],
                                carrier_price = row.Split(',')[11]
                            });
                        }
                    }

                }


                var mt = new MediaTypeWithQualityHeaderValue("application/json");
                var response = Request.CreateResponse(HttpStatusCode.OK, obj, mt);

                return response;
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        // POST: api/Shipment
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("demo/shipment")]
        public void Post(Shipment obj)
        {
        }

        // PUT: api/Shipment/5
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("demo/shipment")]
        public void Put(int id, Shipment obj)
        {
        }

        // DELETE: api/Shipment/5
        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("demo/shipment/{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Shipment
    {
        public string carrier_id { get; set; }
        public string carrier_name { get; set; }
        public string mc { get; set; }
        public string dot { get; set; }
        public string date { get; set; }
        public string origin_city { get; set; }
        public string origin_country { get; set; }
        public string origin_state { get; set; }
        public string destination_city { get; set; }
        public string destination_country { get; set; }
        public string destination_state { get; set; }
        public string pickeup { get; set; }
        public string delivery { get; set; }
        public double price { get; set; }
        public string carrier_price { get; set; }
        public string status { get; set; }
    }
}
