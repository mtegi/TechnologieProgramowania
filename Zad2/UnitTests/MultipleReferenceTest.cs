using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class MultipleReferenceTest
    {

        [TestMethod]
        public void DummyClassesTest()
        {
            DummyClass a = new DummyClass
            {
                Id = 1
            };

            DummyClass b = new DummyClass
            {
                Id = 2
            };

            DummyClass c = new DummyClass
            {
                Id = 3
            };

            a.Other = b;
            b.Other = c;
            c.Other = a;
            DummySerializer serializer = new DummySerializer();
            List<DummyClass> objects = new List<DummyClass>() { a, b, c };

            Assert.AreEqual(1, a.Id);
            Assert.AreEqual(2, b.Id);
            Assert.AreEqual(3, c.Id);
            Assert.ReferenceEquals(b, a.Other);
            Assert.ReferenceEquals(c, b.Other);
            Assert.ReferenceEquals(a, c.Other);


            using (FileStream stream = File.Open("dummies.txt", FileMode.OpenOrCreate))
            {
                serializer.Serialize(objects, stream);
            }
            List<DummyClass> deserializedDummies;
            using (FileStream stream = File.Open("dummies.txt", FileMode.Open))
            {
                deserializedDummies = serializer.Deserialize(stream);
            }

            Assert.AreEqual(1, deserializedDummies[0].Id);
            Assert.AreEqual(2, deserializedDummies[1].Id);
            Assert.AreEqual(3, deserializedDummies[2].Id);
            Assert.ReferenceEquals(deserializedDummies[1], deserializedDummies[0].Other);
            Assert.ReferenceEquals(deserializedDummies[2], deserializedDummies[1].Other);
            Assert.ReferenceEquals(deserializedDummies[0], deserializedDummies[2].Other);
            foreach(DummyClass d in deserializedDummies)
            {
                Assert.AreNotEqual(null, d.Other);
            }
        }

        [TestMethod]
        public void ReferenceTestOther()
        {


            DummyClass a = new DummyClass
            {
                Id = 1
            };

            DummyClass b = new DummyClass
            {
                Id = 2
            };

            DummyClass c = new DummyClass
            {
                Id = 3
            };

            DummyClass d = new DummyClass
            {
                Id = 4
            };

            DummyClass e = new DummyClass
            {
                Id = 5
            };


            a.Other = b;
            b.Other = c;
            c.Other = d;
            d.Other = c;
            e.Other = b;


            DummySerializer serializer = new DummySerializer();
            List<DummyClass> objects = new List<DummyClass>() { a, b, c, d, e };

            Assert.AreEqual(1, a.Id);
            Assert.AreEqual(2, b.Id);
            Assert.AreEqual(3, c.Id);
            Assert.AreEqual(4, d.Id);
            Assert.AreEqual(5, e.Id);
            Assert.ReferenceEquals(b, a.Other);
            Assert.ReferenceEquals(c, b.Other);
            Assert.ReferenceEquals(d, c.Other);
            Assert.ReferenceEquals(c, d.Other);
            Assert.ReferenceEquals(b, e.Other);

            using (FileStream stream = File.Open("dummies.txt", FileMode.OpenOrCreate))
            {
                serializer.Serialize(objects, stream);
            }
            List<DummyClass> deserializedDummies;
            using (FileStream stream = File.Open("dummies.txt", FileMode.Open))
            {
                deserializedDummies = serializer.Deserialize(stream);
            }


            Assert.AreEqual(1, deserializedDummies[0].Id);
            Assert.AreEqual(2, deserializedDummies[1].Id);
            Assert.AreEqual(3, deserializedDummies[2].Id);
            Assert.AreEqual(4, deserializedDummies[3].Id);
            Assert.AreEqual(5, deserializedDummies[4].Id);

            Assert.ReferenceEquals(deserializedDummies[1], deserializedDummies[0].Other);
            Assert.ReferenceEquals(deserializedDummies[2], deserializedDummies[1].Other);
            Assert.ReferenceEquals(deserializedDummies[3], deserializedDummies[2].Other);
            Assert.ReferenceEquals(deserializedDummies[2], deserializedDummies[3].Other);
            Assert.ReferenceEquals(deserializedDummies[1], deserializedDummies[4].Other);

            foreach (DummyClass dummy in deserializedDummies)
            {
                Assert.AreNotEqual(null, dummy.Other);
            }
        }

    }
}
