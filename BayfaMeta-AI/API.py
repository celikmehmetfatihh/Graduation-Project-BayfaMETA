from flask import Flask, request
from flask_cors import CORS
import requests
from Personality_Trait_Extraction.pte_testing import get_predictions
from Keyword_Matching_Scoring.applicant import Applicant
from emailing import send_email, generate_password


"""
The API class.
"""
name = 'BayfaMeta'
app = Flask(name)
cors = CORS(app, origins='*')
app.config['CORS_HEADERS'] = 'Content-Type'


@app.route('/RecruitmentProcess', methods=['POST'])
def recruitment_process():
    """
    Receive the recruitment process data from the front-end and send it to the backend.
    """
    data = request.json

    jobPosition = data.get('jobPosition', '')
    requiredSkills = data.get('requiredSkills', [])
    minExperience = data.get('minExperience', 0)
    eduMultiplier = data.get('eduMultiplier', 0.2)
    expMultiplier = data.get('expMultiplier', 0.2)
    techSkillsMultiplier = data.get('techSkillsMultiplier', 0.2)
    softSkillsMultiplier = data.get('softSkillsMultiplier', 0.2)
    keywordMultiplier = data.get('keywordMultiplier', 0.2)
    openness = data.get('openness', 0.2)
    conscientiousness = data.get('conscientiousness', 0.2)
    extraversion = data.get('extraversion', 0.2)
    agreeableness = data.get('agreeableness', 0.2)
    neuroticism = data.get('neuroticism', 0.2)

    recruitment_process = {
        'jobPosition': jobPosition,
        'requiredSkills': requiredSkills,
        'minExperience': minExperience,
        'eduMultiplier': eduMultiplier,
        'expMultiplier': expMultiplier,
        'techSkillsMultiplier': techSkillsMultiplier,
        'softSkillsMultiplier': softSkillsMultiplier,
        'keywordMultiplier': keywordMultiplier,
        'opennessMultiplier': openness,
        'conscientiousnessMultiplier': conscientiousness,
        'extraversionMultiplier': extraversion,
        'agreeablenessMultiplier': agreeableness,
        'neuroticismMultiplier': neuroticism
    }

    url = 'https://localhost:7284/api/RecruitmentProcess'
    response = requests.post(url, json=recruitment_process)

    if response.status_code == 200:
        print("Success:", response.json())
    else:
        print("Error:", response.status_code, response.text)


@app.route('/InterviewScores', methods=['GET'])
def send_interview_scores():
    """
    Get the user_id, position id, video recording path and trait multipliers from database, predict the personality traits and send them to the backend by calculating the scores.
    """

    # Get the user id, position id and video recording path.
    user_id = request.args.get('userId')
    position_id = request.args.get('positionId')
    video_path = request.args.get('path')

    url = f"https://localhost:7284/api/Position/GetVideoInterviewConfiguration/{position_id}"
    response = requests.get(url, verify=False)

    if response.status_code == 200:
        # Get the trait multipliers.
        request_object = response.json()['data']
        openness = request_object['opennessMultiplier'] / 100
        conscientiousness = request_object['conscientiousnessMultiplier'] / 100
        extraversion = request_object['extraversionMultiplier'] / 100
        agreeableness = request_object['agreeablenessMultiplier'] / 100
        neuroticism = request_object['neuroticismMultiplier'] / 100

        # Predict the personality traits.
        predictions = get_predictions(video_path)

        # Normalize the predictions to be between 0 and 1.
        max_prediction = max(predictions.values())
        if max_prediction > 1:
            for trait in predictions:
                predictions[trait] /= max_prediction

        # Calculate the scores for each trait by multiplying the trait multiplier with the normalized trait prediction.
        openness_score = predictions['openness'] * openness
        conscientiousness_score = predictions['conscientiousness'] * conscientiousness
        extraversion_score = predictions['extraversion'] * extraversion
        agreeableness_score = predictions['agreeableness'] * agreeableness
        neuroticism_score = predictions['neuroticism'] * neuroticism

        # Calculate the interview score by summing up the scores for each trait.
        interview_score = openness_score + conscientiousness_score + extraversion_score + agreeableness_score + neuroticism_score

        # Multiply the scores by 100 to get a percentage.
        openness_score *= 100
        conscientiousness_score *= 100
        extraversion_score *= 100
        agreeableness_score *= 100
        neuroticism_score *= 100
        interview_score *= 100

        interview_scores = {'UserId': user_id,
                            'PositionId': position_id,
                            'Openness': openness_score,
                            'Conscientiousness': conscientiousness_score,
                            'Extraversion': extraversion_score,
                            'Agreeableness': agreeableness_score,
                            'Neuroticism': neuroticism_score,
                            'InterviewScore': interview_score}
        
        return interview_scores
    return [response.status_code, response.text]


