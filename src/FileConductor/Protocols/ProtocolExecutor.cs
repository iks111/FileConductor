﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileConductor.FileTransport;
using NLog;

namespace FileConductor.Protocols
{
    public class ProtocolExecutor
    {

        protected static Logger logger = LogManager.GetCurrentClassLogger();

        private OperationProperties _properties { get; set; }
        private readonly Protocol _protocol;


        public ProtocolExecutor(Protocol protocol ,OperationProperties properties, ElapsedEventHandler afterOperationElapsedHandler)
        {
            _protocol = protocol;
            _properties = properties;
            _properties.AssignOperationHandler(afterOperationElapsedHandler);
        }

        public void ExecuteProtocol()
        {
            try
            {
                var receivedFiles = _protocol.Receiver.Receive(_properties.SourceTarget,_properties.Regex);
                _protocol.Sender.Send(_properties.DestinationTarget, receivedFiles, _properties.Regex);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

    }
}