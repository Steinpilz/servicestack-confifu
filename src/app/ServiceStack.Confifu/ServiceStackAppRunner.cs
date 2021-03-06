﻿using System;

namespace ServiceStack.Confifu
{
    /// <summary>
    /// Class to Run ServiceStack config
    /// </summary>
    public class ServiceStackAppRunner
    {
        private readonly ServiceStackConfig _config;


        /// <summary>
        /// Create new instance based on <para>config</para>
        /// </summary>
        /// <param name="config"></param>
        public ServiceStackAppRunner(ServiceStackConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            _config = config;
        }

        /// <summary>
        /// Run config
        /// </summary>
        public void Run()
        {
            ValidateConfig();

            if (_config.AppHost == null)
                return;

            var appHost = _config.AppHost();

            appHost.Run(endpointConfig =>
            {
                _config.Config?.Invoke(appHost, endpointConfig);
            });
        }

        private void ValidateConfig()
        {

        }
    }
}