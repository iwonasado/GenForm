/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/8/11
 * Time: 10:52 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.PackageForm', {
    extend: 'Ext.form.Panel',
    alias: 'widget.packageform',

    initComponent: function () {
        var me = this;
        me.items = me.createItems();

        this.callParent(arguments);
    },

    createItems: function () {
        var items = [
            { xtype: 'textfield', name:'PakageName',   fieldLabel: 'Verpakking Naam', margin: '10 0 10 10' }
        ];

        return items;
    },

    getPackage: function () {
        var me = this,
            record = me.getRecord();

        me.getForm().updateRecord(record);
        return record;
    }

});