const form = document.getElementById("registerForm");

form.onsubmit = async (e) => {
  e.preventDefault();

  // Validate checkbox
  if (!document.getElementById("acceptCheckbox").checked) {
    alert("Please accept the Terms of Service and Privacy Policy.");
    return;
  }

  if (!validate()) {
    return;
  }

  const name = document.getElementById("nameInput").value;
  const surname = document.getElementById("surnameInput").value;
  const phoneNumber = document.getElementById("telInput").value;
  const email = document.getElementById("emailInput").value;
  const password = document.getElementById("passwordInput").value;

  const data = {
    Name: name,
    Surname: surname,
    PhoneNumber: phoneNumber,
    Email: email,
    Password: password,
  };

  console.log(data);

  try {
    const response = await fetch("https://localhost:7284/api/Users/Register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });

    const dataJSON = await response.json();

    if (!response.ok) {
      console.log("error");
      return;
    }

    console.log(dataJSON);

    alert("Registration successful");
    window.location.href = "../Login-Registration/login.html";
  } catch (err) {
    console.log(err);
  }
};

function showPassword() {
  var x = document.getElementById("passwordInput");
  if (x.type === "password") {
    x.type = "text";
  } else {
    x.type = "password";
  }
}

function validate() {
  var name = document.getElementById("nameInput").value;
  var surname = document.getElementById("surnameInput").value;
  var phoneNumber = document.getElementById("telInput").value;
  var email = document.getElementById("emailInput").value;
  var password = document.getElementById("passwordInput").value;

  if (name == "") {
    alert("Please enter your name");
    return false;
  } else if (surname == "") {
    alert("Please enter your surname");
    return false;
  } else if (phoneNumber == "") {
    alert("Please enter your phone number");
    return false;
  } else if (email == "") {
    alert("Please enter your email");
    return false;
  } else if (password == "") {
    alert("Please enter your password");
    return false;
  } else if (password.length < 8) {
    alert("Password must be at least 8 characters long");
    return false;
  }

  return true;
}
