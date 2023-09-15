const { createApp } = Vue
const { createVuetify } = Vuetify

const vuetify = createVuetify()

function textRequiredRule(value) {
    if (value != null && value.length > 0) {
        return true
    }

    return '此為必填欄位'
}