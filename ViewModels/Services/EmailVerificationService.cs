using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using ViewModels.Stores;
using ViewModels.NotifyControlViewModels;
using Models;
using ViewModels.MyMessageBox;
using System.Text;
using System.Linq;

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
        var account = _dataProvider.GetAcount(_store.Id);
        if (account == null)
        {
            ErrorNotifyViewModel.Instance!.Show("Can not find your account");
            return;
        }
        StringBuilder builder = new();
        int i = account.EmailAddress.IndexOf("@");
        if (i / 2 > 2)
        {
            builder.Append(account.EmailAddress[..2]);
            builder.Append(new string('*', i - 1));
        }
        else
        {
            builder.Append(account.EmailAddress.ElementAt(0));
            builder.Append(new string('*', i - 1));
        }
        builder.Append(account.EmailAddress[i..]);
        _store.EmailMark = builder.ToString();
        _navigationService?.Navigate();
        Task sendMail = new(() => SendMail(account.EmailAddress));
        sendMail.Start();

    }
    private void SendMail(string mail)
    {
        Random rnd = new();
        int ran = rnd.Next(100_000, 999_999);
        string _randomVerifyCode = ran.ToString();

        //Connect Email Here
        string from = "inbaicualong@gmail.com";
        string pass = "ccxwnfnfgoysqreu";
        MailMessage mess = new()
        {
            Subject = "Reset Password",
            From = new MailAddress(from)
        };
        mess.To.Add(mail);
        mess.Body = "From : Electronic Store" + "<br>Your verification code : " + _randomVerifyCode;
        mess.IsBodyHtml = true;
        SmtpClient client = new("smtp.gmail.com")
        {
            EnableSsl = true,
            Port = 587,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(from, pass)
        };

        try
        {
            client.Send(mess);
        }
        catch (Exception ex)
        {
            ErrorNotifyViewModel.Instance!.Show(ex.Message, "Failed");
        }
        _store.VerificationCode = _randomVerifyCode;
    }

}
