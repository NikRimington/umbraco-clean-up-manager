using System.Linq;
using System.Web.Hosting;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RB.Umbraco.CleanUpManager.Helpers
{
    /// <summary>
    /// Class DashboardHelper.
    /// </summary>
    public static class DashboardHelper
    {
        /// <summary>
        /// Ensures the tab.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="path">The path.</param>
        internal static void EnsureTab(string section, string caption, string path)
        {
            var configPath = HostingEnvironment.MapPath("~/config/dashboard.config");
            if (configPath == null)
                return;

            var configXml = XDocument.Load(configPath);

            // Loop through each tab to see if the Domain
            // Manager tab exists in any section

            var tabs = configXml.XPathSelectElements("//section/tab").ToList();
            if (tabs.Any())
            {
                if (tabs.Select(tab => tab.XPathSelectElement("control")).Where(control => control != null)
                                                                         .Any(control => control.Value.Equals(path)))
                { return; }
            }

            // If we have got this far, we know that the Domain
            // Mananger tab does not exist in any section.
            // So let's add it

            var sectionXml = configXml.XPathSelectElement(string.Format("//section [@alias='{0}']", section));
            if (sectionXml == null)
                return;

            var tabXml = sectionXml.XPathSelectElement(string.Format("//tab [@caption='{0}']", caption));
            if (tabXml != null)
                return;

            var tabToAdd = XElement.Parse(string.Format("<tab caption=\"{0}\"><control>{1}</control></tab>", caption, path));

            sectionXml.Add(tabToAdd);
            configXml.Save(configPath);
        }

        /// <summary>
        /// Removes the tab.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="caption">The caption.</param>
        internal static void RemoveTab(string section, string caption)
        {
            var configPath = HostingEnvironment.MapPath("~/config/dashboard.config");
            if (configPath == null)
                return;

            var configXml = XDocument.Load(configPath);

            var sectionXml = configXml.XPathSelectElement(string.Format("//section [@alias='{0}']", section));
            if (sectionXml == null)
                return;

            var tabXml = sectionXml.XPathSelectElement(string.Format("//tab [@caption='{0}']", caption));
            if (tabXml == null)
                return;

            tabXml.Remove();
            configXml.Save(configPath);
        }
    }
}