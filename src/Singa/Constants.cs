using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singa
{
    public static class Constants
    {
        #region Invite New Members

        public const string InvitationString = "Hello,\n\n {0} has requested you to join Signa. \n\n Please click the following link to sign up if you do not have an account: \n {1} \n\n Please click on following link to acknowledge if you already have an account:\n {2} \n\n {3} \n\n Thanks & Regards,\n Webmasters \n Signa";
        public enum InviteTypes { Join = 0, Friend = 1, Group = 2 }
        public enum InvitationStatuses { ConfirmedInvite, SentInvite }

        #endregion
    }
}
