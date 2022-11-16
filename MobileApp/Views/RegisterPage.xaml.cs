﻿using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        UserService us;
        ShipyardService ss;
        public RegisterPage()
        {
            InitializeComponent();
            us = new UserService();
            ss = new ShipyardService();
        }
        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                string code = "";
                code = us.generateCode();

                var msg = validation();
                if (msg != "")
                {
                    throw new InvalidOperationException(msg);
                }

                User obj = new User();
                obj.Email = Email.Text;
                obj.Username = Name.Text;
                obj.Password = Password.Text;
                obj.IsVerified = false;
                obj.VerifiedCode = code;
                obj.Role = "User";
                us.InsertUser(obj);

                Shipyard objS = new Shipyard();
                objS.ShipyardName = Name.Text;
                objS.ShipyardEmail = Email.Text;
                objS.ShipyardPhone = "00000000";
                objS.ShipyardAddress = "example street no 32";
                objS.UserID = obj.ID;
                ss.InsertShipyard(objS);

                string html = EmailTemplate(Name.Text, code);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                // Decode the html in the txtBody TextBox...    
                string body = HttpUtility.HtmlDecode(html);
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
                mail.AlternateViews.Add(alternate);
                
                mail.From = new MailAddress("shipapp.devenv@gmail.com", "Shipyard App");
                mail.To.Add(Email.Text);
                mail.Subject = "Verification Code";
                mail.IsBodyHtml = true;
                mail.Body = body;
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("shipapp.devenv@gmail.com", "fpgmxtmmfaunqcci");

                SmtpServer.Send(mail);
                // await DisplayActionSheet("Operation", "Cancel", null, "Update", "Delete");
                await DisplayAlert("Register complete!", "Verification Code has been sent", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
                //string res = await DisplayActionSheet("Registration Complete", "Cancel", null, "OK");

                //switch (res)
                //{
                //    case "OK":
                //        await Shell.Current.GoToAsync("//LoginPage");
                //        break;
                //}

                //DisplayAlert("Register complete!", "Verification Code has been sent", "OK");
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Something's wrong", ex.Message, "OK");
                Email.Text = "";
                Name.Text = "";
                Password.Text = "";
                ConfPassword.Text = "";
            }
        }
        private string EmailTemplate(string username , string code)
        {
            string html = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'> <html xmlns='http://www.w3.org/1999/xhtml'> <head> <meta name='viewport' content='width=device-width, initial-scale=1.0' /> <meta name='x-apple-disable-message-reformatting' /> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /> <meta name='color-scheme' content='light dark' /> <meta name='supported-color-schemes' content='light dark' /> <title></title> <style type='text/css' rel='stylesheet' media='all'> @import url('https://fonts.googleapis.com/css?family=Nunito+Sans:400,700&display=swap'); body { width: 100% !important; height: 100%; margin: 0; -webkit-text-size-adjust: none; } a { color: #3869D4; } a img { border: none; } td { word-break: break-word; } .preheader { display: none !important; visibility: hidden; mso-hide: all; font-size: 1px; line-height: 1px; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; } body, td, th { font-family: 'Nunito Sans', Helvetica, Arial, sans-serif; } h1 { margin-top: 0; color: #333333; font-size: 22px; font-weight: bold; text-align: left; } h2 { margin-top: 0; color: #333333; font-size: 16px; font-weight: bold; text-align: left; } h3 { margin-top: 0; color: #333333; font-size: 14px; font-weight: bold; text-align: left; } td, th { font-size: 16px; } p, ul, ol, blockquote { margin: .4em 0 1.1875em; font-size: 16px; line-height: 1.625; } p.sub { font-size: 13px; } .align-right { text-align: right; } .align-left { text-align: left; } .align-center { text-align: center; } .u-margin-bottom-none { margin-bottom: 0; } .button { background-color: #3869D4; border-top: 10px solid #3869D4; border-right: 18px solid #3869D4; border-bottom: 10px solid #3869D4; border-left: 18px solid #3869D4; display: inline-block; color: #FFF; text-decoration: none; border-radius: 3px; box-shadow: 0 2px 3px rgba(0, 0, 0, 0.16); -webkit-text-size-adjust: none; box-sizing: border-box; } .button--green { background-color: #22BC66; border-top: 10px solid #22BC66; border-right: 18px solid #22BC66; border-bottom: 10px solid #22BC66; border-left: 18px solid #22BC66; } .button--red { background-color: #FF6136; border-top: 10px solid #FF6136; border-right: 18px solid #FF6136; border-bottom: 10px solid #FF6136; border-left: 18px solid #FF6136; } @media only screen and (max-width: 500px) { .button { width: 100% !important; text-align: center !important; } } /* Attribute list ------------------------------ */ .attributes { margin: 0 0 21px; } .attributes_content { background-color: #F4F4F7; padding: 16px; } .attributes_item { padding: 0; } .related { width: 100%; margin: 0; padding: 25px 0 0 0; -premailer-width: 100%; -premailer-cellpadding: 0; -premailer-cellspacing: 0; } .related_item { padding: 10px 0; color: #CBCCCF; font-size: 15px; line-height: 18px; } .related_item-title { display: block; margin: .5em 0 0; } .related_item-thumb { display: block; padding-bottom: 10px; } .related_heading { border-top: 1px solid #CBCCCF; text-align: center; padding: 25px 0 10px; } .discount { width: 100%; margin: 0; padding: 24px; -premailer-width: 100%; -premailer-cellpadding: 0; -premailer-cellspacing: 0; background-color: #F4F4F7; border: 2px dashed #CBCCCF; } .discount_heading { text-align: center; } .discount_body { text-align: center; font-size: 15px; } /* Social Icons ------------------------------ */ .social { width: auto; } .social td { padding: 0; width: auto; } .social_icon { height: 20px; margin: 0 8px 10px 8px; padding: 0; } .purchase { width: 100%; margin: 0; padding: 35px 0; -premailer-width: 100%; -premailer-cellpadding: 0; -premailer-cellspacing: 0; } .purchase_content { width: 100%; margin: 0; padding: 25px 0 0 0; -premailer-width: 100%; -premailer-cellpadding: 0; -premailer-cellspacing: 0; } .purchase_item { padding: 10px 0; color: #51545E; font-size: 15px; line-height: 18px; } .purchase_heading { padding-bottom: 8px; border-bottom: 1px solid #EAEAEC; } .purchase_heading p { margin: 0; color: #85878E; font-size: 12px; } .purchase_footer { padding-top: 15px; border-top: 1px solid #EAEAEC; } .purchase_total { margin: 0; text-align: right; font-weight: bold; color: #333333; } .purchase_total--label { padding: 0 15px 0 0; } body { background-color: #F2F4F6; color: #51545E; } p { color: #51545E; } .email-wrapper { width: 100%; margin: 0; padding: 0; -premailer-width: 100%; -premailer-cellpadding: 0; -premailer-cellspacing: 0; background-color: #F2F4F6; } .email-content { width: 100%; margin: 0; padding: 0; -premailer-width: 100%; -premailer-cellpadding: 0; -premailer-cellspacing: 0; } /* Masthead ----------------------- */ .email-masthead { padding: 25px 0; text-align: center; } .email-masthead_logo { width: 94px; } .email-masthead_name { font-size: 16px; font-weight: bold; color: #A8AAAF; text-decoration: none; text-shadow: 0 1px 0 white; } .email-body { width: 100%; margin: 0; padding: 0; -premailer-width: 100%; -premailer-cellpadding: 0; -premailer-cellspacing: 0; } .email-body_inner { width: 570px; margin: 0 auto; padding: 0; -premailer-width: 570px; -premailer-cellpadding: 0; -premailer-cellspacing: 0; background-color: #FFFFFF; } .email-footer { width: 570px; margin: 0 auto; padding: 0; -premailer-width: 570px; -premailer-cellpadding: 0; -premailer-cellspacing: 0; text-align: center; } .email-footer p { color: #A8AAAF; } .body-action { width: 100%; margin: 30px auto; padding: 0; -premailer-width: 100%; -premailer-cellpadding: 0; -premailer-cellspacing: 0; text-align: center; } .body-sub { margin-top: 25px; padding-top: 25px; border-top: 1px solid #EAEAEC; } .content-cell { padding: 45px; } @media only screen and (max-width: 600px) { .email-body_inner, .email-footer { width: 100% !important; } } @media (prefers-color-scheme: dark) { body, .email-body, .email-body_inner, .email-content, .email-wrapper, .email-masthead, .email-footer { background-color: #333333 !important; color: #FFF !important; } p, ul, ol, blockquote, h1, h2, h3, span, .purchase_item { color: #FFF !important; } .attributes_content, .discount { background-color: #222 !important; } .email-masthead_name { text-shadow: none !important; } } :root { color-scheme: light dark; supported-color-schemes: light dark; } </style> </head> <body> <span class='preheader'>Thanks for trying out " + username + ". We’ve pulled together some information and resources to help you get started.</span> <table class='email-wrapper' width='100%' cellpadding='0' cellspacing='0' role='presentation'> <tr> <td align='center'> <table class='email-content' width='100%' cellpadding='0' cellspacing='0' role='presentation'> <tr> <td class='email-masthead'> <a href='https://example.com' class='f-fallback email-masthead_name'> Mobile App </a> </td> </tr> <tr> <td class='email-body' width='570' cellpadding='0' cellspacing='0'> <table class='email-body_inner' align='center' width='570' cellpadding='0' cellspacing='0' role='presentation'> <tr> <td class='content-cell'> <div class='f-fallback'> <h1>Welcome, " + username + "!</h1> <p>Here's your verification code : " + code + "</p> </div> </td> </tr> </table> </td> </tr> <tr> <td> <table class='email-footer' align='center' width='570' cellpadding='0' cellspacing='0' role='presentation'> <tr> <td class='content-cell' align='center'> <p class='f-fallback sub align-center'> Mobile App <br>1234 Street Rd. <br>Suite 1234 </p> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>";
            return html;
        }
        private void Login_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//LoginPage");
        }

        public string validation()
        {
            try
            {
                string msg = "";

                if (Email.Text == "")
                {
                    msg = "Email must be filled";
                }

                if (Name.Text == "")
                {
                    msg = "Username must be filled";
                }

                if (Password.Text == "")
                {
                    msg = "Password must be filled";
                }

                if (ConfPassword.Text == "")
                {
                    msg = "Confirm Password must be filled";
                }

                var cekEmail = us.GetDataUserByEmail(Email.Text);
                if (cekEmail.Count() > 0)
                {
                    msg = "Email has been used!";
                }

                if (ConfPassword.Text != Password.Text)
                {
                    msg = "Confirm password must be the same with password";
                }

                if (IsValidEmail(Email.Text) == false)
                {
                    msg = "Email is not valid!";
                }

                return msg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}