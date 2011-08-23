using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class ProductRepository : NHibernateRepository<Product, Guid, ProductDto>
    {
        public ProductRepository(ISessionFactory factory) : base(factory) {}

        public override void Add(Product item)
        {
            base.Add(item, new ProductComparer());
        }
    }
}