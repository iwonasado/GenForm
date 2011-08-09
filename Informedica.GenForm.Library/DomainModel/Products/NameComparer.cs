﻿using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public abstract class NameComparer
    {
        protected bool EqualName(String x, String y)
        {
            return x == y && !String.IsNullOrEmpty(x);
        }        
    }
}