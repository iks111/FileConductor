﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.FileTransport;
using FileConductor.FileTransport.Implementations;
using FileConductor.Protocols;

namespace FileConductor.Helpers
{
    public class TransferFactory
    {
        public static ITransfer GetTransfer(TransferType type)
        {
            switch (type)
            {
                case TransferType.Local:
                    return new LocalTransfer();
                default:
                    throw new Exception("No protocol with specified type!");
            }

        }
    }
}