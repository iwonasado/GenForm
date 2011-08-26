﻿using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Tests;
using Informedica.GenForm.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.NHibernate.Tests
{
    /// <summary>
    /// Summary description for NHibernateRepositoryTests
    /// </summary>
    [TestClass]
    public class NHibernateRepositoryTests : TestSessionContext
    {
        private TestContext testContextInstance;

        public NHibernateRepositoryTests() : base(true)
        {
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
        public void ThatSubstanceCanBeAdded()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            var repos = new SubstanceRepository(GenFormApplication.SessionFactory);
            repos.Add(subst);

            Assert.IsTrue(repos.Contains(subst));
        }

        [TestMethod]
        public void ThatSubstanceHasSubstanceGroup()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            var repos = new SubstanceRepository(GenFormApplication.SessionFactory);
            repos.Add(subst);
            
            Assert.IsNotNull(subst.SubstanceGroup);
        }

        [TestMethod]
        public void ThatSubstanceGroupContainsSubstance()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            new SubstanceRepository(GenFormApplication.SessionFactory) {subst};

            Assert.IsTrue(subst.SubstanceGroup.Substances.Contains(subst));
        }

        [TestMethod]
        public void ThatSubstancCanBeRemovedFromSubstanceGroup()
        {
            var subst = Substance.Create(SubstanceTestFixtures.GetSubstanceWithGroup());
            new SubstanceRepository(GenFormApplication.SessionFactory) {subst};
            var group = subst.SubstanceGroup;
            subst.RemoveFromSubstanceGroup();

            Assert.IsFalse(group.Substances.Contains(subst));   
        }
    }
}
