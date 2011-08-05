﻿using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.Library.Services.Products
{
    public class ProductServices : IProductServices
    {
        #region Implementation of IProductServices

        public IProduct GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IProduct GetProduct(String productName)
        {
            throw new NotImplementedException();
        }

        public void AddNewBrand(IBrand brand)
        {
            var repository = ObjectFactory.GetInstance<IRepository<IBrand>>();
            repository.Insert(brand);
        }

        public void AddNewGeneric(IGeneric generic)
        {
            var repository = ObjectFactory.GetInstance<IRepository<IGeneric>>();
            repository.Insert(generic);
        }

        public void AddNewShape(IShape shape)
        {
            var repository = ObjectFactory.GetInstance<IRepository<IShape>>();
            repository.Insert(shape);
        }

        public void AddNewPackage(IPackage package)
        {
            var repository = ObjectFactory.GetInstance<IRepository<IPackage>>();
            repository.Insert(package);
        }

        public void AddNewUnit(IUnit unit)
        {
            var repository = ObjectFactory.GetInstance<IRepository<IUnit>>();
            repository.Insert(unit);
        }

        public void AddNewSubstance(SubstanceDto substDto)
        {
            var subst = DomainFactory.Create<ISubstance, SubstanceDto>(substDto);
            var repository = ObjectFactory.GetInstance<IRepository<ISubstance>>();
            repository.Insert(subst);
        }

        public ProductDto SaveProduct(ProductDto productDto)
        {
            var repository = ObjectFactory.GetInstance<IRepository<IProduct>>();
            var product = NewProduct(productDto);
            repository.Insert(product);
            productDto.Id = product.ProductId;
            return productDto;
        }

        private static IProduct NewProduct(ProductDto productDto)
        {
            return ObjectFactory.With(productDto).GetInstance<IProduct>();
        }

        public void DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IProduct GetEmptyProduct()
        {
            return ObjectFactory.GetInstance<IProduct>();
        }

        #endregion
    }
}