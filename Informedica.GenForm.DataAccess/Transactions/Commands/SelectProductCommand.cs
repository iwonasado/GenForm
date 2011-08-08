﻿using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public class SelectProductCommand : ISelectCommand<IProduct>, ICommand, IExecutable
    {
        private IEnumerable<IProduct> _resultList;

        public SelectProductCommand(Int32 id): this(p => p.ProductId == id){}

        public SelectProductCommand(String name): this(p => p.ProductName == name){}

        [DefaultConstructor]
        public SelectProductCommand(): this(p => true) {}

        public SelectProductCommand(Func<Product, Boolean> selector)
        {
            throw new NotImplementedException(selector.ToString());
        }

        public IEnumerable<IProduct> Result
        {
            get { return _resultList; }
        }

        public void Execute(GenFormDataContext context)
        {
            _resultList = GetProductRepository().Fetch("");
        }

        private static Repository<IProduct> GetProductRepository()
        {
            return ObjectFactory.GetInstance<Repository<IProduct>>();
        }
    }

}