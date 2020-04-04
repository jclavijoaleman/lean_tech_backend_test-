using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using lean_tech_backend_test.Models;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.sql
{
    public class BolController : ApiController
    {
        // GET: api/Bol
        [Route("sql/bol")]
        public IEnumerable<Bol> Get()
        {
            var ret = new List<Bol>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM bols";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Bol obj = null;
            while (reader.Read())
            {
                obj = new Bol();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.name = reader.GetValue(1).ToString();
                obj.date = Convert.ToDateTime(reader.GetValue(2));
                obj.instructions = reader.GetValue(3).ToString();
                obj.items = reader.GetValue(4).ToString();
                ret.Add(obj);
            }

            myConnection.Close();

            return ret;
        }

        // GET: api/Bol/5
        [Route("sql/bol/{id}")]
        public Bol Get(int id)
        {

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM bols where id_bol = " + id.ToString();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Bol obj = null;
            while (reader.Read())
            {
                obj = new Bol();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.name = reader.GetValue(1).ToString();
                obj.date = Convert.ToDateTime(reader.GetValue(2));
                obj.instructions = reader.GetValue(3).ToString();
                obj.items = reader.GetValue(4).ToString();
            }

            myConnection.Close();

            return obj;
        }

        // POST: api/Bol
        [HttpPost]
        [Route("sql/bol")]
        public void Post(Bol obj)
        {
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO bols (id_bol,name,date,instructions,items) Values (@id_bol, @name, @date, @instructions, @items)";
            sqlCmd.Connection = myConnection;

            sqlCmd.Parameters.AddWithValue("@id_bol", obj.id);
            sqlCmd.Parameters.AddWithValue("@name", obj.name);
            sqlCmd.Parameters.AddWithValue("@date", obj.date);
            sqlCmd.Parameters.AddWithValue("@instructions", obj.instructions);
            sqlCmd.Parameters.AddWithValue("@items", obj.items);

            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        // PUT: api/Bol/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bol/5
        [HttpDelete]
        [Route("sql/bol/{id}")]
        public void Delete(int id)
        {
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from bols where id_bol =" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
