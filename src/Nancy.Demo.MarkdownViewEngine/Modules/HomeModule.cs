namespace Nancy.Demo.MarkdownViewEngine.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using MarkdownSharp;
    using ViewEngines;

    public class HomeModule : NancyModule
    {
        private readonly IViewLocationProvider viewLocationProvider;
        private readonly IFileSystemReader fileSystemReader;
        private readonly IRootPathProvider rootPathProvider;
        private readonly string rootPath;

        public HomeModule(IViewLocationProvider viewLocationProvider, IFileSystemReader fileSystemReader, IRootPathProvider rootPathProvider)
        {
            this.viewLocationProvider = viewLocationProvider;
            this.fileSystemReader = fileSystemReader;
            this.rootPathProvider = rootPathProvider;
            this.rootPath = rootPathProvider.GetRootPath();

            var path = rootPathProvider.GetRootPath() + Path.DirectorySeparatorChar + "Views" +
                        Path.DirectorySeparatorChar + "Posts";


            Get["/"] = _ =>
                           {
                               var popularposts = GetModel(path, new[] { "md", "markdown" });

                               dynamic postModel = new ExpandoObject();
                               postModel.PopularPosts = popularposts;
                               postModel.MetaData = popularposts;

                               return View["blogindex", postModel];
                           };

            Get["/{viewname}"] = parameters =>
                                     {
                                         var popularposts = GetModel(path, new[] { "md", "markdown" });

                                         dynamic postModel = new ExpandoObject();
                                         postModel.PopularPosts = popularposts;
                                         postModel.MetaData =
                                             popularposts.FirstOrDefault(x => x.Slug == parameters.viewname);

                                         return View["Posts/" + parameters.viewname, postModel];
                                     };
        }

        private IEnumerable<BlogModel> GetModel(string path, IEnumerable<string> supportedViewExtensions)
        {
            var views = GetViewsFromPath(path, new[] { "md", "markdown" });

            var model = views.Select(x =>
                            {
                                var markdown = x.Contents().ReadToEnd();
                                return new BlogModel(markdown);
                            })
                            .OrderByDescending(x => x.BlogDate)
                            .ToList();

            return model;
        }

        private IEnumerable<ViewLocationResult> GetViewsFromPath(string path, IEnumerable<string> supportedViewExtensions)
        {
            var matches = this.fileSystemReader.GetViewsWithSupportedExtensions(path, supportedViewExtensions);

            return from match in matches
                   select
                       new FileSystemViewLocationResult(
                       GetViewLocation(match.Item1, this.rootPath),
                       Path.GetFileNameWithoutExtension(match.Item1),
                       Path.GetExtension(match.Item1).Substring(1),
                       match.Item2,
                       match.Item1,
                       this.fileSystemReader);
        }

        private static string GetViewLocation(string match, string rootPath)
        {
            var location = match
                .Replace(rootPath, string.Empty)
                .TrimStart(new[] { Path.DirectorySeparatorChar })
                .Replace(@"\", "/")
                .Replace(Path.GetFileName(match), string.Empty)
                .TrimEnd(new[] { '/' });

            return location;
        }
    }
}
