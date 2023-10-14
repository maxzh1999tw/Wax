var alertHelper = {

    /**
     * 跳出提醒
     * @param {string} message 顯示訊息
     * @returns {Promise} 是否按下確定
     */
    alert(message) {
        alert(message)
        return Promise.resolve()
    },

    confirm(message) {
        return Promise.resolve(confirm('您確定要刪除這個項目嗎？'))
    },
}