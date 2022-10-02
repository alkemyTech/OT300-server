using System;
using System.IO;

namespace OngProject.Core.Helper
{
    public class EmailHelper
    {
        public static string ConvertTemplateToString(string title, string newContent)
        {
            string templateFile = Path.Combine(Directory.GetCurrentDirectory(), @"Templates\email_template.html");
            
            using (StreamReader sr = File.OpenText(templateFile))
            {
                string reader = sr.ReadToEnd();
                reader = reader.Replace("T&iacute;tulo", title);
                reader = reader.Replace("Texto del email", newContent);

                return reader;
            }
        }
    }
}
