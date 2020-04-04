using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lean_tech_backend_test.Models
{
    public class Shipment
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public int id_carrier { get; set; }
    }
}