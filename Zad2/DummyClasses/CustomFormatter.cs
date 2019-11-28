using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace DummyClasses
{

    public class CustomFormatter : Formatter
    {
        public override ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override SerializationBinder Binder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override StreamingContext Context { get; set; }

        StringBuilder ObjectTextForm = new StringBuilder();
        private List<string> ObjectsTextFormList = new List<string>();


        public CustomFormatter()
        {
            Context = new StreamingContext(StreamingContextStates.File);
        }

        public override object Deserialize(Stream serializationStream)
        {
            string resultId = null;
            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, SerializationInfo> info = new Dictionary<string, SerializationInfo>();

            List<Tuple<string, string, string>> referenceBuffer = new List<Tuple<string, string, string>>();

  

            using (StreamReader reader = new StreamReader(serializationStream))
            {
                string id = null;
                bool first = true;
                string line;

                while ((line = reader.ReadLine()) != null)
                {

                    if (line.StartsWith("#"))
                    {
                        string[] splitLine = line.Split(':');

                        Type objType = Type.GetType(splitLine[1]);
                        if (objType is null)
                            throw new SerializationException();

                        id = splitLine[2];
                        data.Add(id, FormatterServices.GetUninitializedObject(objType));
                        info.Add(id, new SerializationInfo(objType, new FormatterConverter()));


                        if (first)
                        {
                            first = false;
                            resultId = id;
                        }
                    }

                    if (line.StartsWith("+"))
                    {
                        string[] splitLine = line.Split(':');
                     
                            info[id].AddValue(splitLine[2], ParseMember(splitLine[1], splitLine[3]));
                    }

                    if (line.StartsWith("-"))
                    {
                        string[] splitLine = line.Split(':');

                        referenceBuffer.Add(Tuple.Create(id, splitLine[1], splitLine[2]));
                    }
                }


            }

            foreach (Tuple<string, string, string> reference in referenceBuffer)
            {
                info[reference.Item1].AddValue(reference.Item2, data[reference.Item3]);
            }

            foreach (KeyValuePair<string, object> keyValue in data)
            {
                Type[] constructorTypes = { typeof(SerializationInfo), typeof(StreamingContext) };
                object[] constuctorParameters = { info[keyValue.Key], Context };

                keyValue.Value.GetType().GetConstructor(constructorTypes).Invoke(keyValue.Value, constuctorParameters);

            }

            return data[resultId];
        }

        public override void Serialize(Stream serializationStream, object graph)
        {
            Stream stream = serializationStream;
            ISerializable serialObj;

            if (graph is ISerializable)
                serialObj = (ISerializable)graph;
            else
                throw new SerializationException();

            SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());

            serialObj.GetObjectData(info, Context);

            // "#" - object, "+" - simple type, "-" - reference

            ObjectTextForm.AppendLine("#" + ":" + graph.GetType().AssemblyQualifiedName + ":" + this.m_idGenerator.GetId(graph, out bool firstTime));

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

        private object ParseMember(string type, string value)
        {
            switch (type)
            {
                case "System.Single":
                    return Single.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                case "System.DateTime":
                    return DateTime.ParseExact(value, "MM/dd/yyyy HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture);
                case "System.String":
                    return value;
                case "null":
                    return null; 
            }

            throw new SerializationException("type: " + type + " value: " + value);
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
            ObjectTextForm.AppendLine("+" + ":" + val.GetType() + ":" + name + ":" + val.ToString("MM/dd/yyyy HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture));
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
                WriteString((String)obj, name);
            }
            else if (obj is null)
            {
                ObjectTextForm.AppendLine("+" + ":"+ "null" + ":" + name + ":" + "null");
            }
            else
            {
                this.Schedule(obj); 
                ObjectTextForm.AppendLine("-" + ":" + name + ":" + this.m_idGenerator.GetId(obj, out bool firstTime));
            }

        }

        private void WriteString(string val, string name)
        {
            ObjectTextForm.AppendLine("+" + ":" + val.GetType() + ":" + name + ":" + val);
        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            ObjectTextForm.AppendLine("+" + ":" + val.GetType() + ":" + name + ":" + val.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
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
