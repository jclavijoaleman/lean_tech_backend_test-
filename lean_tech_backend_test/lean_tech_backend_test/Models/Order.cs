using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lean_tech_backend_test.Models
{
    public class Order
    {
        public int id { get; set; }
        public string origin_city { get; set; }
        public int origin_zip { get; set; }
        public string origin_state { get; set; }
        public string destination_city { get; set; }
        public int destination_zip { get; set; }
        public string destination_state { get; set; }   
        public DateTime pickeup { get; set; }
        public DateTime delivery { get; set; }
        public decimal price { get; set; }
        public string status { get; set; }
        public int id_shipment { get; set; }
    }
}