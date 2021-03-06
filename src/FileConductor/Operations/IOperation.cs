﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileConductor.Protocols;

namespace FileConductor.Operations
{

    public delegate void OperationElapsedEventHandler(IOperation sender, ElapsedEventArgs e);
    public interface IOperation
    {
        IProtocol Protocol { get; set; }
        string Code { get; set; }
        event OperationElapsedEventHandler OnOperationReady;
        OperationProperties Properties { get; set; }
        int Id { get; set; }
   
    }
}
