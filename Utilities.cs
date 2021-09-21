
////////////////////////////////////////////////////////////////////////////
//       Utilities file contains mostly static code to control            //
//       the workflow of this applicantion. It contains two main          //
//       classes. The first provides security, configuration, and         //
//       operation settings. The second provides access control to        //
//       files and folders. AI operations are embedded in Steps 2,3,      // 
//       and 4. String operations are embedded in Utilities class.        //
//       This code is writting by Dr. Hassan H. Alrehamy                  //
//       Email address: h@uobabylon.edu.iq                                //
////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Globalization;
using System.Text;

/// <summary>
/// Utilities class for managing the applicantion.
/// </summary>
public class Utilities
{
    #region Application-specific

    //////////////////////////////////////////////////////////
    //           Workflow Management                        //
    //////////////////////////////////////////////////////////
    // Applicant Control                                    //
    // Notes: On submission expiry must set to false        //
    public static readonly bool ALLOW_REG = false;          //
    public static readonly bool ALLOW_LOGIN = false;         //
    // Manager Updating Control                             //
    // Notes: Must be set by IT manager before addimssions  //
    public static readonly bool ALLOW_CONFIG = false;       //
    // Management Circles Control                           //
    // 1: Enable verification process                       //
    public static readonly bool ALLOW_VERIFY = false;       //
    // 2: Enable certification process                      //
    // Notes: Requires ALLOW_REG = false When the           //
    //        certification process starts                  //
    public static readonly bool ALLOW_CERTIFY = false;      //
    // 3: Enables Clarks to adjust exam & attendance        //
    // Notes: Requires ALLOW_VERIFY = false                 //
    public static readonly bool ALLOW_MARKING = false;      //
    // Manager Post Apply Control                           //
    // 1: Allow verifying admission results internally      //
    public static readonly bool ALLOW_CHECKING = false;     //
    // 2: Allow objection and objection management          //
    public static readonly bool ALLOW_OJECT = false;        //
    // 3: Allow sharing admission results publically        //
    // Notes: True requires:                                //
    //          ALLOW_REG = false                           //
    //          ALLOW_CONFIG = false                        //
    //          ALLOW_CERTIFY = false;                      //
    //          ALLOW_CHECKING = false;                     //
    public static readonly bool ALLOW_OUTPUT = false;        //
    // 4: Deciding whether this is final result or not      //
    public static readonly bool ISFINAL_OUTPUT = true;      //
    //////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          File management                                                                                                         //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Max uploaded file size allowed                                                                                                   //
    public static readonly int FILE_MAX_SIZE = 15728640;                                                                                //
    public static readonly int PICTURE_MAX_SIZE = 1048576;                                                                              //
    public static readonly string FILE_PHYISC_PATH = @"D:\Inetpub\vhosts\uobabylon.edu.iq\subdomains\postgradapp\httpdocs\files\";      //
    public static readonly string MEDIA_PHYISC_PATH = @"D:\Inetpub\vhosts\uobabylon.edu.iq\subdomains\postgradapp\httpdocs\media\";     //
    public static readonly string T1 = "pic|id|ic|ar|en|privil|applych|rough|expell|ata|atb|atc|atd|rec";                               //
    public static readonly string T2 = "sl|excep|cv";                                                                                   //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////////////
    //      Security Management                                                     //
    //////////////////////////////////////////////////////////////////////////////////
    // Email creds for the system to send emails                                    //
    public static readonly string APP_EMAIL = "support@uobabylon.edu.iq";           //
    public static readonly string APP_EMAIL_AUTH = "**********";                    //
    // SMS creds for the system to send messages                                    //
    // Key to allow only IT manager performing extensive operations                 //
    // E.g. results prepration, delete duplicate applicantions ... etc              //
    public static readonly string APP_KEY_AUTH = "***********";                     //
    //Prevent access to files within this URL signture                              //
    public static readonly string FILE_SECURE_URI = ".iq/files/";                   //
    public static readonly string FILE_SECURE_REDIRECT = "../Default.aspx";         //
    //////////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    //      Date Management                                                                                 //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Registration deadline                                                                                //
    public static readonly int EXPIRY_EPOCH = 1623606659;                                                   //
    // Modifications require reflection in step1 and step2 javascript (var today)                           //
    public static readonly string EXP_CONST = "2021/10/01";                                                 //
    public static readonly string AGE_CONST = "2021/05/01";                                                 //
    // Count down until admission resuts can be available publically.                                       //
    public static readonly string EXPORT_COUNTDOWN = "Jul 13, 2021 8:00:00";                               //
    private static DateTime epoch = new DateTime(1970, 1, 1);                                               //
    private static DateTime epochlong = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);      //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Misc management                                                                                                         //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Privilages RAM based list
    public static readonly Dictionary<int, string> Privilagelist = new Dictionary<int, string>() 
            { 
                {-1,"غير معروف"},
                {0,"غير مشمول"},
                {1,"ذوي الشهداء قبل 2003"},
                {2,"السجناء السياسيين"},
                {3,"تعويض المتضررين"},
                {4,"شهداء الحشد الشعبي"},
                {5,"ذوي الاعاقة والاحتياجات الخاصة"} };

    public static readonly string[] HTML_BREAKLINE = new string[] { "<br />" };

    // Exceptions for specific colleges for applying with bypassing admissions instructions (Medical and Engineering groups)
    public static readonly int[] F_QUARTER_CIDS = new int[] { 1, 2, 3, 4, 17, 20, 23 };

    // RAM-loaded list for CheckIqPhone()
    public static readonly List<string> FakePhoneNoIq = new List<string>() { "0000000", "1111111", "2222222", "3333333", "4444444", "5555555", "6666666", "7777777", "8888888", "9999999" };

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Submission apllicantion translation
    public static string GetVerifyFromId(int id)
    {
        if (id == 1) return "مطابق"; else return "غير مطابق";

    }
    public static string getQrtRankFromId(int id)
    {
        if (id == 0) return "ليس من الربع الاول"; else return "ضمن الربع الاول";
    }
    public static string SetEmptyLinkAlt(string str1, string str2)
    {
        if (str1 == "") return str2; else return str1;
    }
    public static string GetCertFromId(int id)
    {
        if (id == 1) return ("البكلوريوس"); else if (id == 2) return ("الدبلوم العالي"); else if (id == 3) return ("الدبلوم العالي المعادل"); else if (id == 4) return ("الماجستير"); else return ("الدكتوراه");
    }
    public static string GetSexFromId(int id)
    {
        if (id == 1) return "ذكر"; else return "انثى";
    }
    public static string GetMStateFromId(int id)
    {
        if (id == 1) return "اعزب"; else if (id == 2) return "متزوج"; else if (id == 3) return "مطلق"; else return "ارمل";
    }
    public static string GetRotateNote(int id, string label)
    {
        if (id == 0) return label; else return @"<span style=""color:red"">مدور ل" + label + "</span>";
    }
    public static string GetChannelFromId(int id)
    {
        if (id == 0) return "القبول العام"; else if (id == 1) return "القبول الخاص"; if (id == 2) return "(خط الصد) - القبول العام"; else return "(خط الصد) - القبول الخاص";
    }
    public static string GetInclusiveFromId(int id)
    {
        if (id == 0) return "غير مشمول"; else return "مشمول";
    }
    public static string GetResultFromId(int id)
    {
        if (id == -1) return "غير محدد"; else if (id == 0) return "غير مقبول"; else return "مقبول";
    }
    public static string GvIconUri(int status, int stype)
    {
        if (stype == 1) { if (status == 2) return "files/check.png"; else return "files/uncheck2.png"; }
        else
        { if (status <= 0) return "files/uncheck.png"; else  return "files/check.png"; }
    }
    public static string GvIconToolTip(int status, int stype)
    {
        if (stype == 1)
        {
            if (status == -1)
                return "حالة الطلب: تم حظر حساب التقديم بسبب سوء الاستخدام";
            else
                if (status == 0)
                    return "حالة الطلب: لم يتم تفعيل الحساب الى الان";
                else
                    if (status == 1)
                        return "حالة التقديم: لم يتم اكمال الطلب وارساله الى الان";
                    else
                        return "حالة التقديم: تم ارسال الطلب بنجاح";
        }
        else
            if (stype == 2)
            {
                if (status == 0)
                    return "حالة المطابقة: لم يتم مطابقة الطلب الى الان";
                else
                    return "حالة المطابقة: تم تفعيل المطابقة بنجاح";
            }
            else
                if (stype == 3)
                {
                    if (status == 0)
                        return "حالة المصادقة: لم يتم مصادقة الطلب الى الان";
                    else
                        return "حالة المصادقة: تم تفعيل المصادقة بنجاح";
                }
                else
                {
                    if (status == 1)
                        return "حالة القبول: المتقدم مقبول للدراسات للعليا";
                    else
                        if (status == 0)
                            return "حالة القبول: المتقدم لم يقبل للدراسات العليا";
                        else
                            return "حالة القبول: لم يتم تحديد نتيجة القبول الى الان";
                }
    }
    #endregion

    #region Stack applicantions
    public static string GetWorkerFromId(int id)
    {
        if (id == 2)
            return "نظام اصدار الايميلات";
        else
            if (id == 3)
                return "نظام اصدار الهويات";
            else
                return "نظام التصاريح الامنية";
    }
    public static string GetWorkerStatusText(int id)
    {
        if (id == 0)
            return "غير معالج";
        else
            return "معالج";
    }
    public static string GetWorkerStatusIcon(int id)
    {
        if (id == 0)
            return "غير معالج";
        else
            return "معالج";
    }
    public static string GetWorkerStatusAlt(int id)
    {
        if (id == 0)
            return "حالة المعالجة: تمت معالجة هذا القيد";
        else
            return "حالة المعالجة: القيد لم يعالج لغاية الان";
    }
    #endregion

    #region Generic
    public static string ToPlainBreakline(string str)
    {
        return str.Replace("<br />", "\n");
    }
    public static string ToHTMLBreakline(string str)
    {
        return str.Replace("\n", "<br />");
    }
    public static int GetCleanInt(string str)
    {
        int val = 0;
        bool check = int.TryParse(str, out val);
        if (check) return val; else return -1;
    }
    public static double GetCleanDouble(string str)
    {
        double val = 0;
        bool check = double.TryParse(str, out val);
        if (check) return val; else return -1;
    }
    public static string RemoveAccents(string text)
    {
        StringBuilder sbReturn = new StringBuilder();
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
        foreach (char letter in arrayText)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);
        }
        return sbReturn.ToString();
    }
    public static string CleanArabicName(string name)
    {
        name = string.Join(" ", name.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x.Trim()));
        name = name.Replace("ة", "ه")
            .Replace("إ", "ا")
            .Replace("أ", "ا")
            .Replace("آ", "ا")
            .Replace("لأ", "لا")
            .Replace("ـ", "")
            .Replace("ة الز", "ةالز")
            .Replace("عبد ال", "عبدال"); return name;
    }
    // Damerau–Levenshtein to measure the distance between two strings
    // Source code and descrption: https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance
    public static int LevenshteinDistance(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            if (string.IsNullOrEmpty(t))
                return 0;
            return t.Length;
        }

        if (string.IsNullOrEmpty(t))
        {
            return s.Length;
        }

        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // initialize the top and right of the table to 0, 1, 2, ...
        for (int i = 0; i <= n; d[i, 0] = i++) ;
        for (int j = 1; j <= m; d[0, j] = j++) ;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                int min1 = d[i - 1, j] + 1;
                int min2 = d[i, j - 1] + 1;
                int min3 = d[i - 1, j - 1] + cost;
                d[i, j] = Math.Min(Math.Min(min1, min2), min3);
            }
        }
        return d[n, m];
    }
    public static bool CheckIqPhone(string no)
    {
        if (no.Length == 11)
        {
            if (no.StartsWith("075") || no.StartsWith("076") || no.StartsWith("077") || no.StartsWith("078") || no.StartsWith("079"))
            { no = no.Substring(4); if (Utilities.FakePhoneNoIq.Contains(no)) return false; else return true; }
            else return false;
        }
        else return false;
    }
    public static string GetNewCode()
    {
        return System.Text.RegularExpressions.Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Remove(22, 2), "[^a-z0-9]", "");
    }
    public static int GetUnixTimestamp()
    {
        return (int)(DateTime.UtcNow.Subtract(epoch)).TotalSeconds;
    }
    public static int GetUnixTimestamp(DateTime dt)
    {
        return Convert.ToInt32((dt.ToUniversalTime() - epochlong).Ticks / TimeSpan.TicksPerSecond);
    }
    public static DateTime GetDateFromUnix(int dt)
    {
        return epochlong.AddSeconds(dt).ToLocalTime();
    }
    public static string GetDateFromUnix(int dt, int dummy)
    {
        // Unix timestamp is seconds past epoch
        return epochlong.AddSeconds(dt).ToLocalTime().ToShortDateString();
    }
    public static int GetYears(DateTime start, DateTime end)
    {
        DateTime zeroTime = new DateTime(1, 1, 1); TimeSpan span = end - start;
        return (zeroTime + span).Year - 1;
    }
    #endregion
}

