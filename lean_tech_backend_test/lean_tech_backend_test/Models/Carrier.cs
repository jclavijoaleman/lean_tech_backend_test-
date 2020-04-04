using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lean_tech_backend_test.Models
{
    public class Carrier
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mc { get; set; }
        public string dot { get; set; }
        public string address { get; set; }
        public Int64 phone { get; set; }
        public int id_type { get; set; }
    }
}