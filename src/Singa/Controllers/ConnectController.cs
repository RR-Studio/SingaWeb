using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Singa.Models;
using Singa.Models.ConnectViewModels;
using Microsoft.AspNetCore.Identity;

namespace Singa.Controllers
{
    public class ConnectController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ConnectController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;        
        }


        public IActionResult Index()
        {
            return View();
        }

        #region Send Invite or Request

        public async Task<IActionResult> Invite(int? id, InviteTypes invitationType)
        {
            var loggedInUser = await GetCurrentUserAsync();

            var senderId = new ApplicationUser() { Id = loggedInUser.Id };

            var model = new NewMemberInvitationViewModel()
            {
                Senderid = senderId,
                CreateDate = DateTime.Now,
                Version = DateTime.Now,
                InvitationType = invitationType
            };

            //Todo: take from DB
            model.MyInvites = new List<NewMemberInvitationViewModel>();

            return View(model);
        }

        [HttpPost]
        public ActionResult Invite(int? id, InviteTypes type, MemberInvitation model)
        {            
            return View(model);
        }
        #endregion


        #region Helpers

        //ToDo: duplicate with helper method in AccountController
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        #endregion
    }
}