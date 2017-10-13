using Amazon;
using Amazon.SQS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AWSSESSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox2.Text = "test1";
            textBox3.Text = "test body 111";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var appSettings = ConfigurationManager.AppSettings;
            SmtpClient client = new SmtpClient(appSettings["SMTPHost"], Convert.ToInt32(appSettings["SMTPPort"]));
            client.Credentials = new NetworkCredential(appSettings["SMTPUser"], appSettings["SMTPPW"]);
            client.EnableSsl = true;

            MailMessage message = new MailMessage("saisuu@hotmail.com", comboBox1.Text);
            message.Subject = textBox2.Text;
            message.Body = textBox3.Text;

            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            client.SendAsync(message, "test-suh");
            //message.Dispose();
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                MessageBox.Show(string.Format("[{0}] Send canceled.", token));
            }
            if (e.Error != null)
            {
                MessageBox.Show(string.Format("[{0}] {1}", token, e.Error.ToString()));
            }
            else
            {
                MessageBox.Show("Message sent.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var appSettings = ConfigurationManager.AppSettings;
            AmazonSQSClient sqsClient = new AmazonSQSClient(appSettings["AWSKeyID"], appSettings["AWSAccessKey"], RegionEndpoint.USEast1);
            var queueUrl = sqsClient.ListQueues("Queue-For-SES-Notifications").QueueUrls[0];

            while(true)
            {
                var response = sqsClient.ReceiveMessage(queueUrl);
                // Check our response
                if (response.Messages.Count > 0)
                {
                    foreach (var message in response.Messages)
                    {
                        // Send an email
                        var body = (JObject)JsonConvert.DeserializeObject(message.Body, typeof(JObject));
                        MessageBox.Show(body["Message"].ToString());

                        // Delete our message so that it doesn't get handled again
                        sqsClient.DeleteMessage(queueUrl, message.ReceiptHandle);
                    }
                }
                else
                {
                    MessageBox.Show("No more SQS message in queue");
                    break;
                }
            }
        }
    }
}