#region Security
/// <summary>
/// Class to control access to sepcific folders based on HTTP Custom Handler Technology (PDF Control).
/// </summary>
public class PdfRestrictHandler : IHttpHandler, IReadOnlySessionState
{
    public PdfRestrictHandler() { }
    bool IHttpHandler.IsReusable { get { return false; } }
    void IHttpHandler.ProcessRequest(HttpContext context)
    {
        bool cont = false;
        // Customize protection only for specific folders
        if (context.Request.Url.ToString().Contains(Utilities.FILE_SECURE_URI))
        {
            // Check for session and on failure change context
            if (context.Session["pid"] != null || context.Session["uid"] != null)
                cont = true;
        }
        else
            cont = true;

        if (cont)
        {
            // Otherwise release the response
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("Content-Disposition", "attachment");
            context.Response.WriteFile(context.Request.RawUrl);
            context.Response.Flush();
        }
        else
            context.Response.Redirect(Utilities.FILE_SECURE_REDIRECT);
    }
}
/// <summary>
/// Class to control access to sepcific folders based on HTTP Custom Handler Technology (Image Control).
/// </summary>
public class JpgRestrictHandler : IHttpHandler, IReadOnlySessionState
{
    public JpgRestrictHandler() { }
    bool IHttpHandler.IsReusable { get { return false; } }
    void IHttpHandler.ProcessRequest(HttpContext context)
    {
        bool cont = false;
        // Customize protection only for specific folders
        if (context.Request.Url.ToString().Contains(Utilities.FILE_SECURE_URI))
        {
            // Check for session and on failure change context
            if (context.Session["pid"] != null || context.Session["uid"] != null)
                cont = true;
        }
        else
            cont = true;

        if (cont)
        {
            // Otherwise release the response
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.ContentType = "image/jpeg";
            context.Response.AddHeader("Content-Disposition", "attachment");
            context.Response.WriteFile(context.Request.RawUrl);
            context.Response.Flush();
        }
        else
            context.Response.Redirect(Utilities.FILE_SECURE_REDIRECT);
    }
}
/// <summary>
/// Class to control access to sepcific folders based on HTTP Custom Handler Technology (CSV Control).
/// </summary>
public class CsvRestrictHandler : IHttpHandler, IReadOnlySessionState
{
    public CsvRestrictHandler() { }
    bool IHttpHandler.IsReusable { get { return false; } }
    void IHttpHandler.ProcessRequest(HttpContext context)
    {
        if (context.Session["uid"] != null)
        {
            // Otherwise release the response
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment");
            context.Response.WriteFile(context.Request.RawUrl);
            context.Response.Flush();
        }
        else
            context.Response.Redirect(Utilities.FILE_SECURE_REDIRECT);
    }
}
#endregion