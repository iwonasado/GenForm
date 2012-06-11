﻿using System;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Mvc3.Environments;
using Informedica.GenForm.Services;
using MyNamespace;
using StructureMap;
using TypeMock.ArrangeActAssert;
using System.Web.Mvc;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    [TestClass]
    public class InjectingActionInvokerShould
    {
        private InjectingActionInvoker _invoker;
        private ControllerContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = Isolate.Fake.Instance<ControllerContext>();
            var controller = new TestController();

            Isolate.WhenCalled(() => _context.Controller).WillReturn(controller);
            _invoker = ObjectFactory.GetInstance<InjectingActionInvoker>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ObjectFactory.Initialize(x => {});
            System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
        }

        [Isolated]
        [TestMethod]
        public void MakeSureThatIShouldBeInjectedSettingOfTestAttributeIsInjected()
        {
            FilterAttributeDependencyInversionConfigurator.Configure<TestAttribute>();

            var testSetting = new TestImplementation();
            ObjectFactory.Configure(x => x.For<IShouldBeInjectedSetting>().Use(testSetting));

            var methodName = "Test";
            Assert.IsTrue(_invoker.InvokeAction(_context, methodName));
        }

        [Isolated]
        [TestMethod]
        public void MakeSureThatIDatabaseServiceOfNHibernateSessinoAttributeIsInjected()
        {
            FilterAttributeDependencyInversionConfigurator.Configure<IDatabaseServices>();

            SetupDatabaseServices();

            InvokeTestNhibernateSessionAttribute();
        }

        [Isolated]
        [TestMethod]
        public void Have_ShouldBeInjectedSettingInjected_ButNot_ShouldNotBeInjectedSettingOfTestAttribute()
        {
            FilterAttributeDependencyInversionConfigurator.Configure<TestAttribute>();

            ConfigureTypesForTestAttribute();

            var methodName = "Test";
            Assert.IsTrue(_invoker.InvokeAction(_context, methodName));
        }

        [Isolated]
        [TestMethod]
        [ExpectedException(typeof(TestAttributeException))]
        public void ThrowATestAttributeExceptionWhenSettingsOfTestAttributeAreNotInjectedProperly()
        {
            FilterAttributeDependencyInversionConfigurator.Configure<IShouldNotBeInjectedSetting>();

            ConfigureTypesForTestAttribute();

            var methodName = "Test";
            Assert.IsTrue(_invoker.InvokeAction(_context, methodName));
        }

        private static void ConfigureTypesForTestAttribute()
        {
            var testSetting = new TestImplementation();
            ObjectFactory.Configure(x => x.For<IShouldBeInjectedSetting>().Use(testSetting));
            var notASetting = new ShouldNotBeInjectedSetting();
            ObjectFactory.Configure(x => x.For<IShouldNotBeInjectedSetting>().Use(notASetting));
        }

        private static void SetupDatabaseServices()
        {
            ObjectFactory.Configure(x => x.For<IDatabaseServices>().Use<DatabaseServices>());
            var cache = Isolate.Fake.Instance<HttpSessionCache>();
            ObjectFactory.Configure(x => x.For<ISessionCache>().Use(cache));
        }

        private void InvokeTestNhibernateSessionAttribute()
        {
            var methodName = "TestNHibernateSessionAttribute";
            Assert.IsTrue(_invoker.InvokeAction(_context, methodName));
        }
    }

    public class TestAttributeException: Exception
    {
    }


    public class TestImplementation: IShouldBeInjectedSetting
    {
    }

    public class TestController : Controller
    {
        private ViewResult _view;

        public TestController()
        {
            _view = Isolate.Fake.Instance<ViewResult>();
        }

        [Test]
        public ActionResult Test()
        {
            return _view;
        }

        [NHibernateSession]
        public ActionResult TestNHibernateSessionAttribute()
        {
            return _view;
        }
    }

    public class TestAttribute : ActionFilterAttribute
    {
        public IShouldBeInjectedSetting ShouldBeInjectedSetting { get; set; }

        public IShouldNotBeInjectedSetting ShouldNotBeInjectedSetting { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ShouldBeInjectedSetting == null || ShouldNotBeInjectedSetting != null) throw new TestAttributeException();
        }
    }


    public interface IShouldBeInjectedSetting
    {
    }

}

namespace MyNamespace
{
    public interface IShouldNotBeInjectedSetting
    {
    }

    public class ShouldNotBeInjectedSetting : IShouldNotBeInjectedSetting
    {
    }


}
