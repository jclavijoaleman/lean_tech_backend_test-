using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using lean_tech_backend_test.Models;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.nosql
{
    public class TypeCarrierNoSqlController : ApiController
    {
        // GET: api/TypeCarrier
        [Route("nosql/typecarrier")]
        public IEnumerable<TypeCarrier> Get()
        {
            var ret = new List<TypeCarrier>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM type_carrier";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            TypeCarrier obj = null;
            while (reader.Read())
            {
                obj = new TypeCarrier();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.name = reader.GetValue(1).ToString();
                ret.Add(obj);
            }

            myConnection.Close();

            return ret;
        }

        // GET: api/TypeCarrier/5
        [Route("nosql/typecarrier/{id}")]
        public TypeCarrier Get(int id)
        {

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=3306;Database=lean_tech_backend_test;Uid=root;Pwd=jmca83;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM type_carrier where id_type = " + id.ToString();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            TypeCarrier obj = null;
            while (reader.Read())
            {
                obj = new TypeCarrier();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.name = reader.GetValue(1).ToString();
            }

            myConnection.Close();

            return obj;
        }

        // POST: api/TypeCarrierNoSql
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TypeCarrierNoSql/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TypeCarrierNoSql/5
        public void Delete(int id)
        {
        }
    }
}
