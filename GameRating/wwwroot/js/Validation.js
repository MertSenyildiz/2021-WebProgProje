function validate() {
    /*var firstName = validateFirstName();
    var mail = validateMail();
    var lastName = validateLastName();
    var userName = validateUserName();
    var password = validatePassword();
    if (mail && firstName && lastName && userName && password)*/
    if (validateFirstName() && validateLastName() && validateUserName() && validateMail() && validatePassword()) {
        return false;
    }  
    return false;
}
function validateMail() {
    var mail = document.getElementById("Email").value;
    pattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    if (pattern.test(mail)) {
        return true;
    }
    document.getElementById("EmailSpan").innerHTML = "Invalid Mail Adress";
    return false;
}
function validateFirstName() {
    var firstName = document.getElementById("FirstName").value;
    if(firstName.length >= 2 && firstName.length <= 50)
    {
        return true;
    }
    document.getElementById("FirstNameSpan").innerHTML = "First Name lenght must be between 2 and 50";
    return false;
}
function validateLastName() {
    var lastName = document.getElementById("LastName").value;
    if (lastName.length >= 2 && lastName.length <= 50) {
        return true;
    }
    document.getElementById("LastNameSpan").innerHTML = "Last Name lenght must be between 2 and 50";
    return false;
}
function validateUserName() {
    var userName = document.getElementById("UserName").value;
    if (userName.length >= 5 && userName.length <= 15) {
        return true;
    }
    document.getElementById("UserNameSpan").innerHTML = "UserName Name lenght must be between 5 and 15";
    return false;
}
function validatePassword() {
    var password = document.getElementById("Password").value;
    var passwordValidation = document.getElementById("PasswordValidation").value;
    if (password.length >= 6 && password.length <= 25 &&password.localeCompare(passwordValidation)==0)
    {
        return true;
    }
    document.getElementById("PasswordSpan").innerHTML = "Passwords must be same and between 6-25 characters";
    return false;
}