const form = document.getElementById("cvForm");

// Get the user ID and position ID from the URL
const userId = localStorage.getItem("userId");
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const positionId = urlParams.get("positionID");

// Validate the form
function validateForm() {
  const countryCode = document.getElementById("countryCode").value;
  const email = document.getElementById("email").value;

  // Validate country code
  if (countryCode.trim() === "") {
    alert("Please enter a valid country code");
    return false;
  }

  // Validate email address
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const knownEmailExtensions = ["com", "net", "org", "edu", "gov", "tr"];
  const emailParts = email.split(".");
  if (
    !emailRegex.test(email) ||
    !knownEmailExtensions.includes(emailParts[emailParts.length - 1])
  ) {
    console.log(emailParts[emailParts.length - 1]);
    alert(
      "Please enter a valid email address with a known extension (e.g. .com, .net, .org, etc.)"
    );
    return false;
  }

  return true;
}

form.onsubmit = async (e) => {
  e.preventDefault();

  if (!validateForm()) return;

  const formData = new FormData(form);

  const education = [];
  for (let i = 0; i < formData.getAll("degree[]").length; i++) {
    education.push({
      Institution: formData.getAll("institution[]")[i],
      Degree: formData.getAll("degree[]")[i],
      Field: formData.getAll("field[]")[i],
      GPA: parseFloat(formData.getAll("gpa[]")[i]),
    });
  }
  formData.delete("degree[]");
  formData.delete("institution[]");
  formData.delete("field[]");
  formData.delete("gpa[]");
  formData.append("education", JSON.stringify(education));

  const experience = [];
  for (let i = 0; i < formData.getAll("company[]").length; i++) {
    experience.push({
      Company: formData.getAll("company[]")[i],
      Duration: formData.getAll("duration[]")[i],
      Title: formData.getAll("title[]")[i],
      SkillsUsed: formData.getAll("skill[]")[i].split(","),
    });
  }

  formData.delete("company[]");
  formData.delete("duration[]");
  formData.delete("title[]");
  formData.delete("skill[]");
  formData.append("experience", JSON.stringify(experience));

  for (data of formData.entries()) {
    console.log(data);
  }

  const cvForm = {
    UserId: userId,
    PositionId: positionId,
    Name: formData.get("name"),
    Email: formData.get("email"),
    PhoneNumber:
      formData.get("countryCode") +
      formData.get("phone2") +
      formData.get("phone3"),
    Education: education,
    Experience: experience,
    TechnicalSkills: technicalSkillsList,
    SoftSkills: softSkillsList,
  };

  console.log(cvForm);
  const response = await sendCvForm(cvForm);
  console.log(response);

  displayPopup();
};

async function sendCvForm(cvForm) {
  try {
    const response = await fetch("http://localhost:5000/CvForm", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(cvForm),
    });
    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error sending CV form:", error);
  }
}

// Technical and soft skills
function updateSkillsDisplay(
  skillsList,
  skillsElementId,
  skillInput,
  addSkillButton,
  removeSkillButtonClass
) {
  const skillsElement = document.getElementById(skillsElementId);
  skillsElement.innerHTML = "";

  skillsList.forEach((skill, index) => {
    const skillElement = document.createElement("div");
    skillElement.innerHTML = `
        <span style="color: black">${skill}</span>
        <button type="button" class="${removeSkillButtonClass}" data-index="${index}">X</button>
      `;
    skillsElement.appendChild(skillElement);
  });

  const removeSkillButtons = document.getElementsByClassName(
    removeSkillButtonClass
  );
  for (const button of removeSkillButtons) {
    button.addEventListener("click", () => {
      const index = button.dataset.index;
      skillsList.splice(index, 1);
      updateSkillsDisplay(
        skillsList,
        skillsElementId,
        skillInput,
        addSkillButton,
        removeSkillButtonClass
      );
    });
  }

  addSkillButton.addEventListener("click", () => {
    const newSkills = skillInput.value.trim().split(",");
    let matchingSkills = [];
    let addedSkills = [];

    newSkills.forEach((newSkill) => {
      if (newSkill.trim() === "") return;
      if (skillsList.includes(newSkill.trim())) {
        if (!addedSkills.includes(newSkill.trim())) {
          matchingSkills.push(newSkill.trim());
          addedSkills.push(newSkill.trim());
        }
      } else {
        skillsList.push(newSkill.trim());
      }
    });

    if (matchingSkills.length > 0) {
      alert(
        "The following skills are already in the list: " +
          matchingSkills.join(", ")
      );
    }

    updateSkillsDisplay(
      skillsList,
      skillsElementId,
      skillInput,
      addSkillButton,
      removeSkillButtonClass
    );
    skillInput.value = "";
  });
}

const technicalSkillsList = [];
const softSkillsList = [];

