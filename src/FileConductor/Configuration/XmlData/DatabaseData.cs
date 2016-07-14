﻿using System;
using System.Xml.Serialization;

namespace FileConductor.Configuration.XmlData
{
    [Serializable]
    public class DatabaseData
    {

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("host")]
        public string Host { get; set; }

        [XmlElement("user")]
        public string User { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }
    }
}