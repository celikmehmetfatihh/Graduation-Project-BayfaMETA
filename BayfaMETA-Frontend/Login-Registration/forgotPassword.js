const forgotPasswordForm = document.getElementById("forgotPasswordForm");

forgotPasswordForm.addEventListener("submit", async function (e) {
  e.preventDefault();

  const email = document.getElementById("emailInput").value;
  const emailAddress = {
    Email: email,
  };

  console.log(emailAddress);
  const response = await sendEmailAddress(emailAddress);
  console.log(response);

  displayPopup();
});

async function sendEmailAddress(emailAddress) {
  try {
    const response = await fetch("http://localhost:5000/ResetPassword", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(emailAddress),
    });
    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error sending email address:", error);
  }
}

// Function to display the popup
function displayPopup() {
  console.log("Displaying popup");
  document.getElementById("popup").style.display = "block";
}

// Close the popup when the close button is clicked
document.getElementById("close-popup").addEventListener("click", function () {
  console.log("Closing popup");
  document.getElementById("popup").style.display = "none";
  document.getElementById("emailInput").value = "";
});
