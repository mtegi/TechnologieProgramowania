using DummyClasses;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class JSONTest
    {
        
        [TestMethod]
        public void DeserializeJSONDummiesTest()
        {
            DummyClassA a = new DummyClassA
            {
                Id = 1
            };

            DummyClassB b = new DummyClassB
            {
                Id = 2
            };

            DummyClassC c = new DummyClassC
            {
                Id = 3
            };

            a.Other = b;
            b.Other = c;
            c.Other = a;



            b.Text = "HELLO";
            c.Time = new DateTime(2020, 1, 1);



            DummyClassA a2;
            DummyClassB b2;

            JSONSerializer serializer = new JSONSerializer();
            serializer.Serialize("test.json", a);
            a2 = serializer.Deserialize<DummyClassA>("test.json");

            Assert.AreEqual(1, a2.Id);
            Assert.AreEqual(2, a2.Other.Id);
            Assert.AreEqual(3, a2.Other.Other.Id);
            Assert.AreEqual(1, a2.Other.Other.Other.Id);

            Assert.AreEqual(typeof(DummyClassA), a2.GetType());
            Assert.AreEqual(typeof(DummyClassB), a2.Other.GetType());
            Assert.AreEqual(typeof(DummyClassC), a2.Other.Other.GetType());
            Assert.AreEqual(typeof(DummyClassA), a2.Other.Other.Other.GetType());

            Assert.AreEqual("HELLO", a2.Other.Text);
            Assert.AreEqual(2020, a2.Other.Other.Time.Year);
            Assert.AreEqual(1, a2.Other.Other.Time.Month);
            Assert.AreEqual(1, a2.Other.Other.Time.Day);

            b2 = a2.Other;
            Assert.ReferenceEquals(a2.Other, b2);
            b2.Id = 10f;
            Assert.AreEqual(10f, a2.Other.Id);
        }

        }
}
