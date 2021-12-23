//// Code for validate all Dropdowns in page
function ValidateDropDown(pType) {
    var isValidddl = true;
    if (pType == '' || pType == null) {
        pType = 'DDLRequired';
    }

    $('.' + pType).each(function () {
        //$('.DDLRequired').each(function () {
        if ($.trim($(this).val()) == '0') {
            isValidddl = false;
            $(this).css({ "border": "1px solid red", "background": "#FFCECE" });
            $(this).focus();
        }
        else {
            $(this).css({ "border": "", "background": "" });
        }
        //if (isValidddl == false) {
        //    e.preventDefault();
        //}
    });
    return isValidddl;
}
// END

// Code for validate All textboxes in Page 
function ValidateTxtBox(pType) {

    var isValid = true;

    //alert('pType : ' + pType);
    if (pType == '' || pType == null) {
        pType = 'Required';
    }

    $('.' + pType).each(function () {
        //$('.Required').each(function () {
        if ($.trim($(this).val()) == '') {
            isValid = false;
            $(this).css({ "border": "1px solid red", "background": "#FFCECE" });
            $(this).focus();
        }
        else {
            $(this).css({ "border": "", "background": "" });
        }
        //if (isValid == false) {
        //    //e.preventDefault();            
        //}
    });
    return isValid;
}
// End
