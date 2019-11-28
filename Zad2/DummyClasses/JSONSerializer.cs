using System;
using System.IO;
using DummyClasses;
using Newtonsoft.Json;

namespace DummyClasses
{
    public class JSONSerializer
    {
        private readonly JsonSerializerSettings settings;

        public JSONSerializer()
        {
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };
        }

        public void Serialize(string filename, Object graph)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(graph, Formatting.Indented, settings));
        }

        public  T Deserialize<T>(string filename)
        {
            string json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

    }
}
