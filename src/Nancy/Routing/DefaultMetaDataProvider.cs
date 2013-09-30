using System.Collections.Generic;
using Nancy.Json;
using System.IO;

namespace Nancy.Routing
{
    using System;
    using System.Linq;

    /// <summary>
    /// Default implementation of the <see cref="IRouteMetaDataProvider"/> interface. Will look for
    /// route metadata in JSON files. The JSON files should have the same name as the module
    /// for which it defines routes.
    /// </summary>
    public class DefaultMetaDataProvider : IRouteMetaDataProvider
    {
        /// <summary>
        /// Returns metadata for a specific route
        /// </summary>
        /// <param name="module">The module being inspected</param>
        /// <param name="path">The path on the route</param>
        /// <param name="method">The HTTP verb</param>
        /// <returns>Metadata for the route</returns>
        public MetaData GetMetaData(INancyModule module, string path, string method)
        {
            var assembly =
                module.GetType().Assembly;

            var moduleName =
                string.Concat(module.GetType().FullName, ".json");

            var resourceName = assembly
                .GetManifestResourceNames()
                    .FirstOrDefault(x => x.Equals(moduleName, StringComparison.OrdinalIgnoreCase));


            if (resourceName != null)
            {
                var jsonData = string.Empty;

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new StreamReader(stream))
                {
                    jsonData = reader.ReadToEnd();
                }

                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var serializer = new JavaScriptSerializer();
                    var metadata = serializer.Deserialize<List<MetaData>>(jsonData);
                    return
                        metadata.FirstOrDefault(
                            x =>
                            string.Equals(x.Method, method, StringComparison.InvariantCultureIgnoreCase) &&
                            string.Equals(x.Path, path, StringComparison.InvariantCultureIgnoreCase));
                }
                return null;
            }
            return null;
        }
    }
}