getApplicant();

// fetch get part, I am getting all positions
async function getApplicant() {
  var userId = localStorage.getItem("userId");

  try {
    const response = await fetch(
      `https://localhost:7284/api/Users/GetAppliedPositionsOfAUser/${userId}`
    );

    const dataJSON = await response.json();
    if (!response.ok) {
      console.log("error");
      return;
    }
    data = dataJSON.data;
    console.log(data);

    renderPositions(data);
  } catch (err) {
    console.log(err);
  }
}

// I have divide applicants based on the position IDs
async function renderPositions(dataIn) {
  let html = "";
  for (i = 0; i < dataIn.length; i++) {
    html += `
            <tr>
            <td scope="col">${dataIn[i].jobTitle}</td>
            <td scope="col">${dataIn[i].jobDescription}</td>
            <td scope="col">You have passed the first stage! Well Done!</td>
            <td scope="col">
                
      
            `;

    var userId = localStorage.getItem("userId");

    try {
      const response = await fetch(
        `https://localhost:7284/api/Users/IsUserInPosition/${userId}/${dataIn[i].id}`
      );

      const dataJSON = await response.json();
      if (!response.ok) {
        console.log("error");
        return;
      }
      data = dataJSON.data;
      console.log(data);
    } catch (err) {
      console.log(err);
    }

    if (!data.isVideoInterviewCompleted || !data.isTechnicalQuestionCompleted) {
      html += `<select onchange="if (this.value) window.location.href=this.value;">
                <option value="">Select a Task to Complete</option>`;
    } else if (
      data.isVideoInterviewCompleted &&
      data.isVideoInterviewCompleted
    ) {
      html += `All Tasks Completed`;
    }
    if (!data.isVideoInterviewCompleted) {
      html += `<option  value="../Video Interview Submission/videoInterviewSubmission.html?positionId=${dataIn[i].id}">Upload Video Interview</option>`;
    }
    if (!data.isTechnicalQuestionCompleted) {
      html += `<option  value="../Technical Test/technicalTest.html?positionId=${dataIn[i].id}">Solve Technical Test</option>`;
    }

    if (!data.isVideoInterviewCompleted || !data.isTechnicalQuestionCompleted) {
      html += `</select>`;
    }
    html += `
            </td>

            
            </tr>`;
  }

  document.querySelector(".bodyTable").innerHTML = html;
}

function searchFunction() {
  // Declare variables
  var input, filter, table, tr, td, i, txtValue;
  input = document.getElementById("txtSearch");
  filter = input.value.toUpperCase();
  table = document.getElementById("myTable");
  tr = table.getElementsByTagName("tr");

  // Loop through all table rows, and hide those who don't match the search query
  for (i = 0; i < tr.length; i++) {
    td = tr[i].getElementsByTagName("td")[0];

    if (td) {
      txtValue = td.textContent || td.innerText;

      if (txtValue.toUpperCase().indexOf(filter) > -1) {
        tr[i].style.display = "";
      } else {
        tr[i].style.display = "none";
      }
    }
  }
}

document
  .getElementById("sidebarToggle")
  .addEventListener("click", toggleSidebar);
document
  .getElementById("sidebarToggleTop")
  .addEventListener("click", toggleSidebar);

function toggleSidebar() {
  let logo = document.getElementById("logo");
  let menuText = document.getElementById("menuText");
  let resizeableElements = document.querySelectorAll(".resizeable-text");
  let sidebarBrands = document.querySelectorAll(".sidebar .sidebar-brand");

  if (menuText.style.fontSize === "small") {
    logo.style.height = "6rem";
    sidebarBrands.forEach((sidebarBrand) => {
      sidebarBrand.style.height = "8rem";
    });
    menuText.style.display = "inline";
    menuText.style.fontSize = "large";
    menuText.style.textAlign = "center";
    resizeableElements.forEach(function (textElement) {
      textElement.style.fontSize = "large";
    });
  } else {
    logo.style.height = "3rem";
    sidebarBrands.forEach((sidebarBrand) => {
      sidebarBrand.style.height = "5rem";
    });
    menuText.style.display = "inline";
    menuText.style.fontSize = "small";
    menuText.style.textAlign = "center";
    resizeableElements.forEach(function (textElement) {
      textElement.style.fontSize = "small";
    });
  }
}
