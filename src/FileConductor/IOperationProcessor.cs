﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Operation;

namespace FileConductor
{
    public interface IOperationProcessor
    {
        void AssignOperation(IOperation operation);
    }
}