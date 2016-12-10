using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singa.Models.ConnectViewModels
{
    public class NewMemberInvitationViewModel
    {
        public string Guid { get; set; }
        public DateTime Version { get; set; }
        public string Email { get; set; }
        public ApplicationUser SenderUser { get; set; }
        public int? BecameUserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? TargetPageId { get; set; }
        public Constants.InviteTypes InvitationType { get; set; }
        public Constants.InvitationStatuses InvitationStatus { get; set; }

        public IList<NewMemberInvitationViewModel> MyInvites { get; set; }
        public string UserMessage { get; set; }

    }
}
