/**
 * Created by .
 * User: hal
 * Date: 27-4-11
 * Time: 13:14
 * To change this template use File | Settings | File Templates.
 */

Ext.define('GenForm.store.product.GenericNameStore', {
    extend: 'Ext.data.Store',
    alias: 'widget.genericnamestore',
    storeId: 'genericnamestore',
    
    model: 'GenForm.model.product.GenericNameModel',
    autoLoad: true

});