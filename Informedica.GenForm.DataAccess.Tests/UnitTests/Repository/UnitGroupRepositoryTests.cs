﻿using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Repository
{
    /// <summary>
    /// Summary description for UnitGroupRepositoryTests
    /// </summary>
    [TestClass]
    public class UnitGroupRepositoryTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ThatTheSameUnitGroupCannotBeAddedTwice()
        {
            var group1 = UnitGroup.Create(new UnitGroupDto
            {
                AllowConversion = true,
                Name = "massa"
            });
            var group2 = UnitGroup.Create(new UnitGroupDto
            {
                AllowConversion = true,
                Name = "massa"
            });
            using (GetContext())
            {
                try
                {
                    new UnitGroupRepository(GenFormApplication.SessionFactory) { group1, group2 };
                    Assert.Fail("repository should not accept same item twice");
                }
                catch (Exception)
                {
                    Assert.IsTrue(true);
                }
            }
        }


        private IDisposable GetContext()
        {
            return ObjectFactory.GetInstance<SessionContext>();
        }
    }
}
