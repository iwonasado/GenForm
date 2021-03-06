﻿using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ProductMap : EntityMap<Product>
    {
        public ProductMap()
        {
            Map(x => x.DisplayName).Not.Nullable().Unique().Length(255);
            Map(x => x.GenericName).Not.Nullable().Unique().Length(255);
            Map(x => x.ProductCode).Length(30);
            References<Brand>(x => x.Brand)
                .Cascade.SaveUpdate();
            References<Package>(x => x.Package)
                .Not.Nullable()
                .Cascade.SaveUpdate();
            Component(x => x.Quantity, UnitValueMap.GetMap());
            References<Shape>(x => x.Shape)
                .Not.Nullable()
                .Cascade.SaveUpdate();
            HasMany(x => x.SubstanceList)
                .AsList(s => s.Column("SortOrder"))
                .Cascade.All().Inverse();
            HasManyToMany(x => x.RouteSet).Table("RouteToProduct")
                .AsSet()
                .Cascade.All();
        }
    }
}

