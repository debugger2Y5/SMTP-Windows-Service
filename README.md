# SMTP-Windows-Service

Course Assignment to build a centralized application which can send emails using SMTP. Created a windows service which reads a JSON file (1 json per file) after every 15 minutes from a fixed location (provided in the app.config) and sends email to the receipts. Following is the JSON input sample: { "To": “”, "Subject": “”, "MessageBody": “” }
