using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using APP0200025.Models;
using DATA0200025;

namespace APP0200025.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(String ParentID, string returnUrl)
        {
            NameValueCollection values = new NameValueCollection();
            string sUserName = CString.SafeString(Request.Form[ParentID + "_sUserName"]).Trim();
            string sMatKhau = CString.SafeString(Request.Form[ParentID + "_sPassword"]).Trim();
            string sKeep = CString.SafeString(Request.Form["rememberCheckbox"]);

            if (string.IsNullOrEmpty(sUserName))
            {
                values.Add("err_sUserName", "Bạn chưa nhập tên đăng nhập!");
            }
            if (string.IsNullOrEmpty(sMatKhau))
            {
                values.Add("err_sMatKhau", "Bạn chưa nhập mật khẩu!");
            }

            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                return View();
            }

            bool bRememberMe = false;
            if (sKeep == "on")
            {
                bRememberMe = true;
            }

            if (sUserName.IndexOf('@') > -1)
            {
                var user = await UserManager.FindByEmailAsync(sUserName);
                if (user == null)
                {
                    values.Add("err_sMessage", "UserName không tồn tại trên hệ thống!");
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(0), values[0]);
                    return View();
                }
                else
                {
                    sUserName = user.UserName;
                }
            }

            int iCheck = CPQ_NGUOIDUNG.Check_NguoiDung(sUserName);
            if(iCheck == 1)
            {
                var result = await SignInManager.PasswordSignInAsync(sUserName, sMatKhau, bRememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = bRememberMe });
                    case SignInStatus.Failure:
                    default:
                        values.Add("err_sMessage", "Tên đăng nhập hoặc mật khẩu không đúng!");
                        ModelState.AddModelError(ParentID + "_" + values.GetKey(0), values[0]);
                        return View();
                }
            }
            else
            {
                values.Add("err_sMessage", "Tên đăng nhập hoặc mật khẩu không đúng!");
                ModelState.AddModelError(ParentID + "_" + values.GetKey(0), values[0]);
                return View();
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string ParentID)
        {
            NameValueCollection values = new NameValueCollection();
            string sFullName = CString.SafeString(Request.Form[ParentID + "_sFullName"]).Trim();
            string sEmail = CString.SafeString(Request.Form[ParentID + "_sEmail"]).Trim();
            string sUserName = CString.SafeString(Request.Form[ParentID + "_sUserName"]).Trim();
            string sMatKhau = CString.SafeString(Request.Form[ParentID + "_sPassword"]).Trim();
            string sReMatKhau = CString.SafeString(Request.Form[ParentID + "_sRePassword"]).Trim();
            string sKeep = CString.SafeString(Request.Form["rememberCheckbox"]);

            if (string.IsNullOrEmpty(sFullName))
            {
                values.Add("err_sFullName", "Bạn chưa nhập họ tên!");
            }
            if (string.IsNullOrEmpty(sEmail))
            {
                values.Add("err_sEmail", "Bạn chưa nhập email!");
            }
            else
            {
                if (CommonFunction.IsEmailValid(sEmail) == false)
                {
                    values.Add("err_sEmail", "Email của bạn chưa đúng định dạng!");
                }
            }
            if (string.IsNullOrEmpty(sUserName))
            {
                values.Add("err_sUserName", "Bạn chưa nhập tên đăng nhập!");
            }
            if (string.IsNullOrEmpty(sMatKhau))
            {
                values.Add("err_sPassword", "Bạn chưa nhập mật khẩu!");
            }
            if (string.IsNullOrEmpty(sReMatKhau))
            {
                values.Add("err_sRePassword", "Bạn chưa nhập lại mật khẩu!");
            }
            if (sReMatKhau != sMatKhau)
            {
                values.Add("err_sRePassword", "Nhập lại mật khẩu chưa chính xác!");
            }

            //if (response.Success == false)
            //{
            //    values.Add("err_sCapCha", "Error From Google ReCaptcha : " + response.ErrorMessage[0].ToString());
            //}
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                return View();
            }

            bool bRememberMe = false;
            if (sKeep == "on")
            {
                bRememberMe = true;
            }

            if (ModelState.IsValid && bRememberMe == true)
            {
                var user = new ApplicationUser { UserName = sUserName, Email = sEmail };
                if (CPQ_NGUOIDUNG.Check_NguoiDung(sUserName) == 0)
                {
                    var result = await UserManager.CreateAsync(user, sMatKhau);
                    if (result.Succeeded)
                    {
                        CPQ_NGUOIDUNG.Insert(sUserName, "1-1", sFullName, sEmail, 1);

                        // Dang nhap he thong ngay
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                        ////Gui mail Confirm
                        //CSendMail sendMail = new CSendMail();
                        //String strSubject = "Email confirmation";
                        //String strBody = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to comlete your registration: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", user.UserName, Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, Request.Url.Scheme));

                        //sendMail.sendMail(strSubject, strBody, sEmail);

                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        values.Add("err_sFullName", "Có lỗi trong quá trình đăng ký! Gọi số điện thoại hỗ trợ!");
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [Authorize]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(String ParentID)
        {
            String sUser = User.Identity.Name;

            string _sMatKhauCu = CString.SafeString(Request.Form[ParentID + "_sMatKhauCu"]).Trim();
            string _sMatKhau = CString.SafeString(Request.Form[ParentID + "_sMatKhau"]).Trim();
            string _sReMatKhau = CString.SafeString(Request.Form[ParentID + "_sReMatKhau"]).Trim();

            NameValueCollection values = new NameValueCollection();
            if (string.IsNullOrEmpty(_sMatKhauCu))
            {
                values.Add("err_sMatKhauCu", "Bạn chưa nhập mật khẩu cũ!");
            }
            if (string.IsNullOrEmpty(_sMatKhau))
            {
                values.Add("err_sMatKhau", "Bạn chưa nhập mật khẩu mới!");
            }
            if (string.IsNullOrEmpty(_sReMatKhau))
            {
                values.Add("err_sReMatKhau", "Bạn chưa nhập lại mật khẩu mới!");
            }
            if (_sMatKhau != _sReMatKhau)
            {
                values.Add("err_sReMatKhau", "Mật khẩu nhập lại chưa đúng!");
            }
            //if (string.IsNullOrEmpty(_iID_MaKhachHang))
            //{
            //    values.Add("err_iID_MaKhachHang", "Bạn chưa chọn khách hàng!");
            //}
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                ViewData["DuLieuMoi"] = "1";
                return View();
            }
            else
            {
                var user = await UserManager.FindByNameAsync(sUser);
                if (user == null)
                {
                    return RedirectToAction("ChangePassword", "Account");
                }
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                var result = await UserManager.ResetPasswordAsync(user.Id, code, _sMatKhau);
                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmChangePassword", "Account");
                }
            }
            return RedirectToAction("ChangePassword", "Account");
        }

        public ActionResult ConfirmChangePassword()
        {
            Session.Clear();
            Session.Abandon();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return View();
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}