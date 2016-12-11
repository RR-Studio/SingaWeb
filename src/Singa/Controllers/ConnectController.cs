using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Singa.Models;
using Singa.Models.ConnectViewModels;
using Microsoft.AspNetCore.Identity;
using Singa.Services;
using Singa.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Singa.Controllers
{
    public class ConnectController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private ApplicationDbContext _context { get; }

        public ConnectController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            context = _context;
        }

        public IActionResult Index(string notification = null)
        {
            //Todo: get from DB
            var myInvites = new List<NewMemberInvitationViewModel>();
            var myRequests = new List<string>();

            ViewBag.myInvites = myInvites;
            ViewBag.myRequests = myRequests;
            
            return View();
        }

        #region Send Invite or Request

        public async Task<IActionResult> Invite(Constants.InviteTypes invitationType)
        {
            var loggedInUser = await GetCurrentUserAsync();

            var senderId = new ApplicationUser() { Id = loggedInUser.Id };

            var model = new NewMemberInvitationViewModel()
            {
                SenderUser = senderId,
                CreateDate = DateTime.Now,
                Version = DateTime.Now,
                InvitationType = invitationType
            };

            //Todo: take from DB
            model.MyInvites = new List<NewMemberInvitationViewModel>();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Invite(int id, Constants.InviteTypes invitationType, NewMemberInvitationViewModel model)
        {
            var senderUser = await GetCurrentUserAsync();

            try
            {     
                var memberInvitation = new MemberInvitation()
                {
                    Guid = Guid.NewGuid().ToString(),
                    Senderid = senderUser,
                    Email = model.Email,
                    InvitationType = invitationType,
                    CreateDate = DateTime.Now,
                    TargetPageId = id,
                    InvitationStatus = Constants.InvitationStatuses.SentInvite
                };
                
                _context.Entry(memberInvitation).State = EntityState.Added;

                await _context.SaveChangesAsync();

                Log.Debug("User {@senderUser} invited new member {@TargetPageId}", senderUser, id);

                SendInvitationEmail(senderUser, id, model.UserMessage);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "User {@senderUser} invited new member {@TargetPageId}", senderUser, id);
            }
            
            model.MyInvites = new List<NewMemberInvitationViewModel>();

            return RedirectToAction(nameof(Index), new { Notification = "ѕриглашение было отослано пользователю:" + model.SenderUser});
        }
        #endregion


        #region Helpers

        //ToDo: duplicate with helper method in AccountController
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private async void SendInvitationEmail(ApplicationUser senderUser, int targetPageId, string message)
        {
            try
            {
                string[] msgParam = new string[4];

                msgParam[0] = senderUser.UserName;
                msgParam[1] = Url.Action("Register", "Account");

                //ToDo
                msgParam[2] = "";
                msgParam[3] = string.IsNullOrEmpty(message) ? "" : "\n----------\n" + message + "\n----------\n";
                
                var strMsg = string.Format(Constants.InvitationString, msgParam);

                var email = await _userManager.GetEmailAsync(senderUser);

                await _emailSender.SendEmailAsync(email, "Security Code", strMsg);

                Log.Debug("{@SenderUser} sends invite email to a new member {@TargetPageId}", message, targetPageId);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "{@SenderUser} sends invite email to a new member {@TargetPageId}", message, targetPageId);
            }
        }

        #endregion
    }
}