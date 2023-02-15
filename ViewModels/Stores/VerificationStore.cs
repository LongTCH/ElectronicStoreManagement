using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Stores;

public class VerificationStore
{
    public string? Id { get; set; }
    public string? VerificationCode { get; set; }
    public string? EmailMark { get; set; }
}
