using System;
using Nancy.Routing;
using System.Linq;

namespace Nancy.Demo.RoutingMetaData
{
	public class MetaDataModule : NancyModule
	{
		public MetaDataModule (IRouteCacheProvider routeCacheProvider)
		{
			Get ["/"] = parameters => {
				var routes = routeCacheProvider.GetCache();

					var model =
						routes.SelectMany(
							x =>
							x.Value.Select(
							y =>
							new 
							{
							Method = y.Item2.Method,
							Path = y.Item2.Path,
							MetaData = y.Item2.MetaData,
						}));

				return model;
				
			};

			Get ["/anotherroute/{id}"] = parameters => {return 200;};

			Get ["/moreroutes"] = parameters => {return 200;};
		}
	}
}

