using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using lean_tech_backend_test.Models;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.nosql
{
    public class CarrierNoSqlController : ApiController
    {
        // GET: api/Carrier
        [Route("nosql/carrier")]
        public IEnumerable<Carrier> Get()
        {
            var ret = new List<Carrier>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM carriers";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Carrier obj = null;
            while (reader.Read())
            {
                obj = new Carrier();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.name = reader.GetValue(1).ToString();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.name = reader.GetValue(1).ToString();
                obj.mc = reader.GetValue(2).ToString();
                obj.dot = reader.GetValue(3).ToString();
                obj.address = Convert.IsDBNull(reader.GetValue(4)) ? "" : reader.GetValue(4).ToString();
                obj.phone = Convert.IsDBNull(reader.GetValue(5)) ? 0 : Convert.ToInt64(reader.GetValue(5));
                obj.id_type = Convert.ToInt32(reader.GetValue(6));
                ret.Add(obj);
            }

            myConnection.Close();

            return ret;
        }

        // GET: api/Carrier/5
        [Route("nosql/carrier/{id}")]
        public Carrier Get(int id)
        {

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM carriers where id_carrier = " + id.ToString();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Carrier obj = null;
            while (reader.Read())
            {
                obj = new Carrier();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.name = reader.GetValue(1).ToString();
                obj.mc = reader.GetValue(2).ToString();
                obj.dot = reader.GetValue(3).ToString();
                obj.address = reader.GetValue(4).ToString();
                obj.phone = Convert.ToInt32(reader.GetValue(5));
                obj.id_type = Convert.ToInt32(reader.GetValue(6));
            }

            myConnection.Close();

            return obj;
        }


        // POST: api/CarrierNoSql
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CarrierNoSql/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CarrierNoSql/5
        public void Delete(int id)
        {
        }
    }
}
