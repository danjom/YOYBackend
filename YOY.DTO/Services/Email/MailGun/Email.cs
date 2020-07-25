using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnjoyIt.DTO.Services.Email.MailGun
{
    public class Email
    {

        /// <summary>
        /// The email address that the email was sent to
        /// </summary>
        public List<KeyValuePair<string,string>> To { get; set; }

        /// <summary>
        /// The HTML body of the email
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// The email address the email was sent from
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Sender display name
        /// </summary>
        public string SenderName { set; get; }

        /// <summary>
        /// The Text body of the email
        /// </summary>
        public string Text { get; set; }
        

        /// <summary>
        /// Number of attachments included in email
        /// </summary>
        public int Attachments { get; set; }

        /// <summary>
        /// The subject of the email
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Domain from which email send
        /// </summary>
        public string CustomDomain { set; get; }
        
    }
}
