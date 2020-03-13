using System;
using System.Net;
using System.Linq;
using AngleSharp;

namespace OptivumParser
{
    public class Parser
    {
        public (bool value, string message) IsValidPlan(string lessonPlanPath)
        {
            var lessonPlanUri = new Uri(lessonPlanPath);
            if (lessonPlanUri.Scheme == Uri.UriSchemeHttp || lessonPlanUri.Scheme == Uri.UriSchemeHttps)
            {
                var file = new WebClient().DownloadString(lessonPlanPath);
                var document = BrowsingContext.New().OpenAsync(r => r.Content(file)).Result;

                var frames = document.All.Where(e => e.TagName.ToLower() == "frame");
                if (frames.Count() == 2)
                {
                    if ((frames.Where(f => f.Attributes.Where(a => a.Name == "name" && a.Value == "plan").Count() == 1).Count() == 1)
                    && (frames.Where(f => f.Attributes.Where(a => a.Name == "name" && a.Value == "list").Count() == 1).Count() == 1))
                    {
                        return (true, "Supported and valid lesson plan page.");
                    }
                    else
                    {
                        return (false, "Unsupported or invalid lesson plan page. Make sure you provide the root address of the plan.");
                    }
                }
                else
                {

                    return (false, "Unsupported or invalid lesson plan page. Make sure you provide the root address of the plan.");
                }
            }
            else
            {
                return (false, "Unsupported URI scheme. Make sure you provide URI with scheme HTTP or HTTPS.");
            }
        }
    }
}