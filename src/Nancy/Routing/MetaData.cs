using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Routing
{
    public class MetaData
    {
        public class Parameter
        {
            public string type { get; set; }

            public string name { get; set; }
        }

        public string method { get; set; }

        public string path { get; set; }

        public List<Parameter> parameters { get; set; }
    }
}
