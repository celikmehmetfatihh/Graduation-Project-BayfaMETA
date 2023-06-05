const form = document.getElementById("recruitmentProcessForm");
const firstStageEndDateInput = document.getElementById("firstStageEndDate");
const secondStageEndDateInput = document.getElementById("secondStageEndDate");
const jobPositionInput = document.getElementById("jobPosition1");
const addJobPositionButton = document.getElementById("addJobPositionButton");
const requiredSkillInput = document.getElementById("requiredSkill1");
const addRequiredSkillButton = document.getElementById(
  "addRequiredSkillButton"
);
const interviewTopicInput = document.getElementById("interviewTopic1");
const addInterviewTopicButton = document.getElementById(
  "addInterviewTopicButton"
);
const fileInput = document.getElementById("excelFile");

function validateForm() {
  if (secondStageEndDateInput < firstStageEndDateInput) {
    alert("Second stage end date must be after first stage end date.");
  }

  if (jobPositionsList.length === 0) {
    alert("At least one job position is needed.");
    return false;
  }

  if (requiredSkillsList.length === 0) {
    alert("At least one required skill is needed.");
    return false;
  }

  if (interviewTopicsList.length === 0) {
    alert("At least one interview topic is needed.");
    return false;
  }

  if (!checkMultipliers()) {
    return false;
  }

  const file = fileInput.files[0];
  const fileExtension = file.name.split(".").pop().toLowerCase();
  if (fileExtension !== "xls" && fileExtension !== "xlsx") {
    alert("Please upload a valid Excel file (.xls or .xlsx).");
    return false;
  }

  return true;
}

let jobPositionsList = [];
function updateJobPositionsDisplay() {
  const jobPositionListElement = document.getElementById("jobPositionsList");
  jobPositionListElement.innerHTML = "";

  jobPositionsList.forEach((position, index) => {
    const positionElement = document.createElement("div");
    positionElement.innerHTML = `
      <span style="color: black">${position}</span>
      <button type="button" class="remove-position-button" data-index="${index}">X</button>
    `;
    jobPositionListElement.appendChild(positionElement);
  });

  const removePositionButtons = document.getElementsByClassName(
    "remove-position-button"
  );
  for (const button of removePositionButtons) {
    button.addEventListener("click", () => {
      const index = button.dataset.index;
      jobPositionsList.splice(index, 1);
      updateJobPositionsDisplay();
    });
  }
}

addJobPositionButton.addEventListener("click", () => {
  const newPositions = jobPositionInput.value.trim().split(",");
  let matchingPositions = [];
  let addedPositions = [];

  newPositions.forEach((newPosition) => {
    if (newPosition.trim() === "") return;
    if (jobPositionsList.includes(newPosition.trim())) {
      if (!addedPositions.includes(newPosition.trim())) {
        matchingPositions.push(newPosition.trim());
        addedPositions.push(newPosition.trim());
      }
    } else {
      jobPositionsList.push(newPosition.trim());
    }
  });

  if (matchingPositions.length > 0) {
    alert(
      "The following positions are already in the list: " +
        matchingPositions.join(", ")
    );
  }

  updateJobPositionsDisplay();
  jobPositionInput.value = "";
});

let requiredSkillsList = [];
function updateRequiredSkillsDisplay() {
  const skillListElement = document.getElementById("requiredSkillsList");
  skillListElement.innerHTML = "";

  requiredSkillsList.forEach((skill, index) => {
    const skillElement = document.createElement("div");
    skillElement.innerHTML = `
      <span style="color: black">${skill}</span>
      <button type="button" class="remove-skill-button" data-index="${index}">X</button>
    `;
    skillListElement.appendChild(skillElement);
  });

  const removeSkillButtons = document.getElementsByClassName(
    "remove-skill-button"
  );
  for (const button of removeSkillButtons) {
    button.addEventListener("click", () => {
      const index = button.dataset.index;
      requiredSkillsList.splice(index, 1);
      updateRequiredSkillsDisplay();
    });
  }
}

addRequiredSkillButton.addEventListener("click", () => {
  const newSkills = requiredSkillInput.value.trim().split(",");
  let matchingSkills = [];
  let addedSkills = [];

  newSkills.forEach((newSkill) => {
    if (newSkill.trim() === "") return;
    if (requiredSkillsList.includes(newSkill.trim())) {
      if (!addedSkills.includes(newSkill.trim())) {
        matchingSkills.push(newSkill.trim());
        addedSkills.push(newSkill.trim());
      }
    } else {
      requiredSkillsList.push(newSkill.trim());
    }
  });

  if (matchingSkills.length > 0) {
    alert(
      "The following skills are already in the list: " +
        matchingSkills.join(", ")
    );
  }

  updateRequiredSkillsDisplay();
  requiredSkillInput.value = "";
});

