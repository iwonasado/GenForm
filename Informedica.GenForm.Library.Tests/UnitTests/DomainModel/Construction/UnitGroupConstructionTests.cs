﻿using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for UnitGroupWithUnitsShould
    /// </summary>
    [TestClass]
    public class UnitGroupConstructionTests
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
        //[TestInitialize]
        //public void MyTestInitialize()
        //{
        //    _unitGroup = CreateNewUnitGroup();
        //}

        //private UnitGroup CreateNewUnitGroup()
        //{
        //    return UnitGroupFactory.CreateUnitGroup(new UnitGroupDto
        //            {
        //                AllowConversion = true,
        //                Environment = "massa"
        //            });
        //}

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ThatUnitGroupHasNameVolume()
        {
            var group = UnitGroupTestFixtures.CreateUnitGroupVolume();
            Assert.AreEqual("volume", group.Name);
        }

        [TestMethod]
        public void ThatUnitGroupVolumeAllowsConversion()
        {
            var group = UnitGroupTestFixtures.CreateUnitGroupVolume();
            Assert.IsTrue(group.AllowsConversion);
        }

        [TestMethod]
        public void ThatNewUnitGroupHasZeroUnits()
        {
            var group = UnitGroupTestFixtures.CreateUnitGroupVolume();
            Assert.AreEqual(0, group.Units.Count());
        }

        [TestMethod]
        public void ThatNewUnitGroupIsAssociatedWithZeroShapes()
        {
            var group = UnitGroupTestFixtures.CreateUnitGroupVolume();
            Assert.AreEqual(0, group.Shapes.Count());
        }

        [TestMethod]
        public void ThatConstrucionWithEmptyNameThrowsException()
        {
            try
            {
                UnitGroup.Create(new UnitGroupDto());
                Assert.Fail();
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }
        
    }
}
