namespace Nancy.Routing
{
    /// <summary>
    /// Defines the functionality for retriving a description for a specific route.
    /// </summary>
    public interface IRouteMetaDataProvider
    {
		MetaData GetMetaData (INancyModule module, string path, string method);
    }
}