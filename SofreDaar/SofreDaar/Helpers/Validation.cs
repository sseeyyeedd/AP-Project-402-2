namespace SofreDaar.Helpers;
using System.Text.RegularExpressions;
public static class Validation
{
    public static bool IsName(this string username)
    {
        string pattern = @"^[a-zA-Z\u0600-\u06FF\s]{3,32}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(username);
    }
    public static bool IsEmail(this string email)   
    {
        string pattern = @"^[a-zA-Z0-9]{3,32}@[a-zA-Z]{3,32}\.[a-zA-Z]{2,3}$";;
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
        
    }
    public static bool IsPhoneNumber(this string phoneNumber)
    {
        string pattern = @"^09[0-9]{9}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(phoneNumber);
    }
    public static bool IsClientPassword(this string password)
    {
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,32}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(password);
    }

    public static bool IsRestaurantPassword(this string password)
    {
        string pattern = @"^[0-9]{8}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(password);
    }
    public static bool IsUsername(this string username)
    {
        string pattern = @"^[a-zA-Z0-9]{3,32}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(username);
    }
}