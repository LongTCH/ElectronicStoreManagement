using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels.Stores;
using ViewModels.Stores.Navigations;
using ViewModels.NotifyControlViewModels;

namespace ViewModels.Services;

public class EmailVerificationService : INavigationService
{
    private readonly ModalNavigationService<VerifyEmailViewModel> _navigationService;
    private readonly EmailStore _emailStore;
    public EmailVerificationService(EmailStore emailStore, ModalNavigationService<VerifyEmailViewModel> navigationService)
    {
        _navigationService = navigationService;
        _emailStore = emailStore;
    }

    public void Navigate()
    {
        _navigationService?.Navigate();
        Task sendMail = new(SendMail);
        sendMail.Start();

    }
    private void SendMail()
    {
        Random rnd = new Random();
        int ran = rnd.Next(100_000, 999_999);
        string _randomVerifyCode = ran.ToString();
        //Connect Email Here
        string from = "inbaicualong@gmail.com";
        string pass = "ccxwnfnfgoysqreu";
        MailMessage mess = new MailMessage();
        mess.From = new MailAddress(from);
        mess.To.Add(_emailStore.Email);
        mess.Body = "From : Electronic Store" + "<br>Your verification code : " + _randomVerifyCode;
        mess.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.gmail.com");
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new NetworkCredential(from, pass);

        try
        {
            client.Send(mess);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        _emailStore.VerificationCode = _randomVerifyCode;
    }

}
