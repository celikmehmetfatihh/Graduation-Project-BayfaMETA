function Soru(soruNumarasi, soruMetni, cevapSecenekleri, dogruCevap) {
  this.soruNumarasi = soruNumarasi;
  this.soruMetni = soruMetni;
  this.cevapSecenekleri = cevapSecenekleri;
  this.dogruCevap = dogruCevap;
}

Soru.prototype.cevabiKontrolEt = function (cevap) {
  return cevap === this.dogruCevap;
};

let sorular = [];
getQuestions();

let questionTime;

async function getQuestions() {
  var queryString1 = window.location.search;
  var urlParams1 = new URLSearchParams(queryString1);
  var positionId1 = urlParams1.get("positionId");
  try {
    const response = await fetch(
      `https://localhost:7284/api/Position/GetTechnicalQuestionsOfPosition/${positionId1}`
    );

    const dataJSON = await response.json();
    if (!response.ok) {
      console.log("error");
      return;
    }
    info = dataJSON.data.interviewQuestions;
    questionTime = dataJSON.data.questionTime;

    createTest(info);
  } catch (err) {
    console.log(err);
  }
}

function createTest(info) {
  for (i = 0; i < info.length; i++) {
    var question = new Soru(
      ((info[i].id - 1) % 10) + 1,
      info[i].question,
      {
        a: info[i].optionA,
        b: info[i].optionB,
        c: info[i].optionC,
        d: info[i].optionD,
        e: info[i].optionE,
      },
      info[i].answer
    );
    sorular.push(question);
  }
}