let interviewTopicsList = [];
function updateInterviewTopicsDisplay() {
  const interviewTopicsElement = document.getElementById("interviewTopicsList");
  interviewTopicsElement.innerHTML = "";

  interviewTopicsList.forEach((topic, index) => {
    const topicElement = document.createElement("div");
    topicElement.innerHTML = `
      <span style="color: black">${topic}</span>
      <button type="button" class="remove-topic-button" data-index="${index}">X</button>
    `;
    interviewTopicsElement.appendChild(topicElement);
  });

  const removeTopicButtons = document.getElementsByClassName(
    "remove-topic-button"
  );
  for (const button of removeTopicButtons) {
    button.addEventListener("click", () => {
      const index = button.dataset.index;
      interviewTopicsList.splice(index, 1);
      updateInterviewTopicsDisplay();
    });
  }
}

addInterviewTopicButton.addEventListener("click", () => {
  const newTopics = interviewTopicInput.value.trim().split(",");
  let matchingTopics = [];
  let addedTopics = [];

  newTopics.forEach((newTopic) => {
    if (newTopic.trim() === "") return;
    if (interviewTopicsList.includes(newTopic.trim())) {
      if (!addedTopics.includes(newTopic.trim())) {
        matchingTopics.push(newTopic.trim());
        addedTopics.push(newTopic.trim());
      }
    } else {
      interviewTopicsList.push(newTopic.trim());
    }
  });

  if (matchingTopics.length > 0) {
    alert(
      "The following topics are already in the list: " +
        matchingTopics.join(", ")
    );
  }

  updateInterviewTopicsDisplay();
  interviewTopicInput.value = "";
});

function checkMultipliers() {
  const TechnicalTestMultiplier = parseFloat(
    document.getElementById("technicalTestMultiplier").value
  );
  const VideoInterviewMultiplier = parseFloat(
    document.getElementById("videoInterviewMultiplier").value
  );
  const ResumeMultiplier = parseFloat(
    document.getElementById("resumeMultiplier").value
  );
  const EduMultiplier = parseFloat(
    document.getElementById("eduMultiplier").value
  );
  const ExpMultiplier = parseFloat(
    document.getElementById("expMultiplier").value
  );
  const TechSkillsMultiplier = parseFloat(
    document.getElementById("techSkillsMultiplier").value
  );
  const SoftSkillsMultiplier = parseFloat(
    document.getElementById("softSkillsMultiplier").value
  );
  const KeywordMultiplier = parseFloat(
    document.getElementById("keywordMultiplier").value
  );

  const totalOverall =
    ResumeMultiplier + VideoInterviewMultiplier + TechnicalTestMultiplier;

  const totalScore =
    EduMultiplier +
    ExpMultiplier +
    TechSkillsMultiplier +
    SoftSkillsMultiplier +
    KeywordMultiplier;

  if (totalOverall !== 100) {
    alert("The total of all score multipliers must be equal to 100.");
    return false;
  } else if (totalScore !== 100) {
    alert("The total of all score multipliers must be equal to 100.");
    return false;
  }

  return true;
}

async function sendRecruitmentProcess(recruitmentProcess) {
  const url = "https://localhost:7284/api/Position";

  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(recruitmentProcess),
  });

  if (response.ok) {
    const data = await response.json();
    console.log("Success:", data);
  } else {
    console.log("Error:", response.status, response.statusText);
  }
}

form.addEventListener("submit", async (event) => {
  console.log("Form submitted");
  event.preventDefault();

  if (!validateForm()) return;

  const JobTitle = document.getElementById("jobTitle").value;
  const JobDescription = document.getElementById("jobDescription").value;
  const NumberOfPeople = parseInt(
    document.getElementById("numberOfPeople").value
  );
  const firstStageEndDatePicker = flatpickr("#firstStageEndDate");
  const secondStageEndDatePicker = flatpickr("#secondStageEndDate");
  const FirstStageEndDate = firstStageEndDatePicker.selectedDates[0];
  const SecondStageEndDate = secondStageEndDatePicker.selectedDates[0];
  const TechnicalTestMultiplier = parseFloat(
    document.getElementById("technicalTestMultiplier").value
  );
  const VideoInterviewMultiplier = parseFloat(
    document.getElementById("videoInterviewMultiplier").value
  );
  const ResumeMultiplier = parseFloat(
    document.getElementById("resumeMultiplier").value
  );
  const JobPositions = jobPositionsList.join(";");
  const RequiredSkills = requiredSkillsList.join(";");
  const MinExperience = parseInt(
    document.getElementById("minExperience").value
  );
  const StageOneThreshold = parseInt(
    document.getElementById("firstStageThreshold").value
  );
  const EduMultiplier = parseFloat(
    document.getElementById("eduMultiplier").value
  );
  const ExpMultiplier = parseFloat(
    document.getElementById("expMultiplier").value
  );
  const TechSkillsMultiplier = parseFloat(
    document.getElementById("techSkillsMultiplier").value
  );
  const SoftSkillsMultiplier = parseFloat(
    document.getElementById("softSkillsMultiplier").value
  );
  const KeywordMultiplier = parseFloat(
    document.getElementById("keywordMultiplier").value
  );
  const InterviewTopics = interviewTopicsList.join(";");
  const AllocatedTime = parseInt(
    document.getElementById("allocatedTime").value
  );
  const QuestionTime = parseInt(document.getElementById("questionTime").value);
  const OpennessMultiplier = parseFloat(
    document.getElementById("openness").value
  );
  const ConscientiousnessMultiplier = parseFloat(
    document.getElementById("conscientiousness").value
  );
  const ExtraversionMultiplier = parseFloat(
    document.getElementById("extraversion").value
  );
  const AgreeablenessMultiplier = parseFloat(
    document.getElementById("agreeableness").value
  );
  const NeuroticismMultiplier = parseFloat(
    document.getElementById("neuroticism").value
  );
  const Path = document.getElementById("excelFile").value;
  var ExcelPath = Path.replace(/C:\\fakepath\\/, "C:\\");

  const recruitmentProcess = {
    JobTitle,
    JobDescription,
    NumberOfPeople,
    FirstStageEndDate,
    SecondStageEndDate,
    TechnicalTestMultiplier,
    VideoInterviewMultiplier,
    ResumeMultiplier,
    JobPositions,
    RequiredSkills,
    MinExperience,
    StageOneThreshold,
    EduMultiplier,
    ExpMultiplier,
    TechSkillsMultiplier,
    SoftSkillsMultiplier,
    KeywordMultiplier,
    InterviewTopics,
    AllocatedTime,
    QuestionTime,
    OpennessMultiplier,
    ConscientiousnessMultiplier,
    ExtraversionMultiplier,
    AgreeablenessMultiplier,
    NeuroticismMultiplier,
    ExcelPath,
  };

  // Log the recruitment process object to the console
  console.log(recruitmentProcess);

  // Send the recruitment process data to the server
  await sendRecruitmentProcess(recruitmentProcess);

  // Display the popup
  displayPopup();
});

