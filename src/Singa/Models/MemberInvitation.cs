using System;

namespace Singa.Models
{
    public class MemberInvitation
    {
        public DateTime Version { get; set; }
        public string Guid { get; set; }
        public string Email { get; set; }
        public ApplicationUser Senderid { get; set; }
        public int? BecameUserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }
        public int? TargetPageId { get; set; }
        public InviteTypes InvitationType { get; set; }
        public InvitationStatuses InvitationStatus { get; set; }
    }

    public enum InviteTypes { Join = 0, Friend = 1, Group = 2 }

    public enum InvitationStatuses { Confirmed, Requested}
}
