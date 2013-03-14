namespace Nancy.Demo.MarkdownViewEngine
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Threading;
    using CsQuery;
    using MarkdownSharp;

    [Serializable]
    public class BlogModel
    {
        private static readonly Regex SSVESubstitution = new Regex("^@[^$]*?$",
                                                                   RegexOptions.Compiled | RegexOptions.IgnoreCase |
                                                                   RegexOptions.Multiline);

        private readonly Markdown parser = new Markdown();

        public string Title { get; private set; }

        public string Abstract { get; private set; }

        public DateTime BlogDate { get; private set; }

        public string FriendlyDate
        {
            get { return BlogDate.ToString("dddd,MMMM dd, yyyy"); }
        }

        public string StrippedTitle
        {
            get
            {
                CQ data = Title;
                return data.Html();
            }
        }

        public BlogModel(string markdown)
        {
            BlogDate = GetBlogDate(markdown);
            Title = GetTitle(markdown);
            Abstract = GetAbstract(markdown);
        }

        private DateTime GetBlogDate(string content)
        {
            string ssveRemoved = SSVESubstitution.Replace(content, "").Trim();
            string datetimeString = ssveRemoved.Substring(0, ssveRemoved.IndexOf(Environment.NewLine, StringComparison.Ordinal));
            return DateTime.ParseExact(datetimeString, "yyyyMMdd", CultureInfo.InvariantCulture);

        }

        private string GetTitle(string content)
        {
            string ssveRemoved = SSVESubstitution.Replace(content, "").Trim();

            var lines = Regex.Split(ssveRemoved, Environment.NewLine);

            return
                parser.Transform(lines[2]);
        }

        private string GetAbstract(string content)
        {
            string ssveRemoved = SSVESubstitution.Replace(content, "").Trim();
            var abstractpost = ssveRemoved.Substring(ssveRemoved.IndexOf(")", StringComparison.Ordinal) + 1, 175).Trim();
            return parser.Transform(abstractpost);
        }
    }
}