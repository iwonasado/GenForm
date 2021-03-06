﻿using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for SubstanceGroup
    /// </summary>
    [TestClass]
    public class SubstanceGroupConstructionTests
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
        public void ThatSubstanceGroupIsConstructedWithNameInotropica()
        {
            var group = SubstanceGroupTestFixtures.CreateSubstanceGroup();
            Assert.AreEqual(group.Name, "inotropica");
        }

        [TestMethod]
        public void ThatSubstancesCountIsZero()
        {
            var group = SubstanceGroupTestFixtures.CreateSubstanceGroup();
            Assert.AreEqual(0, group.Substances.Count());
        }

        [TestMethod]
        public void ThatSubstanceGroupWithoutNameCannotBeCreated()
        {
            try
            {
                SubstanceGroup.Create(new SubstanceGroupDto());
                Assert.Fail();
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }
    }
}