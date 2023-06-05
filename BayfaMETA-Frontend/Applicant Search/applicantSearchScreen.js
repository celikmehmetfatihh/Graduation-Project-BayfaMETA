function getParameterByName(name, url) {
  if (!url) url = window.location.href;
  name = name.replace(/[\[\]]/g, "\\$&");
  var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
    results = regex.exec(url);
  if (!results) return null;
  if (!results[2]) return "";
  return decodeURIComponent(results[2].replace(/\+/g, " "));
}

var positionID = getParameterByName("positionID");

getApplicant();
async function getApplicant() {
  try {
    const response = await fetch(
      "https://localhost:7284/api/Position/GetPositionsWithUsers/" + positionID
    );

    const dataJSON = await response.json();
    if (!response.ok) {
      console.log("error");
      return;
    }
    data = dataJSON.data;
    renderApplicants(data);
  } catch (err) {
    console.log(err);
  }
}

async function renderApplicants(applicantObj) {
  let html = "";
  for (i = 0; i < applicantObj.length; i++) {
    html += `
        <tr>
        <td scope="col">${applicantObj[i].name}</td>
        <td scope="col">${applicantObj[i].surname}</td>
        <td scope="col">${applicantObj[i].email}</td>
        <td scope="col">${applicantObj[i].tel_no}</td>
        
        
        `;

    try {
      const response2 = await fetch(
        `https://localhost:7284/api/Users/GetCompletionOfUser/${applicantObj[i].id}/${positionID}`
      );

      const dataJSON = await response2.json();
      if (!response2.ok) {
        console.log("error");
        return;
      }
      data = dataJSON.data;
      console.log(data);
    } catch (err) {
      console.log(err);
    }
    var full = applicantObj[i].name + " " + applicantObj[i].surname;
    console.log(full);
    if (data.isSecondStageFinished) {
      html += `<td scope="col">Passed Second Stage Check Overall Results</td>`;
    } else if (!data.isFirstStagePassed) {
      html += `<td scope="col">Eliminated in First Stage</td>`;
    } else {
      html += `<td scope="col">Recruitment is in Process</td>`;
    }

    html += `
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
    td1 = tr[i].getElementsByTagName("td")[0];
    td2 = tr[i].getElementsByTagName("td")[1];

    if (td1 || td2) {
      txtValue1 = td1.textContent || td1.innerText;
      txtValue2 = td2.textContent || td2.innerText;
      if (
        txtValue1.toUpperCase().indexOf(filter) > -1 ||
        txtValue2.toUpperCase().indexOf(filter) > -1
      ) {
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
