using SendGrid;
using System.Threading.Tasks;
using System.Collections.Generic;
using SendGrid.Helpers.Mail;
using YOY.DTO.Services.Email.SendGrid;
using System;
using YOY.DTO.Entities.Misc.Structure.POCO;

namespace YOY.ThirdpartyServices.Services.Email.Sendgrid
{
    public class SendgridService
    {
        public static async Task<bool> SendAsync(OutboundEmail email)
        {
            return await SendEmailAsync(email);
        }

        private static string Substitute(string text, Dictionary<string, string> substitutions)
        {
            if (!string.IsNullOrEmpty(text) && substitutions?.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in substitutions)
                {
                    text = text.Replace(item.Key, item.Value);
                }
            }

            return text;
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private static async Task<bool> SendEmailAsync(OutboundEmail email)
        {
            bool success = false;

            try
            {
                // Retrieve the API key from the settings variables.
                string apiKey = Settings.Default.sendgrid_apikey;

                SendGridClient client = new SendGridClient(apiKey);

                var myMessage = new SendGridMessage();
                List<EmailAddress> recipients = new List<EmailAddress>();
                EmailAddress recipient;

                if (email.Tos?.Count > 0)
                {
                    //Build the email data
                    var from = new EmailAddress(email.From.Key, email.From.Value);//("info@clubyoy.com", "Club YOY");
                    var subject = email.Subject;// "Hello World from the SendGrid CSharp Library Helper!";
                    var plainTextContent = Substitute(email.PlainText, email.Substitutions);//"Hello, Email from the helper [SendSingleEmailAsync]!";
                    var htmlContent = Substitute(email.HtmlContent, email.Substitutions);// "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";

                    foreach (Pair<string, string> item in email.Tos)
                    {
                        recipient = new EmailAddress(item.Key, item.Value);
                        recipients.Add(recipient);
                    }

                    //Email using the Mail Helper

                    var msg = (dynamic)null; // MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    Response response;
                    string test;

                    if (recipients?.Count > 0)
                    {
                        if (recipients.Count == 1)
                        {
                            //Single recipient email
                            msg = MailHelper.CreateSingleEmail(from, recipients[0], subject, plainTextContent, htmlContent);

                            response = await client.SendEmailAsync(msg);

                            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                                success = true;

                            //This is just to check email 
                            test = msg.Serialize();
                            test = response.StatusCode.ToString();
                            test = response.Headers.ToString();
                        }
                        else
                        {
                            //Multiple recipients email
                            msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipients, subject, plainTextContent, htmlContent, false);

                            response = await client.SendEmailAsync(msg);

                            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                                success = true;

                            //This is just to check email 
                            test = msg.Serialize();
                            test = response.StatusCode.ToString();
                            test = response.Headers.ToString();

                        }
                    }
                }
            }
            catch (Exception)
            {
                success = false;
                //TODO ERROR HANDLING
            }

            return success;
        }
    }
}