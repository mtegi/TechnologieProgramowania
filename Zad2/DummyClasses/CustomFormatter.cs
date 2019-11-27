using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            string resultId=null;
            Dictionary<string,object> data = new Dictionary<string, object>();
            List<SerializationInfo> info = new List<SerializationInfo>();

            // Przechodzi po pliku, tworzy nowe obiekty i puste serializationInfo. 
            // Zapisuje if pierwszego obiektu bo to to co chcemy zwrócic.   

            using (StreamReader reader = new StreamReader(serializationStream))
            {

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

                        data.Add(splitLine[2], FormatterServices.GetUninitializedObject(objType));
                        info.Add(new SerializationInfo(objType, new FormatterConverter()));

                        if(first)
                        {
                            first = false;
                            resultId = splitLine[2];
                        }
                    }
                }
                
            }

            //Przechodzi po pliku jeszcze raz, tym razem wypełniajac serializationinfo wartosciami
            // razem z referencjami do tych ustych obiektow co stworzylismy

            using (StreamReader reader = new StreamReader(serializationStream))
            {
                string line;
                int i = -1;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                    {
                        i++;
                    }

                    if (line.StartsWith("+"))
                    {
                        string[] splitLine = line.Split(':');
                        info[i].AddValue(splitLine[1], splitLine[2]);
                    }

                    if (line.StartsWith("-"))
                    {
                        string[] splitLine = line.Split(':');
                        info[i].AddValue(splitLine[1], data[splitLine[2]]);
                    }
                }

            }


            // Tutaj trzeba jakos zainicjalizowac te obiekty na podstawie info
            foreach (object item in data.Values)
            {
                Type[] constructorTypes = { typeof(SerializationInfo), Context.GetType() };

                
            }



            return data[resultId];
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

            ObjectTextForm.AppendLine("#" +":"+ graph.GetType().AssemblyQualifiedName + ":"+ this.m_idGenerator.GetId(graph, out bool firstTime));

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
                ObjectTextForm.AppendLine( "-" + ":"+ name + ":" + this.m_idGenerator.GetId(obj, out bool firstTime));
            }

        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            ObjectTextForm.AppendLine("+" + ":"+ name + ":" + val.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
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
