using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using lean_tech_backend_test.Models;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.sql
{
    public class ShipmentsController : ApiController
    {
        // GET: api/Shipment
        [Route("sql/shipments")]
        public IEnumerable<Shipment> Get()
        {
            var ret = new List<Shipment>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM shipments";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Shipment obj = null;
            while (reader.Read())
            {
                obj = new Shipment();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.date = Convert.ToDateTime(reader.GetValue(1));
                obj.description = reader.GetValue(2).ToString();
                obj.id_carrier = Convert.ToInt32(reader.GetValue(3));
                ret.Add(obj);
            }

            myConnection.Close();

            return ret;
        }

        // GET: api/Shipment/5
        [Route("sql/shipments/{id}")]
        public Shipment Get(int id)
        {

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM shipments where id_shipment = " + id.ToString();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Shipment obj = null;
            while (reader.Read())
            {
                obj = new Shipment();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.date = Convert.ToDateTime(reader.GetValue(1));
                obj.description = reader.GetValue(2).ToString();
                obj.id_carrier = Convert.ToInt32(reader.GetValue(3));
            }

            myConnection.Close();

            return obj;
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
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from shipments where id_shipment =" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
