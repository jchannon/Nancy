namespace Nancy.Routing
{
    /// <summary>
    /// Defines the functionality for retriving metadata for a specific route.
    /// </summary>
    public interface IRouteMetaDataProvider
    {
        /// <summary>
        /// Returns metadata for a specific route
        /// </summary>
        /// <param name="module">The module being inspected</param>
        /// <param name="path">The path on the route</param>
        /// <param name="method">The HTTP verb</param>
        /// <returns>Metadata for the route</returns>
        MetaData GetMetaData(INancyModule module, string path, string method);
    }
}