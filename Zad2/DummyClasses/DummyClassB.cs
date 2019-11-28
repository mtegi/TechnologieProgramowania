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
    public class DummyClassB:ISerializable
    {
        public DummyClassC Other { get; set; }
        public float Id { get; set; }
        public string Text { get; set; }

        public DummyClassB(DummyClassC C,float Id, string text)
        {
            this.Id = Id;
            this.Text = Text;
            this.Other = C;
        }
        public DummyClassB() { }
        public DummyClassB(SerializationInfo info,StreamingContext context)
        {
            Id = info.GetSingle(nameof(Id));
            Other = info.GetValue(nameof(Other), typeof(DummyClassC)) as DummyClassC;
            Text = info.GetValue(nameof(Text), typeof(string)) as string;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), this.Id, typeof(float));
            info.AddValue(nameof(Other), this.Other, typeof(DummyClassC));
            info.AddValue(nameof(Text), this.Text, typeof(string));
        }
    }
}
