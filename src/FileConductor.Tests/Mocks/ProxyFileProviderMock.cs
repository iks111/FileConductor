﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Helpers;
using FileConductor.ProxyFile;

namespace FileConductor.Tests.Mocks
{
    public class ProxyFileProviderMock : IProxyFileProvider
    {
        public string ProxyPath { get; set; }
    }
}