using System;
using System.IO;
using Library;
using Newtonsoft.Json;

namespace Library
{
    public class JSONSerializer
    {
        private readonly JsonSerializerSettings settings;

        public JSONSerializer()
        {
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            };
        }

        public void Serialize(string filename, DataContext context)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(context, Formatting.Indented, settings));
        }

        public  DataContext Deserialize(string filename)
        {
            string json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<DataContext>(json, settings);
        }
    }
}
