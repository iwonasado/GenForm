﻿using System;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services.Interfaces;
using Informedica.GenForm.Library.Services.Users;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.GenForm.PresentationLayer.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for LoginControllerTest and is intended
    ///to contain all LoginControllerTest Unit Tests
    ///</summary>
    [TestClass]
    public class LoginControllerShould
    {
        private const String ValidUser = "Admin";
        private const String ValidPassword = "Admin";
        private const String TempPassword = "temp";
        private const String InvalidUser = "foo";
        private const String InvalidPassword = "bar";
        private const String SuccessProperty = "success";

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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        private static bool GetSuccessValueFromActionResult(ActionResult response)
        {
            return ActionResultParser.GetSuccessValue(response);
        }

        [Isolated]
        [TestMethod]
        public void ReturnFalseForInvalidUserLogin()
        {
            var controller = new LoginController();
            IsolateLoginController(GetUser(), GetLoginServices());

            var response = controller.Login(InvalidUser, InvalidPassword);

            Assert.IsFalse(GetSuccessValueFromActionResult(response));
        }

        private static void IsolateLoginController(ILoginCriteria user, ILoginServices services)
        {
            Isolate.WhenCalled(() => LoginUser.NewLoginUser(InvalidUser, InvalidPassword)).WillReturn(user);
            Isolate.WhenCalled(() => LoginServices.NewLoginServices()).WillReturn(services);
        }

        [Isolated]
        [TestMethod]
        public  void ReturnSuccessValueIsTrueWhenValidUserLogin()
        {
            // Setup
            var user = GetUser();
            var loginServices = GetLoginServices();
            IsolateLoginController(user, loginServices);

            Isolate.WhenCalled(() => loginServices.Login(user)).IgnoreCall();
            Isolate.WhenCalled(() => loginServices.IsLoggedIn(user)).WillReturn(true);
            var controller = new LoginController();

            var response = controller.Login(ValidUser, ValidPassword);

            Assert.IsTrue(GetSuccessValueFromActionResult(response));
        }

        private static ILoginCriteria GetUser()
        {
            return Isolate.Fake.Instance<ILoginCriteria>();
        }

        private static ILoginServices GetLoginServices()
        {
            return Isolate.Fake.Instance<ILoginServices>();
        }

        [Isolated]
        [TestMethod]
        public  void ReturnSuccessValueTrueForPasswordChangeForValidUser()
        {
            var controller = GetController();
            var loginServices = GetLoginServices();
            var user = GetUser();
            IsolateLoginController(user, loginServices);

            Isolate.WhenCalled(() => loginServices.IsLoggedIn(user)).WillReturn(true);
            Assert.IsTrue(loginServices.IsLoggedIn(GetUser()), "Cannot log in");

            Isolate.WhenCalled(() => loginServices.ChangePassword(user, TempPassword)).IgnoreCall();
            Isolate.WhenCalled(() => loginServices.CheckPassword(TempPassword)).WillReturn(true);

            var response = controller.ChangePassword(ValidUser, ValidPassword, TempPassword);
            Isolate.Verify.WasCalledWithExactArguments(() => loginServices.ChangePassword(user, TempPassword));            

            Assert.IsTrue(GetSuccessValueFromActionResult(response), "Password was not changed");
        }

        private static LoginController GetController()
        {
            return new LoginController();
        }

        [Isolated]
        [TestMethod]
        public void NotChangePasswordWhenNotLoggedIn()
        {
            var controller = GetController();
            var loginServices = GetLoginServices();
            var user = GetUser();
            IsolateLoginController(user, loginServices);

            Isolate.WhenCalled(() => loginServices.ChangePassword(user, "newpassword")).IgnoreCall();

            var response = controller.ChangePassword("foo", "oldpassword", "newpassword");
            Isolate.Verify.WasCalledWithExactArguments(() => loginServices.ChangePassword(user, "newpassword"));

            Assert.IsFalse(GetSuccessValueFromActionResult(response), "Password was not changed");
        }

        [Isolated]
        [TestMethod]
        public void ReturnLoginPresentationWithDisabledLoginButtonForInValidLogin()
        {
            LoginController controller = new LoginController();
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = controller.GetLoginPresentation("", "");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        [Isolated]
        [TestMethod]
        public void ReturnLoginPresentationForValidUserWithLoginButtonEnabled()
        {
            LoginController controller = new LoginController();
            Isolate.Fake.StaticMethods(typeof(LoginForm));

            var result = controller.GetLoginPresentation("Admin", "Admin");

            Isolate.Verify.WasCalledWithAnyArguments(() => LoginForm.NewLoginForm("", ""));

            Assert.IsNotNull(result);
            Assert.IsFalse(GetLoginInButtonEnabledValue(result));
        }

        private static bool GetLoginInButtonEnabledValue(ActionResult result)
        {
            var form = (ILoginForm)((JsonResult)(result)).Data.GetType().GetProperty("data").GetValue(((JsonResult)(result)).Data, null);
            return form.Login.Enabled;
        }

    }
}
