using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Routing
{
    /// <summary>
    /// Metadata information about the routes in a module
    /// </summary>
    public class MetaData
    {
        /// <summary>
        /// Information about parameters defined in the route
        /// </summary>
        public class Parameter
        {
            /// <summary>
            /// The data type of the parameter
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// The name of the parameter
            /// </summary>
            public string Name { get; set; }
        }

        /// <summary>
        /// The HTTP verb method of the route
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The path of the route
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Information about any parameters defined in the path
        /// </summary>
        public List<Parameter> Parameters { get; set; }
    }
}
