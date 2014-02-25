namespace Nancy.Tests.Functional.Tests
{
    using System;
    using System.Collections.Generic;

    using Nancy.Bootstrapper;
    using Nancy.Testing;
    using Nancy.Tests.Functional.Modules;
    using Nancy.TinyIoc;

    using Xunit;

    public class ExceptionTests
    {
        private ExceptionBootstrapper bootstrapper;
        private Browser browser;
        public static bool OnErrorHit;

        public ExceptionTests()
        {
            this.bootstrapper = new ExceptionBootstrapper();

            this.browser = new Browser(bootstrapper);

            OnErrorHit = false;
        }

        [Fact]
        public void Exception_In_Bootstrapper_Should_Hit_OnError()
        {
            //Given
            
            //When
            var result = browser.Get("/");

            //Then
            Assert.True(OnErrorHit);
        }
    }

    public class ExceptionBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.OnError += (ctx, ex) =>
            {
                ExceptionTests.OnErrorHit = true;
                return null;
            };
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            throw new Exception("Request went bang!");
        }

        protected override IEnumerable<ModuleRegistration> Modules
        {
            get { return new[] {new ModuleRegistration(typeof (ExceptionTestModule))}; }
        }
    }
}
