﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using lean_tech_backend_test.Models;
using MySql.Data.MySqlClient;

namespace lean_tech_backend_test.Controllers.sql
{
    public class OrderController : ApiController
    {
        // GET: api/Order
        [Route("sql/order")]
        public IEnumerable<Order> Get()
        {
            var ret = new List<Order>();

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM orders";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Order obj = null;
            while (reader.Read())
            {
                obj = new Order();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.origin_city = reader.GetValue(1).ToString();
                obj.origin_zip = Convert.ToInt32(reader.GetValue(2));
                obj.origin_state = reader.GetValue(3).ToString();
                obj.destination_city = reader.GetValue(4).ToString();
                obj.destination_zip = Convert.ToInt32(reader.GetValue(5));
                obj.destination_state = reader.GetValue(6).ToString();
                obj.pickeup = Convert.ToDateTime(reader.GetValue(7));
                obj.delivery = Convert.ToDateTime(reader.GetValue(8));
                obj.price = Convert.ToDecimal(reader.GetValue(9));
                obj.status = reader.GetValue(10).ToString();
                obj.id_shipment = Convert.ToInt32(reader.GetValue(11));

                ret.Add(obj);
            }

            myConnection.Close();

            return ret;
        }

        // GET: api/Order/5
        [Route("sql/order/{id}")]
        public Order Get(int id)
        {

            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandText = "SELECT * FROM orders where id_order = " + id.ToString();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            Order obj = null;
            while (reader.Read())
            {
                obj = new Order();
                obj.id = Convert.ToInt32(reader.GetValue(0));
                obj.origin_city = reader.GetValue(1).ToString();
                obj.origin_zip = Convert.ToInt32(reader.GetValue(2));
                obj.origin_state = reader.GetValue(3).ToString();
                obj.destination_city = reader.GetValue(4).ToString();
                obj.destination_zip = Convert.ToInt32(reader.GetValue(5));
                obj.destination_state = reader.GetValue(6).ToString();
                obj.pickeup = Convert.ToDateTime(reader.GetValue(7));
                obj.delivery = Convert.ToDateTime(reader.GetValue(8));
                obj.price = Convert.ToDecimal(reader.GetValue(9));
                obj.status = reader.GetValue(10).ToString();
                obj.id_shipment = Convert.ToInt32(reader.GetValue(11));
            }

            myConnection.Close();

            return obj;
        }

        // POST: api/Order
        [HttpPost]
        [Route("sql/order")]
        public void Post(Order obj)
        {
        }

        // PUT: api/Order/5
        [HttpPut]
        [Route("sql/order")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Order/5
        [HttpDelete]
        [Route("sql/order/{id}")]
        public void Delete(int id)
        {
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = @"Server=127.0.0.1;Port=49489;Database=lean_tech_backend_test;Uid=azure;Pwd=6#vWHD_$;";

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from orders where id_order =" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
