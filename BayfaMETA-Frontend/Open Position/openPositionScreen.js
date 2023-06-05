getApplicant();

// fetch get part, I am getting all positions
async function getApplicant() {
  try {
    const response = await fetch(
      "https://localhost:7284/api/Position/GetAllAvailablePositions"
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
function renderPositions(data) {
  let html = "";
  for (i = 0; i < data.length; i++) {
    html += `
            <tr>
            <td scope="col">${data[i].jobTitle}</td>
            <td scope="col">${data[i].jobDescription}</td>
            
            <td scope="col">
            <a id="moreInfoButton" href="../CV Form/cvForm.html?positionID=${data[i].id}" >Click Here to Apply</a>
            </td>
            
            </tr>
            
            `;
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
