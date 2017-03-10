using ServiceStack.WebHost.Endpoints;

namespace ServiceStack.Confifu
{
    internal class Default
    {
        internal static void ConfigureAppHost(global::Confifu.Abstractions.IAppConfig appConfig,
            EndpointHostConfig config)
        {
            // constainer is here
            var container = config.ServiceManager.Container;
            container.Adapter = new ContainerAdapter(appConfig);
        }
    }
}