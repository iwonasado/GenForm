﻿using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.TestFixtures.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel.Construction
{
    /// <summary>
    /// Summary description for RouteConstructionTests
    /// </summary>
    [TestClass]
    public class RouteConstructionTests
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
        public void ThatAValidRouteCanBeConstructed()
        {
            var route = RouteTestFixtures.CreateRouteIv();
            Assert.IsTrue(RouteIsValid(route));
        }

        [TestMethod]
        public void ThatRouteWithoutNameThrowsException()
        {
            var dto = RouteTestFixtures.GetRouteIvDto();
            dto.Name = String.Empty;

            AssertCreateFails(dto);
        }


        [TestMethod]
        public void ThatRouteWithoutAbbreviationThrowsException()
        {
            var dto = RouteTestFixtures.GetRouteIvDto();
            dto.Abbreviation = String.Empty;

            AssertCreateFails(dto);
        }

        private static void AssertCreateFails(RouteDto dto)
        {
            try
            {
                Route.Create(dto);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof (AssertFailedException));
            }
        }

        private static bool RouteIsValid(Route route)
        {
            return !String.IsNullOrWhiteSpace(route.Name) && !String.IsNullOrWhiteSpace(route.Abbreviation);
        }
    }
}
