// Get the user ID and position ID from the URL
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const positionId = urlParams.get("positionId");

getInterviewWithScores();

async function getInterviewWithScores() {
  try {
    const response = await fetch(
      "https://localhost:7284/api/Scores/GetOverallScores/" + positionId
    );
    const dataJSON = await response.json();
    const reqData = dataJSON.data;
    populateTable(reqData);
  } catch (err) {
    console.log(err);
  }
}

function populateTable(data) {
  const tbody = document.querySelector(".table tbody");

  data.forEach((applicant) => {
    const tr = document.createElement("tr");

    const nameCell = document.createElement("td");
    nameCell.textContent = applicant.fullName;
    tr.appendChild(nameCell);

    const emailCell = document.createElement("td");
    emailCell.textContent = applicant.email;
    tr.appendChild(emailCell);

    const telNoCell = document.createElement("td");
    telNoCell.textContent = applicant.telNo;
    tr.appendChild(telNoCell);

    const keyMatchingScoreCell = document.createElement("td");
    keyMatchingScoreCell.textContent = applicant.resumeMatching;
    keyMatchingScoreCell.classList.add("part1", "cv-matching-score");
    tr.appendChild(keyMatchingScoreCell);

    const cvScoreCell = document.createElement("td");
    cvScoreCell.textContent = applicant.resumeTotalScore;
    cvScoreCell.classList.add("part1", "cv-score");
    tr.appendChild(cvScoreCell);

    const interviewScoreCell = document.createElement("td");
    interviewScoreCell.textContent = applicant.videoScore.toFixed(2);
    interviewScoreCell.classList.add("part2", "interview-score");
    tr.appendChild(interviewScoreCell);

    const technicalScoreCell = document.createElement("td");
    technicalScoreCell.textContent = applicant.technicalScore;
    technicalScoreCell.classList.add("part2", "technical-score");
    tr.appendChild(technicalScoreCell);

    const overallScoreCell = document.createElement("td");
    overallScoreCell.textContent = applicant.overallScore.toFixed(2);
    overallScoreCell.classList.add("part3", "overall-score");
    tr.appendChild(overallScoreCell);

    tbody.appendChild(tr);
  });
}

let filter = document.getElementById("filter");
let table = document.querySelector(".table table");
let ths = table.querySelectorAll("th");

// Sort by default option
let defaultSortOption = document.querySelector('#sort option[value="default"]');
defaultSortOption.selected = true;

filter.addEventListener("change", function () {
  let value = this.value;
  let cells = table.querySelectorAll("td");
  let sortOptionsHtml = "";

  // Generate the HTML for the available sort options based on the selected filter option
  if (value === "all") {
    sortOptionsHtml += '<option value="default">Sort By</option>';
    sortOptionsHtml +=
      '<option value="cv-matching-score">Keyword Matching Score</option>';
    sortOptionsHtml += '<option value="cv-score">CV Score</option>';
    sortOptionsHtml +=
      '<option value="interview-score">Interview Score</option>';
    sortOptionsHtml +=
      '<option value="technical-score">Technical Score</option>';
    sortOptionsHtml += '<option value="overall-score">Overall Score</option>';
  } else if (value === "part1") {
    sortOptionsHtml += '<option value="default">Sort By</option>';
    sortOptionsHtml +=
      '<option value="cv-matching-score">Keyword Matching Score</option>';
    sortOptionsHtml += '<option value="cv-score">CV Score</option>';
  } else if (value === "part2") {
    sortOptionsHtml += '<option value="default">Sort By</option>';
    sortOptionsHtml +=
      '<option value="interview-score">Interview Score</option>';
    sortOptionsHtml +=
      '<option value="technical-score">Technical Score</option>';
  } else if (value === "part3") {
    sortOptionsHtml += '<option value="default">Sort By</option>';
    sortOptionsHtml += '<option value="overall-score">Overall Score</option>';
  }

  // Update the sort select element with the available sort options
  document.getElementById("sort").innerHTML = sortOptionsHtml;

  // Show all columns first
  ths.forEach(function (th) {
    th.classList.remove("hide");
  });

  cells.forEach(function (cell) {
    if (
      (cell.cellIndex !== 0) &
      (cell.cellIndex !== 1) &
      (cell.cellIndex !== 2)
    ) {
      if (value === "all") {
        cell.classList.remove("hide");
      } else if (value === "part1") {
        if (
          cell.classList.contains("cv-matching-score") ||
          cell.classList.contains("cv-score")
        ) {
          cell.classList.remove("hide");
        } else {
          cell.classList.add("hide");
          ths[cell.cellIndex].classList.add("hide");
        }
      } else if (value === "part2") {
        if (
          cell.classList.contains("interview-score") ||
          cell.classList.contains("technical-score")
        ) {
          cell.classList.remove("hide");
        } else {
          cell.classList.add("hide");
          ths[cell.cellIndex].classList.add("hide");
        }
      } else if (value === "part3") {
        if (cell.classList.contains("overall-score")) {
          cell.classList.remove("hide");
        } else {
          cell.classList.add("hide");
          ths[cell.cellIndex].classList.add("hide");
        }
      }
    }
  });
});

let sort = document.getElementById("sort");
sort.addEventListener("change", function () {
  let value = this.value;
  let rows = Array.from(table.querySelectorAll("tr")).slice(1);

  // Get the index of the selected column
  let colIndex = Array.from(ths).findIndex(function (th) {
    return th.id === value;
  });

  rows.sort(function (rowA, rowB) {
    let cellA = rowA.querySelectorAll("td")[colIndex];
    let cellB = rowB.querySelectorAll("td")[colIndex];
    if (value === "Sort By") {
      return 0;
    } else if (value === "cv-score") {
      // Reverse order for cv-score column
      return parseFloat(cellB.textContent) - parseFloat(cellA.textContent);
    } else {
      return parseFloat(cellB.textContent) - parseFloat(cellA.textContent);
    }
  });

  // Append sorted rows back to table
  table.querySelector("tbody").innerHTML = "";
  rows.forEach(function (row) {
    table.querySelector("tbody").appendChild(row);
  });
});

let searchInput = document.querySelector(".search-container input");
searchInput.addEventListener("input", function () {
  let searchText = this.value.toLowerCase();
  let rows = table.querySelectorAll("tbody tr");

  rows.forEach(function (row) {
    let nameCell = row.querySelector("td:first-child");
    let nameText = nameCell.textContent.toLowerCase();

    let emailCell = row.querySelector("td:nth-child(2)");
    let emailText = emailCell.textContent.toLowerCase();

    let telNoCell = row.querySelector("td:nth-child(3)");
    let telNoText = telNoCell.textContent.toLowerCase();

    if (
      nameText.includes(searchText) ||
      emailText.includes(searchText) ||
      telNoText.includes(searchText)
    ) {
      row.classList.remove("hide");
    } else {
      row.classList.add("hide");
    }
  });
});
