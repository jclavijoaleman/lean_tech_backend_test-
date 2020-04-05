using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using lean_tech_backend_test.Models;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.nosql
{
    public class BolNoSqlController : ApiController
    {
        // GET: api/Bol
        [Route("nosql/bol")]
        public IEnumerable<Bol> Get()
        {
            var ret = new List<Bol>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

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
        [Route("nosql/bol/{id}")]
        public Bol Get(int id)
        {

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

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


        // POST: api/BolNoSql
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BolNoSql/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BolNoSql/5
        public void Delete(int id)
        {
        }
    }
}
