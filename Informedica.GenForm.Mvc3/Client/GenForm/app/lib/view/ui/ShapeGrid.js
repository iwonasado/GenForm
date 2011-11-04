Ext.define('GenForm.lib.view.ui.ShapeGrid', {
    extend: 'Ext.grid.Panel',

    itemId: 'grdShape',
    title: 'Vormen',

    initComponent: function () {
        var me = this;

        me.store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetShapeNames }).load();
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