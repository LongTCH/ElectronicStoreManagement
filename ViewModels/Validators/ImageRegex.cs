using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Validators;

public static class ImageRegex
{
    public static string Get() => @"\.(jpe?g|png|bmp|gif|tiff?|ico|webp)$";
}
