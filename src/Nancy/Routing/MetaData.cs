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

            /// <summary>
            /// The description of the parameter
            /// </summary>
            public string Description { get; set; }
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

        /// <summary>
        /// The possible return status codes
        /// </summary>
        public List<StatusCode> StatusCodes { get; set; }

        /// <summary>
        /// Metadata for a status code
        /// </summary>
        public class StatusCode
        {
            /// <summary>
            /// Reason for the status code
            /// </summary>
            public string Reason { get; set; }

            /// <summary>
            /// The status code
            /// </summary>
            public int HttpStatusCode { get; set; }
        }

        /// <summary>
        /// Information about the response
        /// </summary>
        public Response ResponseData { get; set; }

        /// <summary>
        /// Meta data for a response
        /// </summary>
        public class Response
        {
            /// <summary>
            /// The type of data returned
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// The properties of the returned data
            /// </summary>
            public List<Property> Properties { get; set; }

            /// <summary>
            /// Metadata for a property
            /// </summary>
            public class Property
            {
                /// <summary>
                /// The name of the property
                /// </summary>
                public string Name { get; set; }

                /// <summary>
                /// A description about the property
                /// </summary>
                public string Description { get; set; }
            }
        }
    }
}
