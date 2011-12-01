﻿using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Interfaces
{
    public interface IUnit
    {
        String Name { get;}
        UnitGroup UnitGroup { get; }
        String Abbreviation { get; }
        Decimal Multiplier { get; }
        Boolean IsReference { get; }
    }
}