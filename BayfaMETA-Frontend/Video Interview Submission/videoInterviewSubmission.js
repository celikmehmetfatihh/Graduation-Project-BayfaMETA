// Get the user ID and position ID from the URL
const userId = localStorage.getItem("userId");
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const positionId = urlParams.get("positionId");

/** @type{HTMLButtonElement} **/
const recordBtn = document.getElementById("record");

/** @type{HTMLVideoElement} **/
const video = document.getElementById("video");
let recorder;
let recording = false;

let recordingDuration;

function formatTime(minutes) {
  const minutesPart = Math.floor(minutes / 60)
    .toString()
    .padStart(2, "0");
  const secondsPart = (minutes % 60).toString().padStart(2, "0");
  return `${minutesPart}:${secondsPart}`;
}

async function initVideoStream() {
  await getInterviewTime();
  const stream = await navigator.mediaDevices.getUserMedia({
    audio: true,
    video: true,
  });
  video.srcObject = stream;
  video.play();

  recorder = new MediaRecorder(stream, { type: "video/webm" });

  recorder.ondataavailable = async (blobEvent) => {
    const file = new File([blobEvent.data], `video.webm`, {
      type: "video/webm",
    });
    const formData = new FormData();
    formData.append("video", file);
    console.log([...formData.entries()]);
  };

  // Add variables for timer and recording duration
  const timerElement = document.getElementById("timer");
  let timerInterval;

  // Add a function to handle sending the recording to the server
  async function sendRecording(blob) {
    const file = new File([blob], `video.webm`, {
      type: "video/webm",
    });
    const formData = new FormData();
    formData.append("video", file);
    console.log([...formData.entries()]);

    try {
      const response = await fetch(
        "https://localhost:7284/api/VideoInterviews/upload/" +
          userId +
          "/" +
          positionId,
        {
          method: "POST",
          body: formData,
        }
      );

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      console.log("Upload successful:", await response.text());

      // After successfully sending the video, redirect to home page
      window.location.href = "../Home Page/applicantHome.html";
    } catch (error) {
      console.error("Error uploading the video:", error);
    }
  }

  recorder.ondataavailable = async (blobEvent) => {
    const blob = blobEvent.data;
    const shouldUpload = confirm("Do you want to send this recording?");

    if (shouldUpload) {
      await sendRecording(blob);
    }
  };

  // Modify the recordBtn.onclick function
  recordBtn.onclick = async () => {
    if (!recording) {
      recorder.start();
      recordBtn.innerHTML = "Stop Recording";
      timerElement.style.display = "inline-block";
      recording = true;

      // Start the timer and update the timer element every second
      let remainingTime = recordingDuration * 60; // Convert minutes to seconds
      timerElement.textContent = formatTime(remainingTime);
      timerInterval = setInterval(() => {
        remainingTime--;
        timerElement.textContent = formatTime(remainingTime);

        if (remainingTime <= 0) {
          // Stop the recording when the timer reaches 0
          clearInterval(timerInterval);
          recorder.stop();
          recordBtn.innerHTML = "Start Recording";
          timerElement.textContent = "00:00"; // Set to 00:00 after recording
          recording = false;
        }
      }, 1000);
    } else {
      clearInterval(timerInterval);
      recorder.stop();
      recordBtn.innerHTML = "Start Recording";
      timerElement.textContent = "00:00"; // Set to 00:00 after recording
      recording = false;
    }
  };
}

const uploadedFile = document.getElementById("uploadedFile");

uploadedFile.addEventListener("change", async (event) => {
  const file = event.target.files[0];
  if (file) {
    const isValidDuration = await checkFileDuration(file, recordingDuration);
    if (!isValidDuration) {
      alert(
        `The uploaded video should not exceed ${recordingDuration} seconds.`
      );
      // Reset the file input
      event.target.value = "";
    }
  }
});

async function checkFileDuration(file, maxDuration) {
  return new Promise((resolve) => {
    const temporaryVideo = document.createElement("video");

    // Create an object URL for the uploaded file
    const fileURL = URL.createObjectURL(file);
    temporaryVideo.src = fileURL;

    temporaryVideo.addEventListener("loadedmetadata", () => {
      // Compare the video duration to the maxDuration in minutes
      const isValidDuration = temporaryVideo.duration / 60 <= maxDuration;
      URL.revokeObjectURL(fileURL); // Release the object URL
      resolve(isValidDuration);
    });
  });
}

const privacyPolicyCheckbox = document.getElementById(
  "privacy-policy-checkbox"
);
const showVideoSectionBtn = document.getElementById("show-video-section");
const showFileSectionBtn = document.getElementById("show-file-section");

privacyPolicyCheckbox.addEventListener("change", () => {
  if (privacyPolicyCheckbox.checked) {
    showVideoSectionBtn.style.display = "inline-block";
    showFileSectionBtn.style.display = "inline-block";
    privacyPolicyCheckbox.disabled = true; // Lock the checkbox's status
  } else {
    showVideoSectionBtn.style.display = "none";
    showFileSectionBtn.style.display = "none";
  }
});

showVideoSectionBtn.addEventListener("click", async () => {
  document.getElementById("video-section").style.display = "block";
  document.getElementById("file-section").style.display = "none";
  await initVideoStream();

  showVideoSectionBtn.classList.add("active-button");
  showFileSectionBtn.classList.remove("active-button");
  showVideoSectionBtn.disabled = true;
  showFileSectionBtn.disabled = false;
});

showFileSectionBtn.addEventListener("click", () => {
  document.getElementById("file-section").style.display = "block";
  document.getElementById("video-section").style.display = "none";
  stopCamera();

  showFileSectionBtn.classList.add("active-button");
  showVideoSectionBtn.classList.remove("active-button");
  showFileSectionBtn.disabled = true;
  showVideoSectionBtn.disabled = false;
});

function stopCamera() {
  if (video.srcObject) {
    video.srcObject.getTracks().forEach((track) => {
      track.stop();
    });
    video.srcObject = null;
  }
}

async function getRandomInterviewTopic() {
  try {
    const response = await fetch(
      "https://localhost:7284/api/Position/GetVideoInterviewConfiguration/" +
        positionId
    );

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const tempData = await response.json();
    const responseData = tempData["data"];
    const topicsString = responseData.interviewTopics;
    const topicsList = topicsString.split(";");
    const randomIndex = Math.floor(Math.random() * topicsList.length);
    const randomTopic = topicsList[randomIndex];

    // Update the text content of the interview-topic element
    const interviewTopicElement = document.getElementById("interview-topic");

    // Capitalize the first letter of each word
    const topicWords = randomTopic.split(" ").map((word) => {
      return word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
    });
    const capitalizedTopic = topicWords.join(" ");

    interviewTopicElement.textContent = `Interview Topic: ${capitalizedTopic}`;

    return capitalizedTopic;
  } catch (error) {
    console.error("Error fetching interview topics:", error);
  }
}

getRandomInterviewTopic();

async function getInterviewTime() {
  try {
    const response = await fetch(
      "https://localhost:7284/api/Position/GetVideoInterviewConfiguration/" +
        positionId
    );

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const tempData = await response.json();
    const responseData = tempData["data"];
    recordingDuration = parseInt(responseData.allocatedTime);

    // Initialize the timer element to 00:00
    const timerElement = document.getElementById("timer");
    timerElement.textContent = "00:00";
  } catch (error) {
    console.error("Error fetching interview time:", error);
  }
}

// Call the getInterviewTime function to initialize the recording duration
getInterviewTime();