// Function to display the popup
function displayPopup() {
  console.log("Displaying popup");
  document.getElementById("recruitment-process-popup").style.display = "block";
}

// Close the popup when the close button is clicked
document.getElementById("close-popup").addEventListener("click", function () {
  closePopupAndResetForm();
});

// Function to close the popup and reset the form
function closePopupAndResetForm() {
  console.log("Close button clicked");
  document.getElementById("recruitment-process-popup").style.display = "none";

  // Clear lists and update display
  jobPositionsList = [];
  requiredSkillsList = [];
  interviewTopicsList = [];
  updateJobPositionsDisplay();
  updateRequiredSkillsDisplay();
  updateInterviewTopicsDisplay();

  // Reset input values to default after form submission
  resetFormInputs();
}

// Function to reset form inputs
function resetFormInputs() {
  jobPositionInput.value = "";
  requiredSkillInput.value = "";
  interviewTopicInput.value = "";
  document.getElementById("jobTitle").value = "";
  document.getElementById("jobDescription").value = "";
  document.getElementById("numberOfPeople").value = "1";
  document.getElementById("firstStageEndDate").value = "";
  document.getElementById("secondStageEndDate").value = "";
  document.getElementById("technicalTestMultiplier").value = "30";
  document.getElementById("videoInterviewMultiplier").value = "30";
  document.getElementById("resumeMultiplier").value = "40";
  document.getElementById("minExperience").value = "1";
  document.getElementById("firstStageThreshold").value = "100";
  document.getElementById("eduMultiplier").value = "20";
  document.getElementById("expMultiplier").value = "20";
  document.getElementById("techSkillsMultiplier").value = "20";
  document.getElementById("softSkillsMultiplier").value = "20";
  document.getElementById("keywordMultiplier").value = "20";
  document.getElementById("allocatedTime").value = "5";
  document.getElementById("questionTime").value = "30";
  document.getElementById("openness").value = "20";
  document.getElementById("conscientiousness").value = "20";
  document.getElementById("extraversion").value = "20";
  document.getElementById("agreeableness").value = "20";
  document.getElementById("neuroticism").value = "20";
  document.getElementById("excelFile").value = "";
}

const jobTitleSuggestions = document.getElementById("jobTitleSuggestions");
const jobTitleOptions = Array.from(jobTitleSuggestions.options);

jobTitleSuggestions.addEventListener("input", () => {
  const searchQuery = jobTitleSuggestions.value.toLowerCase();
  const filteredOptions = jobTitleOptions.filter((option) => {
    return option.value.toLowerCase().includes(searchQuery);
  });

  jobTitleSuggestions.innerHTML = "";

  filteredOptions.forEach((option) => {
    jobTitleSuggestions.appendChild(option);
  });
});

fetch(
  "https://raw.githubusercontent.com/jneidel/job-titles/master/job-titles.txt"
)
  .then((response) => response.text())
  .then((data) => {
    const jobTitles = data.split("\n").map((title) => {
      // Capitalize the first letter of each word in the title and remove extra spacing
      return title
        .toLowerCase()
        .split(" ")
        .map((word) => {
          return word.charAt(0).toUpperCase() + word.slice(1);
        })
        .join(" ")
        .trim();
    });
    for (const title of jobTitles) {
      const option = document.createElement("option");
      option.value = title;
      jobTitleSuggestions.appendChild(option);
    }
  })
  .catch((error) => console.error(error));

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
