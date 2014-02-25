namespace Nancy.Tests.Functional.Modules
{
    public class ExceptionTestModule : NancyModule
    {
        public ExceptionTestModule()
        {
            Get["/"] = _ => "hi";
        }
    }
}