const technicalSkillsElementId = "technicalSkillsList";
const softSkillsElementId = "softSkillsList";

const technicalSkillInput = document.getElementById("technicalSkillInput");
const softSkillInput = document.getElementById("softSkillInput");

const addTechnicalSkillButton = document.getElementById(
  "addTechnicalSkillButton"
);
const addSoftSkillButton = document.getElementById("addSoftSkillButton");

const removeTechnicalSkillButtonClass = "remove-technicalSkill-button";
const removeSoftSkillButtonClass = "remove-softSkill-button";

updateSkillsDisplay(
  technicalSkillsList,
  technicalSkillsElementId,
  technicalSkillInput,
  addTechnicalSkillButton,
  removeTechnicalSkillButtonClass
);
updateSkillsDisplay(
  softSkillsList,
  softSkillsElementId,
  softSkillInput,
  addSoftSkillButton,
  removeSoftSkillButtonClass
);

// Education
const educationFormContainer = document.getElementById(
  "educationFormContainer"
);
const educationFormsWrapper = document.getElementById("educationFormsWrapper");
addEducationButton.onclick = () => {
  const cloneEducationFormContainer = educationFormContainer.cloneNode(true);
  const inputs = cloneEducationFormContainer.getElementsByTagName("input");
  cloneEducationFormContainer.style.borderTop = "1px solid #000000";
  cloneEducationFormContainer.style.paddingTop = "20px";
  for (const input of inputs) {
    input.value = "";
  }

  educationFormsWrapper.appendChild(cloneEducationFormContainer);
};

// Experience
const handleSkills = (e) => {
  const parentNode = e.target.parentNode.parentNode;
  const fakeInput = parentNode.querySelector("input[name='skill']");
  const input = parentNode.querySelector("input[name='skill[]'");

  let prevValue = input.value.split(",");
  prevValue = prevValue.filter((item) => item !== "");

  // Split the new comma-separated string of skills
  const newSkills = fakeInput.value.split(",");

  // Create an array to store the duplicate skills
  const duplicates = [];

  newSkills.forEach((newSkill) => {
    // Trim whitespace from the new skill
    newSkill = newSkill.trim();

    // Check if the skill already exists in the array
    if (prevValue.includes(newSkill)) {
      // Skill already exists, add to duplicates array
      duplicates.push(newSkill);
    } else {
      prevValue.push(newSkill);
      const tag = document.createElement("div");
      tag.style.display = "flex";
      tag.style.color = "black";
      tag.classList.add("usedSkillsList");

      const div = document.createElement("div");
      tag.appendChild(div);

      const span = document.createElement("span");
      span.innerHTML = newSkill;
      div.appendChild(span);

      const removeButton = document.createElement("button");
      removeButton.type = "button";
      removeButton.className = "remove-usedSkill-button";
      removeButton.innerHTML = "X";
      div.appendChild(removeButton);

      removeButton.onclick = () => {
        const prevValue = input.value.split(",");
        const newValue = prevValue.filter((item) => item !== newSkill);
        input.value = newValue.join(",");
        tag.remove();
      };

      e.target.parentNode.parentNode.parentNode.appendChild(tag);
    }
  });

  // If duplicates array is not empty, alert the user
  if (duplicates.length > 0) {
    alert(
      "The following skills are already in the list: " + duplicates.join(", ")
    );
  }

  fakeInput.value = "";
};

const addUsedSkillButton =
  document.getElementsByClassName("addUsedSkillButton")[0];
addUsedSkillButton.onclick = handleSkills;

const addExperienceButton = document.getElementById("addExperienceButton");
const experienceFormContainer = document.getElementById(
  "experienceFormContainer"
);
const experienceFormsWrapper = document.getElementById(
  "experienceFormsWrapper"
);

addExperienceButton.onclick = () => {
  const cloneExperienceFormContainer = experienceFormContainer.cloneNode(true);
  cloneExperienceFormContainer.style.borderTop = "1px solid #000000";
  cloneExperienceFormContainer.style.paddingTop = "20px";
  experienceFormsWrapper.appendChild(cloneExperienceFormContainer);
  const btn =
    cloneExperienceFormContainer.getElementsByClassName(
      "addUsedSkillButton"
    )[0];
  btn.onclick = handleSkills;

  const inputs = cloneExperienceFormContainer.querySelectorAll("input");
  for (input of inputs) {
    input.value = "";
  }

  const tags = cloneExperienceFormContainer.querySelectorAll(".tags");
  for (tag of tags) {
    tag.remove();
  }

  const usedSkillsList =
    cloneExperienceFormContainer.querySelectorAll(".usedSkillsList");
  for (usedSkills of usedSkillsList) {
    usedSkills.remove();
  }
};

