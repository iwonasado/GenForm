Ext.define('GenForm.test.store.GenericNameStoreTests', {

    describe: 'GenericNameStoreShould',

    tests: function() {
        var me = this, store,
            storeName = 'GenForm.store.product.GenericName';

        beforeEach(function () {
            if (!store) store = me.createStore();
        });

        me.createStore = function () {
            return Ext.create(storeName);
        };

        me.setUpTestProxy = function () {
            store.setProxy(me.getTestProxy());
        };

        me.getTestProxy = function () {
            return Ext.create('Ext.data.proxy.Direct', {
                type: 'direct',
                directFn: GenForm.server.UnitTest.GetGenericNames
            });
        };

        it('be defined', function () {
            expect(store).toBeDefined();
        });

        it('to have a direct function', function () {
            expect(store.proxy.directFn).toBeDefined();
        });

        it('return an empty record with GenericName property', function () {
            var record = store.create();
            expect(record.data.GenericName).toBeDefined();
        });

        it('contain an item', function () {
            store.add({GenericName: 'test'});
            expect(store.count() > 0).toBeTruthy();
        });

        it('have an item with GenericName test', function () {
            expect(store.findExact('GenericName', 'test') !== -1).toBe(true);
        });

        it('have test direct Fn defined', function () {
           expect(GenForm.server.UnitTest.GetGenericNames).toBeDefined();
        });


        it('load five test items', function () {
            var result;
            me.setUpTestProxy();
            store.load({
                scope   : this,
                callback: function(records) {
                    //the operation object contains all of the details of the load operation
                    result = records;
                }
            });

            waitsFor(function () {
                return result && result.length == 5 || false;
            }, 'GenericNameStore to load', GenForm.test.waitingTime);
        });

        it('now contain a generic morfine', function () {
            expect(store.findExact('GenericName', 'morfine') != -1).toBeTruthy();
        });

        it('also contain penicilline after load', function () {
            var found = store.getAt(store.findExact('GenericName', 'penicilline'));
            expect(found.data.GenericName).toBe('penicilline');
        });

    }
});