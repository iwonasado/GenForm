/**
 * Created by .
 * User: hal
 * Date: 6-5-11
 * Time: 20:13
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.product.BrandNameModel', {
    extend: 'Ext.data.Model',
    alias: 'widget.brandnamemodel',

    fields: [
        { name: 'BrandName', type: 'string' },    
    ],

    proxy: {
        type: 'direct',
        directFn: Product.GetBrandNames
    }
});