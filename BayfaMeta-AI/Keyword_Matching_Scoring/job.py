import re

class Job:
    def __init__(self, job_info):
        self.job_title = job_info["jobTitle"]
        self.job_description = job_info["jobDescription"]
        self.required_skills = job_info["requiredSkills"]
        self.job_positions = job_info["jobPositions"]
        self.min_experience = job_info["minExperience"]
        self.job_keywords = self.get_job_title_keywords() + self.get_job_description_keywords() + self.get_required_skills() + self.get_job_positions()


    def get_job_title_keywords(self):
        """
        Get the job title keywords.
        """
        keywords = []
        for keyword in self.job_title.split():
            keywords.append(keyword.lower().strip())
        return keywords


    def get_job_description_keywords(self):
        """
        Get the job description keywords.
        """
        keywords = []
        for keyword in self.job_description.split():
            keywords.append(keyword.lower().strip())
        return keywords    
    

    def get_required_skills(self):
        """
        Get the required skills.
        """
        keywords = []
        for keyword in self.required_skills:
            keywords.append(keyword.lower().strip())
        return keywords


    def get_job_positions(self):
        """
        Get the job positions.
        """
        keywords = []
        for keyword in self.job_positions:
            keywords.append(keyword.lower().strip())
        return keywords


    def get_min_experience(self):
        """
        Get the minimum experience.
        """
        return self.min_experience


    def get_job_keywords(self):
        """
        Get the job keywords.
        """
        return self.job_keywords
    

    def count_matchedPattern_description(self, applicant_keywords):
        """
        Count the number of matched keywords in the job description.
        :param applicant_keywords: The applicant keywords.
        :return: The number of matched keywords in the job description.
        """
        count = 0
        for keyword in applicant_keywords:
            if re.search(keyword, self.job_description, re.IGNORECASE):
                count += 1
        return count