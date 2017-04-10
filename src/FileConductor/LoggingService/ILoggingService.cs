﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConductor.LoggingService
{
    public interface  ILoggingService
    {
        void LogInfo(Operation operation,string message);
        void LogException(Exception exception, Operation operation, string message);

    }
}
