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
            DummyClass a = new DummyClass();
            a.Id = 1;

            DummyClass b = new DummyClass();
            b.Id = 2;

            DummyClass c = new DummyClass();
            c.Id = 3;

            a.Other = b;
            b.Other = c;
            c.Other = a;

            CustomFormatter formatter= new CustomFormatter();

     


            using (FileStream stream = File.Open("format_dummies.txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, a);
            }

            Type objtype = Type.GetType("DummyClasses.DummyClass, DummyClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            Assert.IsNotNull(objtype);
        }

        [TestMethod]
        public void ReferencjeTest()
        {
            DummyClass a = new DummyClass();

            DummyClass b = new DummyClass();

            b.Other = a;

            a = new DummyClass();
            a.Id = 5;


            Assert.AreEqual(5, b.Other.Id);
        }
    }


}
