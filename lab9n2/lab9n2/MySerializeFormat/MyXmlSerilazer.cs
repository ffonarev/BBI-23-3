﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab_9.Serializers
{
    internal class MyXmlSerilazer : MySerializer
    {
        public override void Write<T>(T obj, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                new XmlSerializer(typeof(T)).Serialize(fs, obj);
            }
        }
        public override T Read<T>(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
               return (T)new XmlSerializer(typeof(T)).Deserialize(fs);
            }
        }

    }
}
