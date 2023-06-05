import json
from Keyword_Matching_Scoring.applicant import Applicant


def read_json(file_name):
    """
    Read the json file.
    :param file_name: The json file name.
    :return: The json file dictionary.
    """
    with open(file_name) as f:
        json_dict = json.load(f)
    return json_dict

if __name__ == '__main__':
    resume_info = read_json('Keyword_Matching_Scoring\\applicant.json')
    job_info = {
        'jobTitle' : "Software Engineer",
        "jobPositions": "Software Developer;Backend Developer".split(';'),
        "jobDescription" : "We are looking for a Software Engineer to join our growing Engineering team and build out the next generation of our platform. The ideal candidate is a hands-on platform builder with significant experience in developing scalable data platforms, with experience in business intelligence, analytics, data science and data products. They must have strong, firsthand technical expertise in a variety of configuration management and big data technologies and the proven ability to fashion robust scalable solutions that can manage large data sets. They must be at ease working in an agile environment with little supervision. This person should embody a passion for continuous improvement and innovation.",
        "requiredSkills": "Python;Java;C++;Time management;HTML".split(';'),
        "minExperience": 1
    }
    app_obj = Applicant(resume_info, job_info)
    print(app_obj.get_matching_score()[0])
    print(app_obj.get_total_score())
