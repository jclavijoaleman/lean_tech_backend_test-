using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using lean_tech_backend_test.Models;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.nosql
{
    public class OderBolNoSqlController : ApiController
    {
        // GET: api/OrderBol
        [Route("nosql/orderbol")]
        public IEnumerable<OrderBol> Get()
        {
            var ret = new List<OrderBol>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM orders_bols";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            OrderBol obj = null;
            while (reader.Read())
            {
                obj = new OrderBol();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.id_order = Convert.ToInt32(reader.GetValue(1));
                obj.id_bol = Convert.ToInt32(reader.GetValue(2));
                ret.Add(obj);
            }

            myConnection.Close();

            return ret;
        }

        // GET: api/OrderBol/5
        [Route("nosql/orderbol/{id}")]
        public OrderBol Get(int id)
        {

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM orders_bols where id_order_bol = " + id.ToString();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            OrderBol obj = null;
            while (reader.Read())
            {
                obj = new OrderBol();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.id_order = Convert.ToInt32(reader.GetValue(1));
                obj.id_bol = Convert.ToInt32(reader.GetValue(2));
            }

            myConnection.Close();

            return obj;
        }


        // POST: api/OderBolNoSql
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/OderBolNoSql/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/OderBolNoSql/5
        public void Delete(int id)
        {
        }
    }
}
