Ext.define('GenForm.test.controller.ProductControllerTests', {
    describe: 'ProductControllerShould',

    tests: function () {
        var me = this,
            productController,
            testProduct = {
                Name: '',
                ProductCode: '',
                GenericName: '',
                BrandName: '',
                ShapeName: '',
                Quantity: '',
                Unit: '',
                PackageName: ''
            };

        beforeEach(function () {
           me.setProductController();
        });

        me.copyRecord = function (model, data) {
            var prop;
            if (!model.data) return;

            for (prop in model.data) {
                if (data[prop]) model.data[prop] = data[prop];
            }
        };

        me.setProductController = function () {
            if (!productController) {
                productController = me.createProductController();
            }
        };

        me.createProductController = function () {
            return Ext.create('GenForm.controller.product.Product', {
                id: 'testProductController',
                application: GenForm.application
            });
        };

        it('be defined', function () {
            expect(productController).toBeDefined();
        });

        it('have a productHandler', function () {
            expect(productController.mixins.productHandler).toBeDefined();
        });

        it('have a productHandler with a productSubstanceHandler', function () {
            expect(productController.mixins.productHandler.mixins.productSubstanceHandler).toBeDefined();
        });

        it('have a productHandler with a genericHandler', function () {
            expect(productController.mixins.productHandler.mixins.genericHandler).toBeDefined();
        });

        it('have a productHandler with a shapeHandler', function () {
            expect(productController.mixins.productHandler.mixins.shapeHandler).toBeDefined();
        });

        it('have a productHandler with a packageHandler', function () {
            expect(productController.mixins.productHandler.mixins.packageHandler).toBeDefined();
        });

        it('have a productHandler with a brandHandler', function () {
            expect(productController.mixins.productHandler.mixins.brandHandler).toBeDefined();
        });

        it('have a productHandler with a unitHandler', function () {
            expect(productController.mixins.productHandler.mixins.unitHandler).toBeDefined();
        });

        it('should have a getGenFormModelProductProductModel', function () {
            expect(productController.getProductProductModel).toBeDefined();
        });

        it('can return a GenFormModelProductProductModel', function () {
            expect(productController.getProductProductModel().$className).toBe("GenForm.model.product.Product");
        });

        it('getProductProductModel should return a constructor, i.e. a function', function () {
            expect(typeof productController.getProductProductModel()).toBe('function');
        });

        it('can create and empty instance of a Product', function () {
           expect(productController.createEmptyProduct()).toBeDefined();
        });

        it('should have a getProductProductWindow function', function () {
            expect(productController.getProductProductWindowView).toBeDefined();
        });

        it('should have a getProductProductFormView', function () {
            expect(productController.getProductProductFormView).toBeDefined();
        });

        it('can fire up a productwindow', function () {
            var windowCount = Ext.ComponentQuery.query('window').length;
            productController.showProductWindow();

            expect(Ext.ComponentQuery.query('window').length === (windowCount + 1)).toBeTruthy();
        });

        it('should have a LoadEmptyProduct function to load the productform with an empty product', function () {
            expect(productController.loadEmptyProduct).toBeDefined('loadEmptyProduct was not defined');

            spyOn(productController, 'createEmptyProduct').andCallThrough();
            productController.loadEmptyProduct(productController.getProductWindow());

            expect(productController.createEmptyProduct).toHaveBeenCalled();
            expect(productController.getProductWindow().forms.ProductForm.getForm().getRecord()).toBeDefined('Product form should have a record');
        });

        it('when a form is created with an empty product, productname should be empty', function () {
            var window = productController.getProductWindow();
            expect(window.forms.ProductForm.getProduct().data.Name === '').toBeTruthy();
        });

        it('should have a saveProduct function', function () {
            expect(productController.saveProduct).toBeDefined();
        });


        it('should save a product', function () {
            var form = productController.getProductWindow().forms.ProductForm,
                record = form.getRecord();

            testProduct.Id = Ext.create('GenForm.lib.util.GuidGenerator').emptyGuid();
            testProduct.Name = 'paracetamol 500 mg';
            testProduct.GenericName = 'paracetamol';
            testProduct.BrandName = 'Paracetamol';
            testProduct.ShapeName = 'tablet';
            testProduct.Quantity = '1';
            testProduct.Unit = 'stuk';
            testProduct.PackageName = 'tablet';
            me.copyRecord(record, testProduct);

            form.loadRecord(record);

            spyOn(productController, 'onProductSaved');
            productController.saveProduct(Ext.ComponentQuery.query('productwindow button[text=Opslaan]')[0]);

            waitsFor(function () {
                return productController.onProductSaved.wasCalled;
            }, 'waiting for onProductSaved call', GenForm.test.TestList.waitingTime);
        });


        it('should have a saveShape function', function () {
           expect(productController.onSaveShape).toBeDefined();
        });

        it('saveShape method should save a valid Shape', function () {
            var form = productController.getShapeWindow().forms.ShapeForm,
            model = form.getRecord(),
            validShape = {
                ShapeName: 'infusievloeistof'
            };
            me.copyRecord(model, validShape);

            form.loadRecord(model);

        });

    }
});