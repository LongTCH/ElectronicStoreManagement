using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using ViewModels.Stores;
using ViewModels.NotifyControlViewModels;
using ViewModels.MyMessageBox;
using System.Text;
using System.Linq;
using Models.Interfaces;

namespace ViewModels.Services;

public class EmailVerificationService : INavigationService
{
    private readonly ModalNavigationService<VerifyEmailViewModel> _navigationService;
    private readonly VerificationStore _store;
    private readonly IUnitOfWork _unitOfWork;
    public EmailVerificationService(VerificationStore Store,
        ModalNavigationService<VerifyEmailViewModel> navigationService,
        IUnitOfWork unitOfWork)
    {
        _navigationService = navigationService;
        _store = Store;
        _unitOfWork = unitOfWork;
    }

    public void Navigate()
    {
        var account = _unitOfWork.Accounts.Get(_store.Id);
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
            builder.Append(new string('*', i - 2));
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
            _store.VerificationCode = _randomVerifyCode;
        }
        catch (Exception ex)
        {
            ErrorNotifyViewModel.Instance!.Show(ex.Message, "Failed");
        }
        
    }

}
