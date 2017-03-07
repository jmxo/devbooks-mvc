var ValidationUtility = function () {
    var validationElements = $('[data-role="validate"]');
    var elementCount;

    validationElements.popover({
        placement: 'top'
    });

    validationElements.on('invalid', function () {
        if (elementCount === 0) {
            $('#' + this.id).popover('show');
            elementCount++;
        }
    });

    validationElements.on('blur', function () {
        $('#' + this.id).popover('hide');
    });

    var validate = function (formSelector) {
        elementCount = 0;

        if (formSelector.indexOf('#') === -1) {
            formSelector = '#' + formSelector;
        }

        return $(formSelector)[0].checkValidity();
    };

    return {
        validate: validate
    };
};