@app.route('/CvForm', methods=['POST'])
def send_cvForm_scores():
    """
    Get the resume information from the front-end and send it to the backend.
    """

    # Get the resume information.
    resume_info = request.json

    # Get the job keywords from the database using the position id.
    position_id = resume_info['PositionId']

    url = 'https://localhost:7284/api/Position/' + position_id
    response = requests.get(url, verify=False)

    if response.status_code == 200:
        jobTitle = response.json()['data']['jobTitle']
        jobDescription = response.json()['data']['jobDescription']

    url = f"https://localhost:7284/api/Position/GetResumeConfiguration/{position_id}"
    response = requests.get(url, verify=False)

    if response.status_code == 200:
        # Get the job keywords and the multipliers.
        request_object = response.json()['data']
        job_info = {
            'jobTitle': jobTitle,
            'jobDescription': jobDescription,
            'jobPositions': request_object['jobPositions'].split(';'),
            'requiredSkills': request_object['requiredSkills'].split(';'),
            'minExperience': request_object['minExperience'],
            'eduMultiplier': request_object['eduMultiplier'],
            'expMultiplier': request_object['expMultiplier'],
            'techSkillsMultiplier': request_object['techSkillsMultiplier'],
            'softSkillsMultiplier': request_object['softSkillsMultiplier'],
            'keywordMultiplier': request_object['keywordMultiplier']
        }

        # Get the matching score and the total score.
        app_obj = Applicant(resume_info, job_info)
        matching_score = app_obj.get_matching_score()[0]
        total_score = app_obj.get_total_score(job_info['eduMultiplier']/100, job_info['expMultiplier']/100, job_info['techSkillsMultiplier']/100, job_info['softSkillsMultiplier']/100, job_info['keywordMultiplier']/100)

        # Send the resume information to the backend.
        url = 'https://localhost:7284/api/Resume'
        cvForm = {'UserId' : app_obj.user_id,
                  'PositionId': app_obj.position_id,
                  'Name' : app_obj.name,
                  'Email' : app_obj.email,
                  'PhoneNumber' : app_obj.phone,
                  'Education' : app_obj.str_education(),
                  'TechnicalSkills' : ';'.join(app_obj.technical_skills),
                  'SoftSkills' : ';'.join(app_obj.soft_skills),
                  'Experience' : app_obj.str_experience(),
                  'MatchingScore' : matching_score,
                  'TotalScore' : total_score}

        req = requests.post(url, json = cvForm, verify=False)
        if req.status_code == 200:
            print("Resume information successfully saved.")
        else:
            print("Error occurred while saving resume information.")
    else:
        print("Error occurred while fetching job keywords.")
    
    return [response.status_code, response.text]


@app.route('/PassedEmails', methods=['POST'])
def send_passed_emails():
    """
    Get the users' emails and send them an email informing them that they passed the interview.
    """

    # Get the users' emails.
    emails = request.json
    
    status_dict = {}
    all_ok = True

    for email in emails:
        result = send_email(email, status='passed')
        status_dict[email] = result
        if result != "Email sent successfully.":
            all_ok = False

    if all_ok:
        return "Emails sent successfully."
    else:
        return "Error occurred while sending emails."


@app.route('/FailedEmails', methods=['POST'])
def send_failed_emails():
    """
    Get the users' emails and send them an email informing them that they failed the interview.
    """

    # Get the users' emails.
    emails = request.json
    
    status_dict = {}
    all_ok = True

    for email in emails:
        result = send_email(email, status='failed')
        status_dict[email] = result
        if result != "Email sent successfully.":
            all_ok = False

    if all_ok:
        return "Emails sent successfully."
    else:
        return "Error occurred while sending emails."


@app.route('/ResetPassword', methods=['POST'])
def reset_password():
    """
    Get the user's email, send a randomly generated password to the user and update the user's password in the database.
    """

    # Get the user's email.
    email = request.json['Email']
    password = generate_password()

    update_obj = {'Email': email,
                  'Password': password}

    # Update the user's password in the database.
    url = f"https://localhost:7284/api/User/UpdatePassword"
    response = requests.put(url, json=update_obj, verify=False)

    if response.status_code == 200:
        # Send the new password to the user.
        result = send_email(email, password=password)
        if result == "Email sent successfully.":
            print("Password reset successfully.")
        else:
            print("Error occurred while sending email.")

    else:
        print("Error occurred while updating password.")
    
    return [response.status_code, response.text]

if __name__ == '__main__':
    app.run()