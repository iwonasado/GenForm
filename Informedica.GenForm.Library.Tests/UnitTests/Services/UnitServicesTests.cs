﻿using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.Services.Products;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for UnitServicesTests
    /// </summary>
    [TestClass]
    public class UnitServicesTests : TestSessionContext
    {
        public UnitServicesTests() : base(true) {}

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
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        
        #endregion

        [TestMethod]
        public void ThatServicesCanCreateNewUnitWithNewUnitGroup()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).Get();
            Assert.IsNotNull(unit);
        }

        [TestMethod]
        public void ThatServicesCanCreateNewUnitAndAddUnitToGroup()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).AddToGroup(GetGroupDto()).Get();
            Assert.IsNotNull(unit);
        }

        [TestMethod]
        public void ThatServicesGetsTheUnitFromTheRepositoryOnceItsAdded()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).AddToGroup(GetGroupDto()).Get();
            Assert.AreEqual(unit, UnitServices.GetUnit(unit.Id));
        }

        [TestMethod]
        public void ThatServicesReturnsSameUnitWhenAskedTwiceForSameUnit()
        {
            var unit1 = UnitServices.WithDto(GetUnitDto()).Get();
            var unit2 = UnitServices.WithDto(GetUnitDto()).Get();

            Assert.AreEqual(unit1, unit2);
        }

        [TestMethod]
        public void ThatServicesCanBeQueriedUsingLinq()
        {
            var result = UnitServices.Units.Where(x => x.UnitGroup.Name == "massa");
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void ThatServicesCanDeleteUnit()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).Get();
            UnitServices.Delete(unit);
            Assert.IsNull(UnitServices.Units.SingleOrDefault(x => x.Name == GetUnitDto().Name));
        }

        [TestMethod]
        public void ThatUnitGroupIsStillThereAfterUnitDelete()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).Get();
            UnitServices.Delete(unit);
            Assert.IsNotNull(UnitGroupServices.UnitGroups.SingleOrDefault(x => x.Name == GetUnitDto().UnitGroupName));
        }
        
        [TestMethod]
        public void ThatAUnitGroupCanBeChanged()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).Get();
            // ToDo: rewrite
            UnitServices.ChangeUnitName(unit, "changed");
            Assert.IsNotNull(UnitServices.Units.SingleOrDefault(x => x.Name == "changed"));
        }

        private UnitGroupDto GetGroupDto()
        {
            return new UnitGroupDto
                       {
                           AllowConversion = true,
                           Name = "massa"
                       };
        }

        private UnitDto GetUnitDto()
        {
            return new UnitDto
                       {
                           Abbreviation = "mg",
                           AllowConversion = true,
                           Multiplier = 0.001M,
                           Divisor = 1,
                           IsReference = false,
                           Name = "milligram",
                           UnitGroupName = "massa"
                       };
        }
    }
}
