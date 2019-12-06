using DummyClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class FormatterTest
    {
        [TestMethod]
        public void SerializationTest()
        {
            DummyClassA a = new DummyClassA();
            a.Id = 1;

            DummyClassB b = new DummyClassB();
            b.Id = 2;

            DummyClassC c = new DummyClassC();
            c.Id = 3;

            a.Other = b;
            b.Other = c;
            c.Other = a;


            b.Text = "HELLO";
            c.Time = new DateTime(2020, 1, 1);

            CustomFormatter formatter= new CustomFormatter();

            using (FileStream stream = File.Open("format_dummies.txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, a);
            }

            DummyClassA a2;
            DummyClassB b2;

            using (FileStream stream = File.Open("format_dummies.txt", FileMode.Open))
            {
               a2 = (DummyClassA) formatter.Deserialize(stream);

            }

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
            Assert.AreEqual(10f,a2.Other.Id);
        }

     

    }

}
