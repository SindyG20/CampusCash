document.getElementById('registrationForm').addEventListener('submit', function(event) {
    event.preventDefault(); // Prevent form from submitting

    // Clear previous error messages
    document.getElementById('StudentIDError').innerText = "";
    document.getElementById('StudentNumberError').innerText = "";
    document.getElementById('FullNameError').innerText = "";
    document.getElementById('DOBError').innerText = "";
    document.getElementById('AddressError').innerText = "";
    document.getElementById('emailError').innerText = "";
    document.getElementById('AccountTypeError').innerText = "";
    document.getElementById('passwordError').innerText = "";
    document.getElementById('confirmPasswordError').innerText = "";

    let isValid = true; // Overall form validation flag

    // Fetch input values
    const studentID = document.getElementById('StudentID').value.trim();
    const studentNumber = document.getElementById('StudentNumber').value.trim();
    const fullName = document.getElementById('FullName').value.trim();
    const dob = document.getElementById('DOB').value;
    const address = document.getElementById('Address').value.trim();
    const email = document.getElementById('email').value.trim();
    const accountType = document.getElementById('AccountType').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    // Student ID Validation
    if (studentID === "") {
        document.getElementById('StudentIDError').innerText = "Student ID is required.";
        isValid = false;
    }

    // Student Number Validation
    if (studentNumber === "") {
        document.getElementById('StudentNumberError').innerText = "Student Number is required.";
        isValid = false;
    }

    // Full Name Validation
    if (fullName === "") {
        document.getElementById('FullNameError').innerText = "Full Name is required.";
        isValid = false;
    }

    // Date of Birth Validation
    if (dob === "") {
        document.getElementById('DOBError').innerText = "Date of Birth is required.";
        isValid = false;
    }

    // Address Validation
    if (address === "") {
        document.getElementById('AddressError').innerText = "Address is required.";
        isValid = false;
    }

    // Email Format Validation using regex
    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if (!email.match(emailPattern)) {
        document.getElementById('emailError').innerText = "Enter a valid email address.";
        isValid = false;
    }

    // Account Type Dropdown Validation
    if (accountType === "") {
        document.getElementById('AccountTypeError').innerText = "Please select an account type.";
        isValid = false;
    }

    // Password Length Validation
    if (password.length < 6) {
        document.getElementById('passwordError').innerText = "Password must be at least 6 characters.";
        isValid = false;
    }

    // Confirm Password Match Validation
    if (password !== confirmPassword) {
        document.getElementById('confirmPasswordError').innerText = "Passwords do not match.";
        isValid = false;
    }

    // If all validations passed
    if (isValid) {
        alert("Registration Successful!");
        document.getElementById('registrationForm').reset(); // Clear form after successful submission
      
    }
});
