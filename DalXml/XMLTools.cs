using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace Dal
{
    static class XMLTools
    {
        const string s_dir = @"..\xml\";
        static XMLTools()
        {
            if (!Directory.Exists(s_dir))
                Directory.CreateDirectory(s_dir);
        }
        public static string GetDir()
        {
            return  s_dir;
        }
        public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
        {
            string filePath = $"{s_dir + entity}.xml";
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T?>));
                using (FileStream stream = new FileStream(filePath, FileMode.Create,FileAccess.Write,FileShare.None))
                {
                    serializer.Serialize(stream, list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while serializing the list: " + ex.Message);
            }
        }
        public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T : struct
        {
            string filePath = $"{s_dir + entity}.xml";
            try
            {
                if (!File.Exists(filePath)) return new();
                XmlSerializer serializer = new XmlSerializer(typeof(List<T?>));
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    return serializer.Deserialize(stream) as List<T?> ?? new();
                }
            }
            catch (Exception ex)
            {
               throw new Exception("An error occurred while deserializing the file: " + ex.Message);
            }           
        }
    }
}
