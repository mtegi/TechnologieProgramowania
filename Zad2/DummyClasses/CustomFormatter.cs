using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DummyClasses
{
    
   public class CustomFormatter : Formatter
    { 
        public override ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override SerializationBinder Binder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override StreamingContext Context { get; set; }

        StringBuilder ObjectTextForm= new StringBuilder();
        private List<string> ObjectsTextFormList = new List<string>();


        public override object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }
        public override void Serialize(Stream serializationStream, object graph)
        {
            Stream stream = serializationStream;
            ISerializable serialObj;
            
            if (graph is ISerializable)
                serialObj = (ISerializable) graph;
            else
                throw new SerializationException();

            SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());
            Context = new StreamingContext(StreamingContextStates.File);
            serialObj.GetObjectData(info, Context);

            // "#" - object, "+" - simple type, "-" - reference

            ObjectTextForm.AppendLine("#" + graph.GetType() + ":" + this.m_idGenerator.GetId(graph, out bool firstTime));

            foreach (SerializationEntry item in info)
            {
                WriteMember(item.Name, item.Value);
            }

            ObjectsTextFormList.Add(ObjectTextForm.ToString());
            ObjectTextForm = new StringBuilder();

            while (this.m_objectQueue.Count != 0)
            {
                this.Serialize(null, this.m_objectQueue.Dequeue());
            }



            StreamWrite(stream);
        }

        private void StreamWrite(Stream serializationStream)
        {
            if (serializationStream != null)
            {
                using (StreamWriter writer = new StreamWriter(serializationStream))
                {
                    foreach (string line in ObjectsTextFormList)
                        writer.Write(line);
                }
            }
        }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteBoolean(bool val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteByte(byte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteChar(char val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDouble(double val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt32(int val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt64(long val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {

            if (obj is string)
            {
                //todo: serializacja string
            }
            else
            {
                this.Schedule(obj); //funkcja formattera, sama sprawdza czy obiekt juz jest
                ObjectTextForm.AppendLine( "-" + name + ":" + this.m_idGenerator.GetId(obj, out bool firstTime));
            }

        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            ObjectTextForm.AppendLine("+" + name + ":" + val.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
        }

        protected override void WriteTimeSpan(TimeSpan val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt16(ushort val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt32(uint val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt64(ulong val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteValueType(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }
    }
}
