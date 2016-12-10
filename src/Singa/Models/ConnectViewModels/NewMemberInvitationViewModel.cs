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
        public ApplicationUser Senderid { get; set; }
        public int? BecameUserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }
        public int? TargetPageId { get; set; }
        public InviteTypes InvitationType { get; set; }
        public InvitationStatuses InvitationStatus { get; set; }

        public IList<NewMemberInvitationViewModel> MyInvites { get; set; }

    }
}
