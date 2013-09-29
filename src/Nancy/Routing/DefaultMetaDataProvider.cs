using System.Collections.Generic;
using Nancy.Json;
using System.IO;

namespace Nancy.Routing
{
    using System;
    using System.Linq;

    /// <summary>
    /// Default implementation of the <see cref="IRouteMetaDataProvider"/> interface. Will look for
    /// route descriptions in resource files. The resource files should have the same name as the module
    /// for which it defines routes.
    /// </summary>
    public class DefaultMetaDataProvider : IRouteMetaDataProvider
    {
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
                string jsonData = string.Empty;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
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
                            string.Equals(method, x.method, StringComparison.InvariantCultureIgnoreCase) &&
                            string.Equals(x.path, path, StringComparison.InvariantCultureIgnoreCase));
                }
                return null;
            }
            return null;
        }
    }
}