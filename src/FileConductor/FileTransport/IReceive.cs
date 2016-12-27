﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.FileTransport
{
    public interface IReceive
    {
        void Receive(string ip, string path, string fileName);
    }
}