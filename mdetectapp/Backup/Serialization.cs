using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



namespace MotionDetector
{


    [Serializable]
    public abstract class SerializableXml<T>
    {


        public void Save(string filename)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, this);
            writer.Close();

            
        }


        public static T Load(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            TextReader reader = new StreamReader(filename);
            T obj = (T)serializer.Deserialize(reader);
            reader.Close();

            return obj;
        }


    }


    public class BinarySerializer
    {

        public static void Serialize(string filename, object obj)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, obj);
            stream.Close();
        }

        public static object Deserialize(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(stream);
            stream.Close();
            return obj;
        }

    }


}
