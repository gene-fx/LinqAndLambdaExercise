using System.Net.Mail;
using System.Globalization;


namespace LinqAndLambdaExercise.Services
{
    class TextTreatment
    {
        public string NameTreatment(string name)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            name = textInfo.ToTitleCase(name);

            return name;
        }

        public string FileExtensionAdder(string fileName)
        {
            if (fileName.Contains('.'))
            {
                fileName = fileName.Substring(0, fileName.IndexOf('.'));
                fileName += ".txt";
            }
            else
                fileName += ".txt";

            return fileName;
        }

        public bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith('.'))
            {
                return false;
            }

            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }

    }
}
