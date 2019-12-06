using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DummyClasses
{
    public class DummyClassA:ISerializable
    {
        public DummyClassB Other { get; set; }
        public float Id { get; set; }

        public DummyClassA(DummyClassB B, float Id)
        {
            this.Id = Id;
            this.Other = B;
        }

        public DummyClassA() { }
        public DummyClassA(SerializationInfo info,StreamingContext context)
        {
            Id = info.GetSingle(nameof(Id));
            Other = info.GetValue(nameof(Other), typeof(DummyClassB)) as DummyClassB;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), this.Id, typeof(float));
            info.AddValue(nameof(Other), this.Other, typeof(DummyClassB));
        }
    }
}
