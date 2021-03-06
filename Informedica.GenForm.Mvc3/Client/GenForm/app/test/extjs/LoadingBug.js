Ext.define('IdNameModel', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        { name: 'Id', type: 'string'},
        { name: 'Name', type: 'string'}
    ]
});

Ext.define('IdNameStore', {
    extend: 'Ext.data.DirectStore',
    model: 'IdNameModel',

    constructor: function (config) {
        var me = this;

        if (!config || !config.directFn || !(config.directFn instanceof Function)) {
            Ext.Error.raise('IdName store has to be constructed with a valid directFn');
        }

        config.root = 'data';
        config.idProperty = 'Id';

        me.initConfig(config);
        me.callParent(arguments);
        return me;
    }
});


Ext.define('RouteGrid', {
    extend: 'Ext.grid.Panel',

    itemId: 'grdRoute',
    title: 'Routes',

    initComponent: function() {
        var me = this;

        me.store = Ext.create('IdNameStore', { directFn: GenForm.server.UnitTest.GetRouteNames });

        me.columns = [
            {
                xtype: 'gridcolumn',
                hidden: true,
                itemId: 'colId',
                dataIndex: 'Id',
                text: 'Id'
            },
            {
                xtype: 'gridcolumn',
                itemId: 'colName',
                dataIndex: 'Name',
                text: 'Naam'
            }
        ];
        me.callParent(arguments);
    }

});

var routeGrid = Ext.define('GenForm.view.product.RouteGrid', {
    extend: 'GenForm.lib.view.ui.RouteGrid',
    alias: 'widget.routegrid',

    initComponent: function () {
        var me = this;

        this.callParent(arguments)
    },

    getRouteStore: function () {
        var store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetRouteNames });
        store.load();
        return store;
    }
});

Ext.define('GenForm.lib.view.ui.UnitGroupGrid', {
    extend: 'Ext.grid.Panel',

    initComponent: function () {
        var me = this;

        me.itemId = 'grdUnitGroup',
        me.title = 'Eenheden',
        me.store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetUnitNames }).load();

        me.columns = [
            {
                xtype: 'gridcolumn',
                hidden: true,
                itemId: 'colId',
                dataIndex: 'Id',
                text: 'Id'
            },
            {
                xtype: 'gridcolumn',
                itemId: 'colName',
                dataIndex: 'Name',
                text: 'Eenheidgroup'
            }
        ];

        me.callParent(arguments);
    }
}).create();

Ext.define('GenForm.view.product.UnitGroupGrid', {
    extend: 'GenForm.lib.view.ui.UnitGroupGrid',
    alias: 'widget.unitgroupgrid',

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },

    createUnitGroupStore: function () {
        var store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetUnitNames});
        store.load();
        return store;
    }
});

Ext.define('GenForm.lib.view.ui.PackageGrid', {
    extend: 'Ext.grid.Panel',

    itemId: 'grdPackage',
    title: 'Verpakkingen',

    initComponent: function () {
        var me = this;

        me.store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetPackageNames }).load();
        me.columns =  [
            {
                xtype: 'gridcolumn',
                hidden: true,
                itemId: 'colId',
                dataIndex: 'Id',
                text: 'String'
            },
            {
                xtype: 'gridcolumn',
                itemId: 'colName',
                dataIndex: 'Name',
                text: 'Naam'
            }
        ];

        me.callParent(arguments);
    }
});

Ext.define('GenForm.view.product.PackageGrid', {
    extend: 'GenForm.lib.view.ui.PackageGrid',
    alias: 'widget.packagegrid',

    initComponent: function () {
        var me = this;

        this.callParent(arguments)
    },

    getPackageStore: function () {
        var store =  Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetPackageNames});
        store.load();
        return store;
    }
});

Ext.define('GenForm.lib.view.component.IdNameCombo', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.idnamecombo', 'widget.idnamecombobox'],

    displayField: 'Name',
    valueField: 'Name',
    editable: false,
    typeAhead: true,
    queryMode: 'local',

    constructor: function (config) {
        var me = this;

        if(config.directFn) {
            config.store = me.createStore(config.directFn);
        }

        if (!config || !config.store) {
            Ext.Error.raise('Combobox needs a store');
        }

        me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },

    createStore: function (directFn) {
        var store = Ext.create('GenForm.store.common.IdName', { directFn: directFn });
        store.load();
        return store;
    },

    getIdValue: function () {
        var me = this;

        return me.store.findRecord('Name', me.getValue()).data.Id;
    }

});

