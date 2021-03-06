//noinspection JSUnusedGlobalSymbols
Ext.define('GenForm.lib.util.PropertySearcher', {

    searchProp: function (obj, propName) {
        var fnObj = undefined;
        if (obj === undefined) return;
        for (var prop in obj) {
            if (prop === propName) return 'found click on ' + obj;
            if (typeof(obj[prop]) === 'function') {
                try {
                    fnObj = obj[prop]();
                } catch(e) {

                } finally {
                    fnObj = undefined
                }
                if (fnObj !== obj && fnObj !== undefined) searchProp(fnObj, propName);
            }
        }
    }
});
