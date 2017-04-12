using Confifu.Abstractions;
using Confifu.Abstractions.DependencyInjection;
using Confifu.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceStack.Confifu.Tests
{
    public class AppTests
    {

        class App : global::Confifu.AppSetup
        {
            public App() : base(new EmptyConfigVariables())
            {
                Configure(() =>
                {
                    AppConfig
                       .RegisterCommonServices()
                       .UseServiceStack(c =>
                       {
                           c.ServiceHostAssemblies.Add(GetType().Assembly);
                           c.WebAppHost();
                       });

                    AppConfig.AddAppRunnerAfter(() =>
                    {
                        AppConfig.SetupAutofacContainer();
                    });
                });
               
            }
        }

        static AppTests()
        {
            new App().Setup().Run();
        }

        [Fact]
        public void it_does_not_smoke()
        {

        }
    }
}
