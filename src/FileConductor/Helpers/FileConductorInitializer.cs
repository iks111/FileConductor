﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.Protocols;

namespace FileConductor.Helpers
{
    public class FileConductorInitializer
    {
        public void InitializeOperations()
        {
            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();
            var configurationData = deserializer.XmlData;

            var operationProcessor = new OperationProcessor();

            foreach (var watcher in configurationData.Watchers)
            {
                var shedule = configurationData.Schedules.First(x => x.Id == watcher.ScheduleId);
                var database = configurationData.Databases.First(x => x.Id == watcher.DatabaseId);
                var sourceTarget = configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.SourceTargetId);
                var sourceServer = configurationData.Servers.First(x => x.Id == sourceTarget.ServerId);
                var destinationTarget =
                    configurationData.Targets.First(x => x.Id == watcher.WatcherRouting.DestinationTargetId);
                var destinationServer = configurationData.Servers.First(x => x.Id == destinationTarget.ServerId);
                int operationId = watcher.Id;

                var days = GetDaysArray(shedule);
                DateTime time;
                DateTime.TryParseExact(shedule.Hours, "HHmm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out time);

                var operationProperties = FillOperationsProperties(destinationTarget, destinationServer, days, time, sourceTarget, sourceServer, watcher);



                operationProcessor.AssignOperation(new Operation(ProtocolFactory.GetProtocol(destinationServer.Protocol), operationProperties,operationId));
            }
       
           
        }

        private static int[] GetDaysArray(ScheduleData shedule)
        {
            var days = shedule.DaysOfWeek.Split(Constants.DaysSeparator)
                .ToList()
                .ConvertAll(Convert.ToInt32)
                .ToArray();
            return days;
        }

        private static OperationProperties FillOperationsProperties(TargetData destinationTarget, ServerData destinationServer, int[] days, DateTime time,
            TargetData sourceTarget, ServerData sourceServer, WatcherData watcher)
        {
            var operationProperties = new OperationProperties()
            {
                DestinationServer = destinationServer,
                DestinationTarget = destinationTarget,
                NotificationSettings =
                    new SpecifiedTimeNotification(days, new TimeSpan(0, time.Hour, time.Minute, 0)),
                SourceServer = sourceServer,
                SourceTarget = sourceTarget,
                Regex = watcher.FileNameRegex
            };
            return operationProperties;
        }
    }
}