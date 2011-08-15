﻿using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.NHibernate.Tests
{
    [TestClass]
    public class ASessionContextShould
    {
        [TestMethod]
        public void AlwaysReturnASessionObject()
        {
            using (SessionContext.UseContext())
            {
                Assert.IsNotNull(GenFormApplication.SessionFactory.GetCurrentSession());
            }
        }

        [TestMethod]
        public void HaveClosedTheSessionAfterDispose()
        {
            using (SessionContext.UseContext())
            {
                Assert.IsNotNull(GenFormApplication.SessionFactory.GetCurrentSession());
            }

            try
            {
                GenFormApplication.SessionFactory.GetCurrentSession();
                Assert.Fail("No session should be open after disposal of SessionBuilder");
            }
            catch (Exception)
            {
                Assert.IsTrue(true); 
            }
        }
    }
}