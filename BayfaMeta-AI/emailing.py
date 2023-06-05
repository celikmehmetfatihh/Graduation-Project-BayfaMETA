import smtplib
from email.mime.text import MIMEText
import ssl
import random
import string

def generate_password():
    """
    Generate a random password.
    """
    return ''.join(random.choice(string.ascii_letters + string.digits) for _ in range(8))

def send_email(receiver_email, sender_email='bayfameta@outlook.com', status='passed', reason='staging'):
    """
    Send an email to the user to inform them about the status of their application.
    """
    # Create the message.

    try:
        if reason == 'staging':
            if status == 'passed':
                message = MIMEText(f"""<html>
            <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.5;
                }}

                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                }}

                .header {{
                    background-color: #f2f2f2;
                    padding: 20px;
                    text-align: center;
                }}

                .content {{
                    padding: 30px;
                    background-color: #ffffff;
                }}

                .footer {{
                    background-color: #f2f2f2;
                    padding: 20px;
                    text-align: center;
                }}
            </style>
            </head>
            <body>
                <div class="container">
                    <div class="header">
                        <h2>Congratulations!</h2>
                    </div>
                    <div class="content">
                        <p>Dear Applicant,</p>
                        <p>We are pleased to inform you that you have <strong>{status}</strong> the <strong>first</strong> stage. Your qualifications and experience have impressed our team. You can now proceed to the next stage of the selection process.</p>
                        <p>Please log in to our website for more detailed instructions and to access the next steps.</p>
                        <p>We appreciate your interest in BayfaMeta and look forward to further reviewing your application.</p>
                        <p>Best regards,<br>BayfaMeta</p>
                    </div>
                    <div class="footer">
                        <p>If you have any questions, please contact us at support@bayfameta.com</p>
                    </div>
                </div>
            </body>
            </html>
            """, 'html')
                    
            else:
                message = MIMEText(f"""<html>
            <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.5;
                }}

                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                }}

                .header {{
                    background-color: #f2f2f2;
                    padding: 20px;
                    text-align: center;
                }}

                .content {{
                    padding: 30px;
                    background-color: #ffffff;
                }}

                .footer {{
                    background-color: #f2f2f2;
                    padding: 20px;
                    text-align: center;
                }}
            </style>
            </head>
            <body>
                <div class="container">
                    <div class="header">
                        <h2>Unfortunately...</h2>
                    </div>
                    <div class="content">
                        <p>Dear Applicant,</p>
                        <p>We regret to inform you that your application has <strong>{status}</strong> the <strong>first</strong> stage. Although your qualifications were carefully considered, we have decided to proceed with other candidates who better align with our requirements.</p>
                        <p>We sincerely appreciate your interest in BayfaMeta and the time you invested in the application process. Your skills and experiences may be a good fit for future opportunities, so we encourage you to keep an eye on our website for updates.</p>
                        <p>Thank you again for considering us as your potential employer. We wish you the best in your job search and career endeavors.</p>
                        <p>Kind regards,<br>BayfaMeta</p>
                    </div>
                    <div class="footer">
                        <p>If you have any questions, please contact us at support@bayfameta.com</p>
                    </div>
                </div>
            </body>
            </html>
            """, 'html')

            message['Subject'] = f'Job Application Status'

        else:
            new_password = generate_password()

            message = MIMEText(f'''\
                <html>
                <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        line-height: 1.5;
                    }}

                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                    }}

                    .header {{
                        background-color: #f2f2f2;
                        padding: 20px;
                        text-align: center;
                    }}

                    .content {{
                        padding: 30px;
                        background-color: #ffffff;
                    }}

                    .footer {{
                        background-color: #f2f2f2;
                        padding: 20px;
                        text-align: center;
                    }}
                </style>
                </head>
                <body>
                    <div class="container">
                        <div class="header">
                            <h2>Password Reset</h2>
                        </div>
                        <div class="content">
                            <p>Dear User,</p>
                            <p>We have received a request to reset your password. Your new password is:</p>
                            <p><strong>{new_password}</strong></p>
                            <p>Please use this new password to log in to your account. After logging in, we recommend changing your password to a secure and personalized one.</p>
                            <p>If you did not request a password reset, please ignore this email or contact our support team immediately.</p>
                            <p>Best regards,<br>BayfaMeta</p>
                        </div>
                        <div class="footer">
                            <p>If you have any questions, please contact us at support@bayfameta.com</p>
                        </div>
                    </div>
                </body>
                </html>
            ''', 'html')

            message['Subject'] = 'Password Reset'


        message['From'] = sender_email
        message['To'] = receiver_email

        # Send email
        with smtplib.SMTP('smtp-mail.outlook.com', 587) as server:
            server.starttls()
            server.login(sender_email, 'atsgrad123')
            server.sendmail(sender_email, receiver_email, message.as_string())

        if new_password is not None:
            return new_password
        else:
            return 'Email sent successfully.'

    except Exception as e:
        return 'Failed to send email: ', str(e)

if __name__ == '__main__':
    sender_email = 'bayfameta@outlook.com'
    receiver_email = 'bayfameta@outlook.com'
    send_email(sender_email, receiver_email, reason='staging', status='failed')
