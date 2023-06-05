getApplicant();

// fetch get part, I am getting all positions
async function getApplicant() {
  try {
    const response = await fetch(
      "https://localhost:7284/api/Position/GetAllPositions"
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

// Open a popup window centered on the screen
function openPopupCenter(url, title, w, h) {
  // Fixes dual-screen position                         Most browsers      Firefox
  var dualScreenLeft =
    window.screenLeft != undefined ? window.screenLeft : window.screenX;
  var dualScreenTop =
    window.screenTop != undefined ? window.screenTop : window.screenY;

  var width = window.innerWidth
    ? window.innerWidth
    : document.documentElement.clientWidth
    ? document.documentElement.clientWidth
    : screen.width;
  var height = window.innerHeight
    ? window.innerHeight
    : document.documentElement.clientHeight
    ? document.documentElement.clientHeight
    : screen.height;

  var left = width / 2 - w / 2 + dualScreenLeft;
  var top = height / 2 - h / 2 + dualScreenTop;
  var newWindow = window.open(
    url,
    title,
    "scrollbars=yes, width=" +
      w +
      ", height=" +
      h +
      ", top=" +
      top +
      ", left=" +
      left
  );

  // Puts focus on the newWindow
  if (window.focus) {
    newWindow.focus();
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
        <td scope="col">${data[i].numberOfPeople}</td>
        <td scope="col">
        <a id="moreInfoButton" href="../Applicant Search/applicantSearchScreen.html?positionID=${data[i].id}" >ID${data[i].id}: See Applicants</a></td>`;

    if (data[i].isClosed) {
      html += `<td scope="col">
            <a href="../Applicant Scores/applicantScores.html?positionId=${data[i].id}" target="popup" onclick="openPopupCenter('../Applicant Scores/applicantScores.html?positionId=${data[i].id}', 'popup', 1000, 600); return false;">
                See Overall Results
            </a>
            </td>
            `;
    } else {
      html += `<td scope="col">Recruitment in Progress</td>`;
    }

    html += `
        
        
        </tr>
        
        `;
  }

  document.querySelector(".bodyTable").innerHTML = html;
}

function searchFunction() {
  console.log("test");
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
