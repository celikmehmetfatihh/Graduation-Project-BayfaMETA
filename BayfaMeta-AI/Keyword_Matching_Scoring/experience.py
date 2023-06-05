class Experience:
    """
    Experience class.
    """
    def __init__(self, title, company, duration, skills_used):
        super().__init__()
        self.title = title
        self.company = company
        self.duration = duration
        self.skills_used = skills_used
    
    def get_title_keywords(self):
        """
        Get the title keywords.
        """
        keywords = []
        for keyword in self.title.split():
            keywords.append(keyword.lower().strip())
        return keywords
    
    def get_company(self):
        """
        Get the company.
        """
        return self.company
    

    def get_duration(self):
        """
        Get the duration of the experience.
        """
        return self.duration


    def get_skills_used(self):
        """
        Get the skills used in the experience.
        """
        keywords = []
        for keyword in self.skills_used:
            keywords.append(keyword.lower().strip())
        return keywords