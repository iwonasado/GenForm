﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.Security
{
    public class AnonymousIdentity: IAnonymousIdentity
    {
        public string Name
        {
            get { return String.Empty; }
        }

        public string AuthenticationType
        {
            get { return String.Empty; }
        }

        public bool IsAuthenticated
        {
            get { return false; }
        }
    }
}
