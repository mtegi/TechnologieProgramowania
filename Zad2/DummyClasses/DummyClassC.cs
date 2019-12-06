using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DummyClasses
{
    [Serializable]
    public class DummyClassC:ISerializable
    {
        public DummyClassA Other { get; set; }
        public float Id { get; set; }
        public DateTime Time { get; set; }

        public DummyClassC(DummyClassA A, float Id, DateTime dateTime)
        {
            this.Id = Id;
            this.Time = dateTime;
            this.Other = A;
        }
        public DummyClassC() { }
        public DummyClassC(SerializationInfo info,StreamingContext context)
        {
            Id = info.GetSingle(nameof(Id));
            Other = info.GetValue(nameof(Other), typeof(DummyClassA)) as DummyClassA;
            Time = (DateTime) info.GetValue(nameof(Time), typeof(DateTime));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), this.Id, typeof(float));
            info.AddValue(nameof(Other), this.Other, typeof(DummyClassA));
            info.AddValue(nameof(Time), this.Time, typeof(DateTime));
        }
    }
}
