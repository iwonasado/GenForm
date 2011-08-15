﻿using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class RouteMap : EntityMap<Route, Guid, RouteDto>
    {
        public RouteMap()
        {
            Map(r => r.Abbreviation).Not.Nullable().Length(30).Unique();
            HasMany(r => r.Shapes)
                .Cascade.AllDeleteOrphan()
                .Cascade.SaveUpdate();
        }
    }
}