//noinspection JSUnusedGlobalSymbols
Ext.define('GenForm.lib.util.mixin.FormFieldCreator', {

    createTextField: function (config) {
        var me = this;
        return me.createField('Ext.form.Text', config);
    },

    createNumberField: function (config) {
        var me = this;
        return me.createField('Ext.form.Number', config);
    },

    createHiddenField: function (config) {
        var me = this;
        return me.createField('Ext.form.field.Hidden', config);
    },

    createComboBox: function (config) {
        var me = this, field;
        //noinspection JSUnusedGlobalSymbols
        field = config.idName ? 'GenForm.lib.view.component.IdNameCombo' : '';
        if (field == '') field = 'Ext.form.field.ComboBox';

        return me.createField(field, config);
    },

    createField: function (field, config) {
        var me = this;

        if (!me.fields) me.fields = {};

        me.fields[config.name] = Ext.create(field, config);
        return me.fields[config.name];
    }
});

Ext.define('GenForm.lib.view.form.FormBase', {
    extend: 'Ext.form.Panel',

    mixins: ['GenForm.lib.util.mixin.FormFieldCreator'],

    height: 600,
    width: 700,

    constructor: function (config) {
        var me = this;
        me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;

        if (me.createItems) me.items = me.createItems();
        me.callParent(arguments);
    },

    getFormData: function () {
        var me = this,
            record = me.getRecord();

        me.getForm().updateRecord(record);
        return record;
    }
});

Ext.define('GenForm.lib.view.window.SaveCancelWindow', {
    extend: 'Ext.window.Window',

    mixins: ['GenForm.lib.util.mixin.FormCreator'],
    closeAction: 'destroy',

    constructor: function (config) {
        var me = this;

        me = me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;

        me.toolbar = {};

        me.callParent(arguments);
    }

});

Ext.define('GenForm.lib.view.ui.ShapeForm', {
    extend: 'GenForm.lib.view.form.FormBase',

    height: 326,
    width: 460,
    bodyPadding: 10,

    initComponent: function() {
        var me = this;

        me.items = [
            {
                xtype: 'fieldset',
                itemId: 'flsShapeDetails',
                title: 'Vorm Details',
                items: [
                    {
                        xtype: 'hiddenfield',
                        itemId: 'fldId',
                        name: 'Id',
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        itemId: 'fldName',
                        name: 'Name',
                        fieldLabel: 'Naam',
                        anchor: '100%'
                    }
                ]
            },
            {
                xtype: 'tabpanel',
                activeTab: 0,
                layout: 'anchor',
                anchor: '100%',
                maintainFlex: true,
                items: [
                    me.createPackageGrid(),
                    me.createRouteGrid(),
                    me.createUnitGroupGrid()
                ]
            }
        ];
        me.callParent(arguments);
    }
});

Ext.define('GenForm.view.product.ShapeForm', {
    extend: 'GenForm.lib.view.ui.ShapeForm',
    alias: 'widget.shapeform',

    getShape: function () {
        var me = this;
        return me.getFormData();
    },

    createPackageGrid: function () {
        return Ext.create('GenForm.view.product.PackageGrid');
    },

    createRouteGrid: function () {
        return Ext.create('GenForm.view.product.RouteGrid');
    },

    createUnitGroupGrid: function () {
        return Ext.create('GenForm.view.product.UnitGroupGrid');
    }

});


Ext.define('GenForm.view.product.ShapeWindow', {
    extend: 'GenForm.lib.view.window.SaveCancelWindow',
    alias: 'widget.shapewindow',

    width: 300,
    height: 300,
    layout: 'fit',

    initComponent: function() {
        var me = this;
        me.forms = {};
        me.items = me.createShapeForm();

        me.callParent(arguments);
    },

    createShapeForm: function () {
        var me = this;
        return me.createForm({ xtype: 'shapeform', name: 'ShapeForm' });
    },

    loadWithShape: function (shape) {
        var me = this;
        me.forms.ShapeForm.loadRecord(shape);
    }

}).create().show();

