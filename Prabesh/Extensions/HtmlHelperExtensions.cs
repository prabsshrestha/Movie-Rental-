using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Prabesh.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString DeploymentVersion(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(DeploymentVersion());
        }

        public static string DeploymentVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileInfo fileInfo = new FileInfo(assembly.Location);
            string lastModified = fileInfo.LastWriteTime.ToString("yyyyMMddHHmmss");
            return lastModified;
            //try
            //{
            //    return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            //}
            //catch (Exception ex)
            //{
            //    return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //}
        }
        public static string ConvertHtmlToPlainText(string html)
        {
            string noHtmlTags = Regex.Replace(!string.IsNullOrWhiteSpace(html) ? html : "", "<.*?>", "");
            string decodedText = System.Net.WebUtility.HtmlDecode(noHtmlTags);
            return Regex.Replace(decodedText, @"\s+", " ").Trim();
        }
    }
}