// Fetch the country data from the API
fetch(
  "https://gist.githubusercontent.com/kcak11/4a2f22fb8422342b3b3daa7a1965f4e4/raw/2cc0fcb49258c667f1bc387cfebdfd3a00c4a3d5/countries.json"
)
  .then((response) => response.json())
  .then((data) => {
    // Create an option element for each country code
    const countryCodeSelect = document.getElementById("countryCode");
    data.forEach((country) => {
      const callingCode = country.dialCode.replace("+", "");
      const option = document.createElement("option");
      option.value = `+${callingCode}`;
      option.text = `${country.isoCode} (+${callingCode})`;
      option.style.textAlign = "left";
      countryCodeSelect.appendChild(option);
    });
  })
  .catch((error) => {
    console.error("Error fetching country data:", error);
  });

// Function to display the popup
function displayPopup() {
  console.log("Displaying popup");
  document.getElementById("cv-form-popup").style.display = "block";
}

// Close the popup when the close button is clicked
document.getElementById("close-popup").addEventListener("click", function () {
  closePopupAndResetForm();
});

// Function to close the popup and reset the form
function closePopupAndResetForm() {
  console.log("Closing popup");
  document.getElementById("cv-form-popup").style.display = "none";
  resetFormInputs();
}

const fieldSuggestions = document.getElementById("fieldSuggestions");
const fieldOptions = [];

fieldSuggestions.addEventListener("input", () => {
  const searchQuery = fieldSuggestions.value.toLowerCase();
  const filteredOptions = fieldOptions.filter((option) => {
    return option.toLowerCase().includes(searchQuery);
  });

  fieldSuggestions.innerHTML = "";

  filteredOptions.forEach((option) => {
    const optionElem = document.createElement("option");
    optionElem.textContent = option;
    fieldSuggestions.appendChild(optionElem);
  });
});

fetch(
  "https://raw.githubusercontent.com/jneidel/job-titles/master/job-titles.txt"
)
  .then((response) => response.text())
  .then((data) => {
    const fields = data.split("\n").map((field) => {
      // Capitalize the first letter of each word in the field and remove extra spacing
      return field
        .toLowerCase()
        .split(" ")
        .map((word) => {
          return word.charAt(0).toUpperCase() + word.slice(1);
        })
        .join(" ")
        .trim();
    });
    fields.forEach((field) => {
      fieldOptions.push(field);
      const optionElem = document.createElement("option");
      optionElem.textContent = field;
      fieldSuggestions.appendChild(optionElem);
    });
  })
  .catch((error) => console.error(error));

const institutionSuggestions = document.getElementById(
  "institutionSuggestions"
);
const institutionOptions = [];

institutionSuggestions.addEventListener("input", () => {
  const searchQuery = institutionSuggestions.value.toLowerCase();
  const filteredOptions = institutionOptions.filter((option) => {
    return option.toLowerCase().includes(searchQuery);
  });

  institutionSuggestions.innerHTML = "";

  filteredOptions.forEach((option) => {
    const optionElem = document.createElement("option");
    optionElem.textContent = option;
    institutionSuggestions.appendChild(optionElem);
  });
});

fetch(
  "https://raw.githubusercontent.com/Hipo/university-domains-list/master/world_universities_and_domains.json"
)
  .then((response) => response.json())
  .then((data) => {
    const institutions = data.map((uni) => uni.name);
    institutions.sort();
    institutions.forEach((institution) => {
      institutionOptions.push(institution);
      const optionElem = document.createElement("option");
      optionElem.textContent = institution;
      institutionSuggestions.appendChild(optionElem);
    });
  })
  .catch((error) => console.error(error));

function resetFormInputs() {
  const inputs = document.querySelectorAll("input");
  for (input of inputs) {
    input.value = "";
  }

  const textareas = document.querySelectorAll("textarea");
  for (textarea of textareas) {
    textarea.value = "";
  }

  const selects = document.querySelectorAll("select");
  for (select of selects) {
    select.value = "";
  }

  const tags = document.querySelectorAll(".tags");
  for (tag of tags) {
    tag.remove();
  }

  const usedSkillsList = document.querySelectorAll(".usedSkillsList");
  for (usedSkill of usedSkillsList) {
    usedSkill.remove();
  }

  const countryCodeSelect = document.getElementById("countryCode");
  countryCodeSelect.selectedIndex = 0;

  // Reset technicalSkillsList and clear HTML element
  technicalSkillsList.length = 0;
  const technicalSkills = document.getElementById("technicalSkillsList");
  while (technicalSkills.firstChild) {
    technicalSkills.firstChild.remove();
  }

  // Reset softSkillsList and clear HTML element
  softSkillsList.length = 0;
  const softSkills = document.getElementById("softSkillsList");
  while (softSkills.firstChild) {
    softSkills.firstChild.remove();
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

const jobTitleSuggestions = document.getElementById("titleSuggestions");
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
