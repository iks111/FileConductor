﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FileConductor.Presentation
{
    public class MainWindowsViewModel
    {
        public string Status { get; set; }

        public MainWindowsViewModel()
        {
            TopshelfExitCode exitCode = TopshelfExitCode.AbnormalExit;

            Task.Factory.StartNew(() =>
            {

                var service = HostFactory.Run(serviceConfig =>
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

                exitCode = service;
            });

            Status = exitCode.ToString();


        }

    }
}
