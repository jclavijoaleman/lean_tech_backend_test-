using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lean_tech_backend_test.Models
{
    public class Bol
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public string instructions { get; set; }
        public string items { get; set; }
    }
}