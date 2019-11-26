using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DummyClasses
{
    [Serializable]
    public class DummyClass:ISerializable
    {
        public DummyClass Other { get; set; }
        public float Id { get; set; }

        public DummyClass(SerializationInfo info,StreamingContext context)
        {
            Id = info.GetSingle(nameof(Id));
            Other = info.GetValue(nameof(Other), typeof(DummyClass)) as DummyClass;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), this.Id, typeof(float));
            info.AddValue(nameof(Other), this.Other, typeof(DummyClass));
        }
    }
}
