document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault(); // Prevent form submission

    // Clear previous error messages
    document.getElementById('loginStudentNumberError').innerText = "";
    document.getElementById('loginPasswordError').innerText = "";

    let isValid = true; // Overall validation flag

    // Get input values
    const studentNumber = document.getElementById('loginStudentNumber').value.trim();
    const password = document.getElementById('loginPassword').value;

    /* ----------------------
       VALIDATION RULES
    ---------------------- */
    
    // Student Number Validation
    if (studentNumber === "") {
        document.getElementById('loginStudentNumberError').innerText = "Student number is required.";
        isValid = false;
    } else if (!/^\d{8}$/.test(studentNumber)) {
        document.getElementById('loginStudentNumberError').innerText = "Student number must be 8 digits.";
        isValid = false;
    }

    // Password Validation
    if (password === "") {
        document.getElementById('loginPasswordError').innerText = "Password is required.";
        isValid = false;
    } else if (password.length < 8) {
        document.getElementById('loginPasswordError').innerText = "Password must be at least 8 characters.";
        isValid = false;
    }

    /* ----------------------
       SUBMIT IF VALID
    ---------------------- */
    if (isValid) {
        // Disable button during processing
        const loginBtn = document.getElementById('loginButton');
        loginBtn.disabled = true;
        loginBtn.textContent = "Logging in...";
        
        // Here you would normally send data to server
        // For now, we'll simulate a successful login
        setTimeout(function() {
            alert("Login successful!");
            loginBtn.disabled = false;
            loginBtn.textContent = "Login";
            
            // Redirect to dashboard or clear form
            // window.location.href = "dashboard.html";
            document.getElementById('loginForm').reset();
        }, 1500); // Simulate network delay
    }

    fetch("http://localhost:5264/api/auth/login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
        studentNumber: studentNumber,
        password: password
    })
})
.then(res => res.json())
.then(data => {
    localStorage.setItem("user", JSON.stringify(data));
    window.location.href = "dashboard.html";
})
.catch(err => {
    alert("Login failed: " + err.message);
});

});