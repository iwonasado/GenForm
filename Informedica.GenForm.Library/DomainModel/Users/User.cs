﻿using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Users
{
    public class User: IUser
    {
        #region IUser

        private string _userName;
        private string _password;
        private string _lastName;
        private string _firstName;
        private string _email;
        private string _pager;
        private Int32 _userId;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Pager
        {
            get { return _pager; }
            set { _pager = value; }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private static IRepository<IUser> Repository
        {
            get { return ObjectFactory.GetInstance<IRepository<IUser>>(); }
        }

        #endregion

        #region Factory Methods

        public static IUser NewUser()
        {
            return new User();
        }

        public static IEnumerable<IUser> GetUser(String name)
        {
            return Repository.Fetch(name);
        }

        #endregion

        #region Implementation of IIdentity

        public string Name
        {
            get { return UserName; }
        }

        public string AuthenticationType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
