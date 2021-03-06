﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Singa.Models
{
    public class MemberInvitation
    {
        [Key]
        public string Guid { get; set; }
        public DateTime Version { get; set; }
        
        public string Email { get; set; }
        public ApplicationUser Senderid { get; set; }
        public int? BecameUserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? TargetPageId { get; set; }
        public Constants.InviteTypes InvitationType { get; set; }
        public Constants.InvitationStatuses InvitationStatus { get; set; }
    }    
}
