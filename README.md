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