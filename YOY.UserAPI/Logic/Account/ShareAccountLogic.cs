using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.DAO.Entities;

namespace YOY.UserAPI.Logic.Account
{
    public static class ShareAccountLogic
    {

        public static string AssignAccountCode(BusinessObjects businessObjects, string userId, string name, string email)
        {
            Random r = new Random();
            Guid g;
            string emailComponent;
            string code;

            bool codeAssigned;
            do
            {
                g = Guid.NewGuid();
                string guidString = Convert.ToBase64String(g.ToByteArray());

                guidString = guidString.Replace("=", "");
                guidString = guidString.Replace("+", "");

                int rInt = r.Next(0, guidString.Length - 3);
                guidString = guidString.Substring(rInt, 3);

                email = email.Replace(@"\", "");
                email = email.Replace(@"-", "");
                email = email.Replace(@"_", "");
                emailComponent = email.Split('@')[0];

                if (emailComponent.Length > 3)
                {
                    rInt = r.Next(0, emailComponent.Length - 3);

                    emailComponent = emailComponent.Substring(rInt, 3);
                }

                name = name.Trim().ToLower();

                if (name.Length > 4)
                {
                    name = name.Substring(0, 4);
                }

                code = name + emailComponent + guidString;

                code.Replace('/', 'l');

                codeAssigned = businessObjects.Users.AddAccountCode(userId, code);

            } while (!codeAssigned);

            return code;
        }

    }
}
