using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using ViewModels.Stores;
using ViewModels.NotifyControlViewModels;
using Models;
using ViewModels.MyMessageBox;

namespace ViewModels.Services;

public class EmailVerificationService : INavigationService
{
    private readonly ModalNavigationService<VerifyEmailViewModel> _navigationService;
    private readonly VerificationStore _store;
    private readonly DataProvider _dataProvider;
    public EmailVerificationService(VerificationStore Store, 
        ModalNavigationService<VerifyEmailViewModel> navigationService,
        DataProvider dataProvider)
    {
        _navigationService = navigationService;
        _store = Store;
        _dataProvider = dataProvider;
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
        // Check Valid
        var account = _dataProvider.GetAcountByID(_store.Id);
        if (account == null)
        {
            ErrorNotifyViewModel.Instance.Show("Can not find your account");
            return;
        }
        //Connect Email Here
        string from = "inbaicualong@gmail.com";
        string pass = "ccxwnfnfgoysqreu";
        MailMessage mess = new MailMessage();
        mess.Subject = "Reset Password";
        mess.From = new MailAddress(from);
        mess.To.Add(account.EmailAddress);
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
            ErrorNotifyViewModel.Instance.Show(ex.Message, "Failed");
        }
        _store.VerificationCode = _randomVerifyCode;
    }

}
