using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB5
{
    internal class Model
    {
        public class Rootobject
        {
            public Response response { get; set; }
        }

        public class Response
        {
            public int id { get; set; }
            public string home_town { get; set; }
            public string status { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string bdate { get; set; }
            public City city { get; set; }
            public Country country { get; set; }
            public string phone { get; set; }
            public int sex { get; set; }
        }

        public class City
        {
            public int id { get; set; }
            public string title { get; set; }
        }

        public class Country
        {
            public int id { get; set; }
            public string title { get; set; }
        }
    }
}
