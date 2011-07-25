﻿using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Brand = Informedica.GenForm.Database.Brand;
using Package = Informedica.GenForm.Database.Package;
using Product = Informedica.GenForm.Database.Product;
using Shape = Informedica.GenForm.Database.Shape;
using Substance = Informedica.GenForm.Database.Substance;
using Unit = Informedica.GenForm.Database.Unit;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class ProductMapper: IDataMapper<IProduct, Product>
    {
        #region Implementation of IDataMapper<IProduct,Product>

        public void MapFromBoToDao(IProduct bo, Product dao)
        {
            dao.Brand = new Brand {BrandName = bo.BrandName == String.Empty ? null: bo.BrandName};
            dao.DisplayName = bo.ProductName == String.Empty ? null: bo.ProductName;
            dao.Divisor = 1;
            dao.Package = new Package {PackageName = bo.PackageName == String.Empty ? null : bo.PackageName};
            dao.ProductCode = "1";
            dao.ProductKey = "1";
            dao.ProductName = dao.DisplayName;
            dao.ProductQuantity = 5;
            dao.Shape = new Shape {ShapeName = bo.ShapeName == String.Empty ? null : bo.ShapeName};
            dao.Substance = new Substance
                                {
                                    SubstanceName = bo.GenericName == String.Empty ? null : bo.GenericName, 
                                    IsGeneric = true
                                };
            dao.Unit = new Unit
                           {
                               UnitGroup = new UnitGroup
                                               {
                                                   UnitGroupName = "Verpakking", 
                                                   AllowsConversion = false
                                               },
                               UnitAbbreviation = bo.UnitName == String.Empty ? null : bo.UnitName,
                               UnitName = bo.UnitName,
                               Divisor = 1,
                               IsReference = false,
                               Multiplier = 1
                           };
        }

        public void MapFromDaoToBo(Product dao, IProduct bo)
        {
            bo.BrandName = dao.Brand == null ? String.Empty: dao.Brand.BrandName;
            bo.GenericName = dao.Substance.SubstanceName;
            bo.PackageName = dao.Package.PackageName;
            bo.ProductCode = dao.ProductCode;
            bo.ProductId = dao.ProductId;
            bo.ProductName = dao.ProductName;
            bo.Quantity = dao.ProductQuantity ?? Decimal.Zero;
            bo.ShapeName = dao.Shape.ShapeName;
            bo.UnitName = dao.Unit.UnitName;
        }

        #endregion
    }
}
