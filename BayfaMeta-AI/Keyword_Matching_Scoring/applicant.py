from Keyword_Matching_Scoring.education import Education
from Keyword_Matching_Scoring.experience import Experience
from Keyword_Matching_Scoring.job import Job


class Applicant:
    """
    Applicant class.
    """
    def __init__(self, resume_info, job_info):
        self.user_id = resume_info["UserId"]
        self.position_id = resume_info["PositionId"]   
        self.name = resume_info["Name"]
        self.email = resume_info["Email"]
        self.phone = resume_info["PhoneNumber"]

        self.education = []
        for edu in resume_info["Education"]:
            education = Education(edu["Institution"], edu["Degree"], edu["Field"], edu["GPA"])
            self.education.append(education)

        self.technical_skills = resume_info["TechnicalSkills"]
        self.soft_skills = resume_info["SoftSkills"]

        self.experience = []
        for exp in resume_info["Experience"]:
            experience = Experience(exp["Title"], exp["Company"], exp['Duration'], exp["SkillsUsed"])
            self.experience.append(experience)

        self.job = Job(job_info);
    

    def applicant_keywords(self):
        """
        Get the applicant keywords.
        :return: The applicant keywords.
        """
        keywords = self.get_technical_skills() + self.get_soft_skills()
        for edu in self.education:
            for edu_kw in edu.get_keywords():
                keywords.append(edu_kw.lower().strip())
        for exp in self.experience:
            for exp_title in exp.get_title_keywords():
                keywords.append(exp_title.lower().strip())
            for exp_kw in exp.get_skills_used():
                keywords.append(exp_kw.lower().strip())
        return keywords
    

    def get_matching_score(self):
        """
        Get the matching percentage score.
        :return: The matching score.
        """

        # Max keyword score
        max_keyword_score = len(self.job.get_job_title_keywords() + self.job.get_required_skills() + self.job.get_job_positions()) * 2 # Assuming max keywords = all keywords except job description * 2

        # Calculate keyword matching score
        keyword_score = 0
        for keyword in self.job.get_job_keywords():
            if keyword.lower() in self.applicant_keywords():
                keyword_score += 2
        
        # Give additional points for matching job description
        keyword_score += self.job.count_matchedPattern_description(self.applicant_keywords())

        if keyword_score > max_keyword_score:
            keyword_score = max_keyword_score
        mathcing_score = keyword_score / max_keyword_score * 100

        return round(mathcing_score, 2), keyword_score


    def str_education(self):
        """
        Get the education object semi-colon separated string.
        :return: The education object semi-colon separated string.
        """
        edu_str = ""
        for edu in self.education:
            for edu_kw in edu.get_keywords():
                edu_str += edu_kw + ";"

            # Add an extra semi-colon at the end of each education object.
            edu_str += ";"
        
        # Remove the last two semi-colons.
        return edu_str[:-2]


    def str_experience(self):
        """
        Get the experience object semi-colon separated string.
        :return: The experience object semi-colon separated string.
        """
        exp_str = ""
        for exp in self.experience:
            for exp_kw in exp.get_skills_used():
                exp_str += exp_kw + ";"

            # Add an extra semi-colon at the end of each experience object.
            exp_str += ";"
        
        # Remove the last two semi-colons.
        return exp_str[:-2]


    def get_technical_skills(self):
        """
        Get the technical skills.
        :return: The technical skills.
        """
        skills = []
        for ts in self.technical_skills:
            skills.append(ts.lower().strip())
        return skills
    

    def get_soft_skills(self):
        """
        Get the soft skills.
        :return: The soft skills.
        """
        skills = []
        for ss in self.soft_skills:
            skills.append(ss.lower().strip())
        return skills


    def get_total_score(self, edu_multiplier=0.20, tech_skills_multiplier=0.20, soft_skills_multiplier=0.20, exp_multiplier=0.20, keyword_multiplier=0.20):
        """
        Calculate the total score.
        :param edu_multiplier: The education multiplier.
        :param tech_skills_multiplier: The technical skills multiplier.
        :param soft_skills_multiplier: The soft skills multiplier.
        :param exp_multiplier: The experience multiplier.
        :param keyword_multiplier: The keyword matching multiplier.
        :return: The total score.
        """

        # Max scores for each category
        max_edu_score = 10  # Assuming max GPA = 4 * 2.5
        max_tech_skills_score = len(self.job.get_required_skills()) * 10  # Assuming max technical skills = required skills * 10
        max_soft_skills_score = len(self.job.get_required_skills()) * 5  # Assuming max soft skills = required skills * 5
        max_exp_score = self.job.get_min_experience() * 5  # Assuming max experience = min experience * 5
        max_keyword_score = len(self.job.get_job_title_keywords() + self.job.get_required_skills() + self.job.get_job_positions()) * 2  # Assuming max keywords = all keywords except job description * 2

        # Calculate education score based on GPA
        edu_score = 0
        for edu in self.education:
            gpa = edu.get_gpa()
            edu_score += gpa * 2.5  # score is the GPA times 2.5

        # Normalize education score
        normalized_edu_score = edu_score / max_edu_score

        # Calculate technical skills score
        tech_skills_score = len(set(self.get_technical_skills()) & set(self.job.get_required_skills())) * 10

        # Normalize technical skills score
        normalized_tech_skills_score = tech_skills_score / max_tech_skills_score

        # Calculate soft skills score
        soft_skills_score = len(set(self.get_soft_skills()) & set(self.job.get_required_skills())) * 5

        # Normalize soft skills score
        normalized_soft_skills_score = soft_skills_score / max_soft_skills_score

        # Calculate experience score
        exp_score = 0
        for exp in self.experience:
            if set(exp.get_skills_used()) & set(self.job.get_required_skills()):
                if exp.get_duration() >= self.job.get_min_experience():
                    exp_score += (exp.get_duration() - self.job.get_min_experience()) * 5
                    exp_score += self.job.get_min_experience() * 5

        # Normalize experience score
        normalized_exp_score = exp_score / max_exp_score

        # Calculate keyword matching score
        keyword_score = self.get_matching_score()[1]
        if keyword_score > max_keyword_score:
            keyword_score = max_keyword_score
        normalized_keyword_score = keyword_score / max_keyword_score

        # Calculate overall score without exceeding 100
        total_score = (normalized_edu_score * edu_multiplier + normalized_tech_skills_score * tech_skills_multiplier +
                    normalized_soft_skills_score * soft_skills_multiplier + normalized_exp_score * exp_multiplier +
                    normalized_keyword_score * keyword_multiplier) * 100

        # Adjust total score if it exceeds 100
        if total_score > 100:
            total_score = 100

        # Return the score
        return round(total_score, 2)