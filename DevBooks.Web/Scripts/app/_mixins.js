var _mixIns = (function () {
    _.mixin({
        numberWithCommas: function (value) {
            return "$" + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
    });
}());