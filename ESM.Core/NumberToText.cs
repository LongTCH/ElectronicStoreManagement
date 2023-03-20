using System;

namespace ESM.Core;

public static class NumberToText
{

    public static string FuncNumberToText(double inputNumber, bool suffix = true)
    {
        char[] separator = { ',', '.' };
        string[] strlist = inputNumber.ToString().Split(separator);
        string result;
        if (strlist.Length < 2) result = IntNumberToText(Convert.ToUInt64(strlist[0]), true);
        else result = IntNumberToText(Convert.ToUInt64(strlist[0]), false) + " phẩy " + IntNumberToText(Convert.ToUInt64(strlist[1]), suffix);
        result = char.ToUpper(result[0]) + result[1..];
        return result;
    }
    public static string IntNumberToText(UInt64 inputNumber, bool suffix = true)
    {
        string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
        bool isNegative = false;

        // -12345678.3445435 => "-12345678"
        string sNumber = inputNumber.ToString("#");
        double number = Convert.ToDouble(sNumber);
        if (number < 0)
        {
            number = -number;
            sNumber = number.ToString();
            isNegative = true;
        }


        int ones, tens, hundreds;

        int positionDigit = sNumber.Length;   // last -> first

        string result = " ";


        if (positionDigit == 0)
            result = unitNumbers[0] + result;
        else
        {
            // 0:       ###
            // 1: nghìn ###,###
            // 2: triệu ###,###,###
            // 3: tỷ    ###,###,###,###
            int placeValue = 0;

            while (positionDigit > 0)
            {
                // Check last 3 digits remain ### (hundreds tens ones)
                tens = hundreds = -1;
                ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                positionDigit--;
                if (positionDigit > 0)
                {
                    tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                    }
                }

                if (ones > 0 || tens > 0 || hundreds > 0 || placeValue == 3)
                    result = placeValues[placeValue] + result;

                placeValue++;
                if (placeValue > 3) placeValue = 1;

                if (ones == 1 && tens > 1)
                    result = "mốt " + result;
                else
                {
                    if (ones == 5 && tens > 0)
                        result = "lăm " + result;
                    else if (ones > 0)
                        result = unitNumbers[ones] + " " + result;
                }
                if (tens < 0)
                    break;
                else
                {
                    if (tens == 0 && ones > 0) result = "lẻ " + result;
                    if (tens == 1) result = "mười " + result;
                    if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                }
                if (hundreds < 0) break;
                else
                {
                    if (hundreds > 0 || tens > 0 || ones > 0)
                        result = unitNumbers[hundreds] + " trăm " + result;
                }
                result = " " + result;
            }
        }
        result = result.Trim();
        if (isNegative) result = "Âm " + result;
        //if (!isNegative) result = char.ToUpper(result[0]) + result.Substring(1);
        return result + (suffix ? " đồng" : "");
    }
}
