﻿using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IUnit
    {
        String Name { get;}
        IUnitGroup UnitGroup { get; }
        String Abbreviation { get; }
        Decimal Multiplier { get; }
        Boolean IsReference { get; }
    }
}
