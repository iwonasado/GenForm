﻿using Informedica.GenForm.Mvc3.Tests.UnitTests;

namespace Informedica.GenForm.Acceptance
{
    public class UserLoginDecisions
    {
        private static bool _loggedIn;

        public string GivenUser { get; set; }

        public string GivenPassword { get; set; }

        public string GivenEnvironment { get; set; }

        public string GivenFormularium { get; set; }

        public bool IsUserAuthenticated()
        {
            var test = new LoginControllerShould();

            try
            {
                test.ReturnSuccessValueIsTrueWhenValidUserLogin();
                return true;

            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool IsUserLoggedIn()
        {
            return _loggedIn;
        }

        public bool GivenUserLogsIn(string user)
        {
            _loggedIn = user == "Admin";
            return IsUserAuthenticated();
        }

        public bool IsLoggedInUser(string user)
        {
            return _loggedIn && user == "Admin";
        }

        public bool IsUserAuthorized()
        {
            return GivenUser == "Admin";
        }

        public bool CanUserLogin()
        {
            return IsUserAuthenticated() && IsUserAuthorized();
        }

    }
}
