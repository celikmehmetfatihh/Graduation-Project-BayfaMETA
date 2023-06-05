class Education:
    """
    Education class.
    """
    def __init__(self, institution, degree, field, gpa):
        self.institution = institution
        self.degree = degree
        self.field = field
        self.gpa = gpa
    
    def get_keywords(self):
        """
        Get the education keywords.
        """
        return [self.field.lower()]
    
    def get_gpa(self):
        """
        Get the gpa of the education.
        """
        return self.gpa