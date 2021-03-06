Ext.define('GenForm.test.mvc.ApplicationTests', {

    describe: 'Application Tests',

    tests: function () {
        var app;

        beforeEach(function () {
           if (!app) app = GenForm.tests;
        });

        it('TestApplication should be defined', function () {
            expect(GenForm.tests).toBeDefined();
        });

        it('TestApplication should have a path to /Client/GenForm/tests/mvc', function () {
            expect(app.appFolder).toBe('../Client/GenForm/tests/mvc');
        });

        it('TestApplication should have a list of controllers', function () {
            expect(app.controllers).toBeDefined();
        });

        it('TestApplication should have a list of models', function () {
           expect(app.models).toBeDefined();
        });

        it('TestApplication can have a sub module that is an application?', function () {
           expect(app.getController('module1.Module1')).toBeDefined();
        });

    }
});