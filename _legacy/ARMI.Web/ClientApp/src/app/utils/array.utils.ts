export class ArrayUtilities {
  public static serializeCircularToJson(obj) {
        var au: ArrayUtilities = new ArrayUtilities();
        var res1 = au.str('', { '': obj });
        //console.log("serializeCircularToJson", obj, res1);
        return res1;
    }

    objs: any = [];

    escapable: RegExp = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g;

    private quote(string) {
        const meta = {    // table of character substitutions
            '\b': '\\b',
            '\t': '\\t',
            '\n': '\\n',
            '\f': '\\f',
            '\r': '\\r',
            '"': '\\"',
            '\\': '\\\\'
        };
        // If the string contains no control characters, no quote characters, and no
        // backslash characters, then we can safely slap some quotes around it.
        // Otherwise we must also replace the offending characters with safe escape
        // sequences.
        this.escapable.lastIndex = 0;
        return this.escapable.test(string) ?
            '"' + string.replace(this.escapable, function (a) {
                var c = meta[a];
                return typeof c === 'string' ? c :
                    '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
            }) + '"' :
            '"' + string + '"';
    }

    private str(key, holder) {

        // Produce a string from holder[key].

        var i,          // The loop counter.
            k,          // The member key.
            v,          // The member value.
            length,
            partial,
            value = holder[key];

        // If the value has a toJSON method, call it to obtain a replacement value.

        if (value && typeof value === 'object' &&
            typeof value.toJSON === 'function') {
            value = value.toJSON(key);
        }

        // What happens next depends on the value's type.

        switch (typeof value) {
            case 'string':
                return this.quote(value);

            case 'number':

                // JSON numbers must be finite. Encode non-finite numbers as null.

                return isFinite(value) ? String(value) : 'null';

            case 'boolean':

                // If the value is a boolean or null, convert it to a string. Note:
                // typeof null does not produce 'null'. The case is included here in
                // the remote chance that this gets fixed someday.

                return String(value);

            // If the type is 'object', we might be dealing with an object or an array or
            // null.

            case 'object':

                // Due to a specification blunder in ECMAScript, typeof null is 'object',
                // so watch out for that case.

                if (!value) {
                    return 'null';
                }

                // Make an array to hold the partial results of stringifying this object value.

                partial = [];

                // Is the value an array?

                if (Object.prototype.toString.apply(value) === '[object Array]') {

                    // The value is an array. Stringify every element. Use null as a placeholder
                    // for non-JSON values.

                    length = value.length;
                    for (i = 0; i < length; i += 1) {
                        partial[i] = this.str(i, value) || 'null';
                    }

                    // Join all of the elements together, separated with commas, and wrap them in
                    // brackets.

                    v = partial.length === 0 ? '[]' : '[' + partial.join(',') + ']';
                    return v;
                } else {
                    var pevid = this.isUsed(value);
                    if (pevid === -1) {
                        partial.push('"$id":"' + (this.objs.length - 1) + '"');
                        var ks = Object.getOwnPropertyNames(value);
                        for (k in ks) {
                            v = this.str(ks[k], value);
                            if (v) {
                                partial.push(this.quote(ks[k]) + ':' + v);
                            }
                        }
                    } else {
                        partial.push('"$ref":"' + pevid + '"');
                    }
                }
                // Join all of the member texts together, separated with commas,
                // and wrap them in braces.

                v = partial.length === 0 ? '{}' : '{' + partial.join(',') + '}';
                return v;
        }
        return v;
    }

    private isUsed(obj) {
        const idx = this.objs.indexOf(obj);
        if (idx === -1) {
            this.objs.push(obj);
        }
        return idx;
    }

    public static rebuildJsonDotNetObj(obj) {
        if (typeof obj === "boolean") {
            return obj;
        }
        var arr = {};
        this.buildRefArray(obj, arr);
        return this.setReferences(obj, arr)
    }

    private static buildRefArray(obj, arr) {
        if (!obj || obj['$ref'])
            return;
        var objId = obj['$id'];
        if (!objId) {
            if (Object.prototype.toString.apply(obj) === '[object Array]') {
                obj.forEach(function (elem) {
                    if (typeof elem === "object")
                        ArrayUtilities.buildRefArray(elem, arr);
                });
            }
            return;
        }
        var id = parseInt(objId);
        arr[id] = obj;
        var ks = Object.getOwnPropertyNames(obj);
        for (var k in ks) {
            var prop = ks[k];
            if (typeof obj[prop] === "object") {
                ArrayUtilities.buildRefArray(obj[prop], arr);
            }
        }
    }

    private static setReferences(obj, arrRefs) {
        if (!obj)
            return obj;
        var ref = obj['$ref'];
        if (ref)
            return arrRefs[parseInt(ref)];

        if (Object.prototype.toString.apply(obj) === '[object Array]') {
            for (var i = 0; i < obj.length; ++i)
                obj[i] = ArrayUtilities.setReferences(obj[i], arrRefs)
            return obj;
        }

        if (!obj['$id']) //already visited
            return obj;

        var ks = Object.getOwnPropertyNames(obj);
        for (var k in ks) {
            var prop = ks[k];
            if (typeof obj[prop] === "object")
                obj[prop] = ArrayUtilities.setReferences(obj[prop], arrRefs)
        }
        delete obj['$id'];
        return obj;
    }
}
