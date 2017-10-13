AWS SES Sample
- Send email through AWS SES
- Get status through SES -> SNS -> SQS

Note:
AWS services have been cancelled. So, the info in app.config is useless. Need to be updated before running.

You have to setup SES in AWS.
This program send email through SES.

You also need SNS and SQS. 
SNS to receive notification from SES.
Then queue the message in SQS.
This program is querying the message from SQS.

Example settings in app.config
    <add key="SMTPUser" value="AKIAJEK6VIXNOOMEXSXA"/>
    <add key="SMTPPW" value="AuPqDWu1Fg50Il2XOBUv3nY/S32/nQlTeuNw+siyvRyP"/>
    <add key="SMTPHost" value="email-smtp.us-east-1.amazonaws.com"/>
    <add key="SMTPPort" value="25"/>
    <add key="AWSKeyID" value="AKIAJYT2T22MPEXZG7HA"/>
    <add key="AWSAccessKey" value="GK+srg5LUyPvSIgsFQIlogQtwGMMBLqHUgE9awpY"/>