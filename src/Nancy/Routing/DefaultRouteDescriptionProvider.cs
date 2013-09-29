using System.Collections.Generic;
using Nancy.Json;
using System.IO;

namespace Nancy.Routing
{
	using System;
	using System.Linq;
	using System.Resources;

	/// <summary>
	/// Default implementation of the <see cref="IRouteDescriptionProvider"/> interface. Will look for
	/// route descriptions in resource files. The resource files should have the same name as the module
	/// for which it defines routes.
	/// </summary>
	public class DefaultRouteDescriptionProvider : IRouteDescriptionProvider
	{
		private MetaData RouteMetaData;

		public DefaultRouteDescriptionProvider ()
		{



		}

		/// <summary>
		/// Get the description for a route.
		/// </summary>
		/// <param name="module">The module that the route is defined in.</param>
		/// <param name="path">The path of the route that the description should be retrieved for.</param>
		/// <returns>A <see cref="string"/> containing the description of the route if it could be found, otherwise <see cref="string.Empty"/>.</returns>
		public string GetDescription (INancyModule module, string path)
		{
			var assembly =
                module.GetType ().Assembly;

			var moduleName =
                string.Concat (module.GetType ().FullName, ".resources");

			var resourceName = assembly
                .GetManifestResourceNames ()
                .FirstOrDefault (x => x.Equals (moduleName, StringComparison.OrdinalIgnoreCase));
                
			if (resourceName != null) {
				var manager =
                    new ResourceManager (resourceName.Replace (".resources", string.Empty), assembly);

				return manager.GetString (path);
			}

			return string.Empty;
		}

		public object GetMetaData (INancyModule module, string path, string method)
		{
			var assembly =
				module.GetType ().Assembly;

			var moduleName =
				string.Concat (module.GetType ().FullName, ".json");

			var resourceName = assembly
				.GetManifestResourceNames ()
					.FirstOrDefault (x => x.Equals (moduleName, StringComparison.OrdinalIgnoreCase));


			if (resourceName != null) {
				string jsonData = string.Empty;

				using (Stream stream = assembly.GetManifestResourceStream(resourceName))
					using (StreamReader reader = new StreamReader(stream)) {
					jsonData = reader.ReadToEnd ();
				}
				if (!string.IsNullOrWhiteSpace (jsonData)) {
					var serializer = new JsonDeserializer(new JavaScriptSerializer());
					var deserialized = serializer.Deserialize(jsonData);
					return new object ();
				}
				return null;
			}


			return null;
		}

		internal class MetaData
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
}