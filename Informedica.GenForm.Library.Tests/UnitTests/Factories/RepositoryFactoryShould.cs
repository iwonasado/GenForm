﻿using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Factories
{
    /// <summary>
    /// Summary description for RepositoryFactoryShould
    /// </summary>
    [TestClass]
    public class RepositoryFactoryShould : TestSessionContext
    {
        public RepositoryFactoryShould() : base(false)
        {
        }

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
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize(); }
        
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
        public void BeAbleToCreateASubstanceRepository()
        {
            var repos = RepositoryFactory.Create<Substance>();
            Assert.IsInstanceOfType(repos, typeof(SubstanceRepository));
        }
    }
}
