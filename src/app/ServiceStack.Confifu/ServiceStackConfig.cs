using System;
using System.Collections.Generic;
using System.Reflection;
using ServiceStack.WebHost.Endpoints;
using Confifu.Abstractions;
using Confifu.Abstractions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ServiceStack.Confifu
{
    /// <summary>
    /// ServiceStack config
    /// </summary>
    public class ServiceStackConfig
    {
        public IAppConfig AppConfig { get; }
        /// <summary>
        /// AppHost factory
        /// </summary>
        public Func<IRunnableAppHost> AppHost { get; set; }

        /// <summary>
        /// EndpointHostConfig configuration action
        /// </summary>
        public Action<IAppHost, EndpointHostConfig> Config { get; set; }

        /// <summary>
        /// Default ServiceHost Name
        /// </summary>
        public string ServiceHostName { get; set; } = "ServiceStack";

        /// <summary>
        /// Assemblies with services. Used by default
        /// </summary>
        public List<Assembly> ServiceHostAssemblies { get; } = new List<Assembly>();

        public ServiceStackConfig(IAppConfig appConfig)
        {
            AppConfig = appConfig;
        }

        /// <summary>
        /// Added configuration action
        /// </summary>
        /// <param name="configAction">configuration action delegate</param>
        public void ConfigureEndpoint(Action<IAppHost, EndpointHostConfig> configAction)
        {
            if (configAction == null) throw new ArgumentNullException(nameof(configAction));

            var current = Config;
            if (current == null)
                Config = configAction;
            else
                Config = (appHost, config) =>
                {
                    current(appHost, config);
                    configAction(appHost, config);
                };
        }

        public void AddPlugin(Func<IPlugin> pluginFunc)
        {
            if (pluginFunc == null)
                throw new ArgumentNullException(nameof(pluginFunc));

            ConfigureEndpoint((appHost, config) =>
            {
                appHost.Plugins.Add(pluginFunc());
            });
        }

        public void AddPlugin<T>() where T:IPlugin
        {
            AddPlugin(() => AppConfig.GetServiceProvider().GetService<T>());
        }

        public void ServiceName(string serviceName)
        {
            ServiceHostName = serviceName;
        }

        public void HttpAppHost(string url)
        {
            AppHost = () => new AppHostHttpListener(ServiceHostName, url, ServiceHostAssemblies.ToArray());
        }

        public void WebAppHost()
        {
            AppHost = () => new AppHostWebListener(ServiceHostName, ServiceHostAssemblies.ToArray());
        }

        public void ConfigureForProduction()
        {
            ConfigureEndpoint((appHost, config) =>
            {
                var pluginsToRemove = new[]
                    {
                        typeof (global::ServiceStack.WebHost.Endpoints.Formats.HtmlFormat),
                        typeof (global::ServiceStack.WebHost.Endpoints.Formats.CsvFormat),
                        typeof (global::ServiceStack.WebHost.Endpoints.Formats.MarkdownFormat),
                        typeof (global::ServiceStack.PredefinedRoutesFeature),
                        typeof (global::ServiceStack.MetadataFeature)
                    };

                foreach (var pluginType in pluginsToRemove)
                {
                    var plugin = appHost.Plugins.FirstOrDefault(x => x.GetType() == pluginType);
                    if (plugin != null)
                        appHost.Plugins.Remove(plugin);
                }

                config.DebugMode = false;

                config.GlobalResponseHeaders.Clear();
            });
        }
    }
}