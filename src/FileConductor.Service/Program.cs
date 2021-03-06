﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FileConductor.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            // Basic service configuration
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.Service<FileConductorService>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(() => new FileConductorService());
                    serviceInstance.WhenStarted(execute => execute.Start());
                    serviceInstance.WhenStopped(execute => execute.Stop());

                });

                serviceConfig.SetServiceName("FileConductor");
                serviceConfig.SetDisplayName("File Conductor Service");
                serviceConfig.SetDescription("Service observing local/remote files and moves/processes them.");
                serviceConfig.StartAutomatically();
            });
        }
    }
}
