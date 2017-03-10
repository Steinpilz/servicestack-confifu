using Confifu.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStack.Confifu
{
    public static class AppConfigExtensions
    {
        public static IAppConfig UseServiceStack(IAppConfig appConfig, Action<ServiceStackConfig> configurator)
        {
            appConfig.RunOnce("ServiceStack", () =>
            {
                var newConfig = new ServiceStackConfig(appConfig);
                newConfig.ConfigureEndpoint((appHost, endpointConfig) => Default.ConfigureAppHost(appConfig, endpointConfig));

                appConfig.AddRunner(() => new ServiceStackAppRunner(appConfig.GetServiceStackConfig()).Run());
            });

            var config = appConfig.GetServiceStackConfig();

            configurator?.Invoke(config);

            return appConfig;
        }

        private static ServiceStackConfig GetServiceStackConfig(this IAppConfig appConfig)
        {
            return appConfig["ServiceStack:Config"] as ServiceStackConfig;
        }

        private static IAppConfig SetServiceStackConfig(this IAppConfig appConfig, ServiceStackConfig config)
        {
            appConfig["ServiceStack:Config"] = config;
            return appConfig;
        }
    }
}
