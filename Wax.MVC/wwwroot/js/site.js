const { createApp } = Vue
const { createVuetify, validatable } = Vuetify

const vuetify = createVuetify()

const validateRule = {
    lengthRequiredRule(value) {
        if (value != null && value.length > 0) {
            return true
        }

        return '此為必填欄位'
    },

    notEmptyRule(value) {
        if (value !== null && value !== undefined) {
            return true
        }

        return '此為必填欄位'
    },
}



Array.prototype.firstOrDefault = function (predicate) {
    if (predicate) {
        return this.find(predicate) || null;
    }
    return this.length > 0 ? this[0] : null;
}
