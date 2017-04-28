﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Owin;
using Voyage.Models;
using Voyage.Models.Enum;
using Voyage.Services.PasswordRecovery;
using Voyage.Web.Filters;
using Voyage.Web.Models;

namespace Voyage.Web.Controllers
{
    public class ForgotpasswordController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new ForgotPasswordModel { ForgotPasswordStep = ForgotPasswordStep.VerifyUser };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClientAuthorize]
        public async Task<ActionResult> Index(ForgotPasswordModel model)
        {
            if (Request.HttpMethod != "POST")
                return View(model);

            try
            {
                var context = HttpContext.GetOwinContext().GetAutofacLifetimeScope();
                var passwordRecoverService = context.Resolve<IPasswordRecoverService>();

                if (!string.IsNullOrEmpty(Request.Form.Get("submit.VerifyUser")))
                {
                    // validate user and phone number
                    model = await passwordRecoverService.ValidateUserInfoAsync(Request.Form.Get("username"), Request.Form.Get("phonenumber"));

                    // if there is not error set time allowance to 10 minutes and move to next step
                    if (!model.HasError)
                    {
                        // save user id and password recovery token to session for next step to identify if it is the same session
                        Session["appUser"] = new UserApplicationSession
                        {
                            UserId = model.UserId
                        };

                        // set this password recovery session for 10 minutes after the security code is sent
                        Session.Timeout = 10;
                    }
                }
                else if (!string.IsNullOrEmpty(Request.Form.Get("submit.VerifySecurityCode")))
                {
                    // session must not be new
                    if (!IsNewSession(model))
                    {
                        // verify code
                        var appUser = Session["appUser"] as UserApplicationSession;
                        model = await passwordRecoverService.VerifyCodeAsync(appUser, Request.Form.Get("code"));

                        // after verifying code successfully save code for later use
                        if (!model.HasError)
                        {
                            appUser.PasswordRecoveryToken = model.PasswordRecoveryToken;
                            Session["appUser"] = appUser;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(Request.Form.Get("submit.VerifySecurityAnswers")))
                {
                    // session must not be new
                    if (!IsNewSession(model))
                    {
                        var appUser = Session["appUser"] as UserApplicationSession;
                        model = passwordRecoverService.VerifySecurityAnswers(appUser?.UserId, new List<string>());
                    }
                }
                else if (!string.IsNullOrEmpty(Request.Form.Get("submit.ResetPassword")))
                {
                    // session must not be new
                    if (!IsNewSession(model))
                    {
                        // reset user password
                        var appUser = Session["appUser"] as UserApplicationSession;
                        model = await passwordRecoverService.ResetPasswordAsync(appUser, Request.Form.Get("newpassword"), Request.Form.Get("confirmnewpassword"));

                        // redirect user to log in if no error
                        if (!model.HasError)
                        {
                            Session.Clear();
                            return RedirectPermanent(Request.QueryString["ReturnUrl"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log error to database
                model.HasError = true;
                model.ErrorMessage = ex.Message;
            }

            return View(model);
        }

        /// <summary>
        /// check if current request is new session
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsNewSession(ForgotPasswordModel model)
        {
            if (!Session.IsNewSession)
                return false;

            model.HasError = true;
            model.ErrorMessage = "Your allowance time has expired.";
            return true;
        }
    }
}