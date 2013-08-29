using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace PipeExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            TestClass testObject = new TestClass();

            try
            {
                Boolean isKeyAvailable = Console.KeyAvailable;
                testObject.text = args[0];
                
                Console.WriteLine(SerializeToString(testObject));
            }
            catch (InvalidOperationException)
            {
                StringBuilder input = new StringBuilder();
                string s = null;

                while ((s = Console.ReadLine()) != null)
                {
                    input.Append(s);
                }
                
                testObject = DeserializeFromString<TestClass>(input);
                testObject.text += " I'm back!";
                Console.WriteLine(SerializeToString(testObject));
            }
        }

        static protected string SerializeToString<T>(T objToSerialize)
        {
            XmlSerializer serializer = new XmlSerializer(objToSerialize.GetType());
            StringBuilder output = new StringBuilder();
            StringWriter writer = new StringWriter(output);

            serializer.Serialize(writer, objToSerialize);
            return output.ToString();
        }

        static protected T DeserializeFromString<T>(StringBuilder stringToDeserialize)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            object deserialized = null;

            using (StringReader reader = new StringReader(stringToDeserialize.ToString()))
            {
                deserialized = serializer.Deserialize(reader);
            }
            return (T)deserialized;
        }
    }
}
