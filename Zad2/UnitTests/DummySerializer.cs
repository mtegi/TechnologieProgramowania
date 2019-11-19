using DummyClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace UnitTests
{
    class DummySerializer
    {
        public void Serialize(List<DummyClass> dummies, Stream stream)
        {
            ObjectIDGenerator idGenerator = new ObjectIDGenerator();

            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (DummyClass dummy in dummies)
                {
                    long dummyId = idGenerator.GetId(dummy, out bool firstTime);
                    long dummyOtherId = idGenerator.GetId(dummy.Other, out firstTime);
                    StringBuilder builder = new StringBuilder();
                    builder.Append(dummyId);
                    builder.Append(';');
                    builder.Append(dummyOtherId);
                    builder.Append(';');
                    builder.Append(dummy.Id);
                    writer.WriteLine(builder.ToString());
                }
                writer.WriteLine('#');
            }
        }

        public List<DummyClass> Deserialize(FileStream stream)
        {
            Dictionary<string, DummyClass> tmpDummies = new Dictionary<string, DummyClass>();
            Dictionary<DummyClass, string> dummiesWithoutReference = new Dictionary<DummyClass, string>();
            List<DummyClass> ret = new List<DummyClass>();
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                while (!(line = reader.ReadLine()).StartsWith("#"))
                {
                    string[] dummyProps = line.Split(';');
                    string objectId = dummyProps[0];
                    string other = dummyProps[1];
                    string id = dummyProps[2];
                    if (tmpDummies.ContainsKey(objectId))
                    {
                        DummyClass deserializedDummy = tmpDummies[objectId];
                        ret.Add(deserializedDummy);
                    }
                    else
                    {
                        DummyClass deserializedDummy = new DummyClass();
                        deserializedDummy.Id = int.Parse(dummyProps[2]);
                        if (tmpDummies.ContainsKey(other))
                        {
                            deserializedDummy.Other = tmpDummies[other];
                        }
                        else dummiesWithoutReference.Add(deserializedDummy, other);
                        tmpDummies.Add(objectId, deserializedDummy);
                        ret.Add(deserializedDummy);
                    
                    }
                }
            }
            foreach(DummyClass dummy in ret)
            {
                if (dummy.Other == null)
                    dummy.Other = tmpDummies[dummiesWithoutReference[dummy]];
            }
                    return ret;
        }
    }
}
       