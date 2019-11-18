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
    }
}
       