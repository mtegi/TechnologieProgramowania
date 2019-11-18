using DummyClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class MultipleReferenceTest
    {

        [TestMethod]
        public void DummyClassesTest()
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


            
        }
    }
}
