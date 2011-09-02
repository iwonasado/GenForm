﻿using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products.Interfaces
{
    public interface ISubstance
    {
        String Name { get; }
        ISubstanceGroup SubstanceGroup { get; }
        IEnumerable<IProduct> Products { get; }
        void SetSubstanceGroup(ISubstanceGroup group);
        void RemoveFromSubstanceGroup();
        void AddProduct(IProduct product);
    }
}