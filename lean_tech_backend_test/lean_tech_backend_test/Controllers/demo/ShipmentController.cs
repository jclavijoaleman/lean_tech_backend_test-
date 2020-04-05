using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.demo
{
    public class ShipmentController : ApiController
    {
        // GET: api/Shipment
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

        // POST: api/Shipment
        [HttpPost]
        [Route("sql/shipments")]
        public void Post(Shipment obj)
        {
        }

        // PUT: api/Shipment/5
        [HttpPut]
        [Route("sql/shipments")]
        public void Put(int id, Shipment obj)
        {
        }

        // DELETE: api/Shipment/5
        [HttpDelete]
        [Route("sql/shipments/{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Shipment
    {
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
        public string status { get; set; }
    }
}
