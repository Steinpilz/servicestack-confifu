using Confifu.Abstractions;
using Confifu.Abstractions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStack.Confifu
{
    public static class AppConfigExtensions
    {
        public static IAppConfig UseServiceStack(this IAppConfig appConfig, Action<ServiceStackConfig> configurator)
        {
            var config = appConfig.EnsureConfig("ServiceStack",
                () => new ServiceStackConfig(appConfig),
                c =>
            {
                c.ConfigureEndpoint((appHost, endpointConfig) => Default.ConfigureAppHost(appConfig, endpointConfig));


                appConfig.AddRunner(new ServiceStackAppRunner(c).Run);
            });

            configurator?.Invoke(config);
            
            return appConfig;
        }
    }
}
