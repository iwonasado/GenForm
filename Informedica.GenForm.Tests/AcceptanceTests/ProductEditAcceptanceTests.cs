﻿using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.IoC.Factory;
using Informedica.GenForm.Library.Services;
using Informedica.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Tests.AcceptanceTests
{
    /// <summary>
    /// Summary description for ProductEditAcceptanceTests
    /// </summary>
    [TestClass]
    public class ProductEditAcceptanceTests
    {
        public ProductEditAcceptanceTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            ProductAssembler.RegisterDependencies();
        }
        
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void User_can_start_with_a_new_empty_product()
        {
            var services = GetProductServices();
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(services.GetEmptyProduct()), "User could not start with empty product");
        }

        private static IProductServices GetProductServices()
        {
            return LibraryObjectFactory.GetImplementationFor<IProductServices>();
        }

        [TestMethod]
        public void User_can_change_fields_of_empty_product()
        {
            const string testName = "Test";
            var product = GetProductServices().GetEmptyProduct();
            product.ProductName = testName;
            Assert.AreEqual(testName, product.ProductName,"User could not change the fields of an empty product");
        }


        [TestMethod]
        public void User_can_see_if_entered_values_are_valid()
        {
            Assert.Fail("User could not see whether entered values were valid");
        }

        [TestMethod]
        public void User_can_save_product_with_values_entered()
        {
            var product = GetProductServices().GetEmptyProduct();
            product.ProductName = "paracetamol 500 mg tablet";
            product.GenericName = "paracetamol";
            product.ShapeName = "tablet";
            product.Quantity = 1;
            product.UnitName = "stuk";
            product.PackageName = "tablet";

            try
            {
                GetProductServices().SaveProduct(product);
            }
            catch (Exception)
            {
                Assert.Fail("product with values entered (valid product) could not be saved");
                throw;
            }
        }

        [TestMethod]
        public void When_product_is_save_the_saved_version_of_product_is_returned()
        {
            Assert.Fail("after save, no saved version of the product was returned");
        }
    }
}