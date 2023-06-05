// Alert the user if the email or password is invalid
function inputWarning(isVis) {
  const x = document.getElementById("passwordInput");
  passwordValue = x.value;
  var size = Object.keys(passwordValue).length;
  //check whether the size of the password is enough or not
  if (size < 8) {
    html = `
    <p class="alert alert-danger ng-binding login-validation" style="font-size:12px">Invalid password length. Please try again!</p>
      `;
    document.querySelector("#warnings").innerHTML = html;
  } else {
    if (!isVis) {
      html = `
      <p class="alert alert-danger ng-binding login-validation" style="font-size:12px">Invalid email or password. Please try again!</p>
        `;
      document.querySelector("#warnings").innerHTML = html;
    }
  }
}

// Make the password visible when the user clicks on the eye icon
function showPassword() {
  var x = document.getElementById("passwordInput");
  if (x.type === "password") {
    x.type = "text";
  } else {
    x.type = "password";
  }
}

login();

function login() {
  var form = document.getElementById("form");

  form.addEventListener("submit", function (e) {
    e.preventDefault();

    var email = document.getElementById("emailInput").value;
    var password = document.getElementById("passwordInput").value;

    // Check if the email is valid
    async function getRole() {
      try {
        const response = await fetch("https://localhost:7284/api/Users/Login", {
          method: "POST",
          body: JSON.stringify({
            email: email,
            password: password,
          }),
          headers: {
            "Content-Type": "application/json; charset=utf-8",
          },
        });

        const data = await response.json();
        if (!response.ok) {
          console.log("error: " + data.message);
          return;
        }

        var role = data.data.role;
        var userId = data.data.id;

        if (role == "applicant") {
          localStorage.setItem("userId", userId);
          window.open("../Home Page/applicantHome.html", "_self");
        } else {
          window.open("../Home Page/homePage.html", "_self");
        }

        inputWarning(true);
      } catch (err) {
        console.log("error: " + err);
        inputWarning(false);
      }
    }

    getRole();
  });
}
