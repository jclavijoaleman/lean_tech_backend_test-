using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using lean_tech_backend_test.Models;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.sql
{
    public class CarrierController : ApiController
    {
        // GET: api/Carrier
        [Route("sql/carrier")]
        public IEnumerable<Carrier> Get()
        {
            var ret = new List<Carrier>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

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
        [Route("sql/carrier/{id}")]
        public Carrier Get(int id)
        {

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

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

        // POST: api/Carrier
        [HttpPost]
        [Route("sql/carrier")]
        public void Post(Carrier obj)
        {
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO carriers (id_carrier, name_carrier, mc, dot, address, phone, id_type) Values (@id_carrier, @name_carrier, @mc, @dot,	@address, @phone, @id_type)";
            sqlCmd.Connection = myConnection;

            sqlCmd.Parameters.AddWithValue("@id_carrier", obj.id);
            sqlCmd.Parameters.AddWithValue("@name_carrier", obj.name);
            sqlCmd.Parameters.AddWithValue("@mc", obj.id);
            sqlCmd.Parameters.AddWithValue("@dot", obj.name);
            sqlCmd.Parameters.AddWithValue("@address", obj.id);
            sqlCmd.Parameters.AddWithValue("@phone", obj.name);
            sqlCmd.Parameters.AddWithValue("@id_type", obj.id);

            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        // PUT: api/Carrier/5
        [HttpPut]
        [Route("sql/carrier")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Carrier/5
        [HttpDelete]
        [Route("sql/carrier/{id}")]
        public void Delete(int id)
        {
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from carriers where id_carrier =" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
