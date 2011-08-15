﻿using System;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Services.Products;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.DomainModel.Products;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class ProductController : Controller
    {
        private IProductServices _services;

        public ProductController(IProductServices services)
        {
            _services = services;
        }

        public ProductController() : this(ObjectFactory.GetInstance<IProductServices>()) { }

        //
        // GET: /Product/

        public ActionResult GetGenericNames()
        {
            return this.Direct(
                new[] { new { } }
            );
        }

        public ActionResult GetBrandNames()
        {
            return this.Direct(new[]
                { 
                    new {}
                }
            );
        }

        public ActionResult GetShapeNames()
        {
            return this.Direct(new[]
                {
                    new {}
                }
            );
        }

        public ActionResult GetPackageNames()
        {
            return this.Direct(new[]
                {
                    new {}
                }
            );
        }

        public ActionResult GetUnitNames()
        {
            return this.Direct(new[]
                {
                    new {}
                }
            );
        }

        public ActionResult GetProducts()
        {
            return this.Direct(new { success = true });
        }

        public ActionResult GetProduct(JObject productId)
        {
            var product = String.IsNullOrEmpty(productId.Value<String>("id")) ? LoadProduct(0) : LoadProduct(Int32.Parse(productId.Value<String>("id")));
            return this.Direct(new { success = true, data = (Object)product ?? new { } });
        }

        public IProduct LoadProduct(Int32 productId)
        {
            var product = new Product(new ProductDto());

            return product;
        }

        public ActionResult SaveProduct(ProductDto product)
        {
            var success = true;
            var message = String.Empty;
            try
            {
                _services.SaveProduct(product);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }
            return this.Direct(new { success, data = product, message });
        }

        public ActionResult AddNewBrand(JObject brandDto)
        {
            var success = true;
            var message = String.Empty;
            var brand = GetBrandFromJObject(brandDto);

            try
            {
                _services.AddNewBrand(brand);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = brand, message });
        }

        public ActionResult AddNewGeneric(JObject genericDto)
        {
            var success = true;
            var message = String.Empty;
            var generic = GetGenericFromJObject(genericDto);

            try
            {
                _services.AddNewGeneric(generic);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = generic, message });
        }

        public ActionResult AddNewShape(JObject shapeDto)
        {
            var success = true;
            var message = String.Empty;
            var shape = GetShapeFromJObject(shapeDto);

            try
            {
                _services.AddNewShape(shape);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = shape, message });
        }

        public ActionResult AddNewPackage(JObject packageDto)
        {
            var success = true;
            var message = String.Empty;
            var package = GetPackageFromJObject(packageDto);

            try
            {
                _services.AddNewPackage(package);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = package, message });
        }

        public ActionResult AddNewUnit(JObject unitDto)
        {
            var success = true;
            var message = String.Empty;
            var unit = GetUnitFromJObject(unitDto);

            try
            {
                _services.AddNewUnit(unit);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = unit, message });
        }

        public ActionResult AddNewSubstance(SubstanceDto substanceDto)
        {
            var success = true;
            var message = String.Empty;

            try
            {
                _services.AddNewSubstance(substanceDto);

            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, data = substanceDto, message });
        }

        private IBrand GetBrandFromJObject(JObject brand)
        {
            return new Brand
                       (new BrandDto
                       {
                           Name = brand.Value<String>("BrandName")
                       });
        }

        private IShape GetShapeFromJObject(JObject shape)
        {
            return new Shape(new ShapeDto
                       {
                           Name = shape.Value<String>("ShapeName")
                       });
        }

        private IPackage GetPackageFromJObject(JObject package)
        {
            return DomainFactory.Create<IPackage, PackageDto>( new PackageDto
                       {
                           Name = package.Value<String>("PackageName")
                       });
        }   

        private IGeneric GetGenericFromJObject(JObject generic)
        {
            return new Generic
                       {
                           GenericName = generic.Value<String>("GenericName")
                       };
        }

        private IUnit GetUnitFromJObject(JObject unit)
        {
            return new Unit(new UnitDto
                       {
                           Name = unit.Value<String>("UnitName")
                       });
        }

        public ActionResult DeleteProduct(Int32 id)
        {
            Boolean success;
            var message = String.Empty;
            try
            {
                _services.DeleteProduct(id);
                success = true;
            }
            catch (Exception e)
            {
                success = false;
                message = e.ToString();
            }

            return this.Direct(new { success, message });
        }
    }
}