﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.ConfigurationTool.ViewModels;
using FileConductor.UI.Annotations;

namespace FileConductorUI.UI.Entities
{
    public class ConfigurationDataCollection : INotifyPropertyChanged
    {
        private ConfigurationData _configData;
        public ConfigurationData ConfigData

        {
            get { return _configData; }
            set
            {
                _configData = value;
                SelectedTarget = ConfigData.Targets.FirstOrDefault();
                SelectedDatabase = ConfigData.Databases.FirstOrDefault();
                SelectedSchedule = ConfigData.Schedules.FirstOrDefault();
                OnPropertyChanged("ConfigurationData");
            }
        }

        public ConfigurationDataCollection()
        {
            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();
            ConfigData = deserializer.XmlData;
        }


        public TargetData SelectedTarget { get; set; }
        public DatabaseData SelectedDatabase { get; set; }
        public ScheduleData SelectedSchedule { get; set; }
 
        public CommandHandler AddCommand { get; set; }
        public CommandHandler EditCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}