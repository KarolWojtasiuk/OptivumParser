using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
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
                    if ((frames.Where(f => f.Attributes.Where(a => a.Name == "name" && a.Value == "plan").Any()).Count() == 1)
                    && (frames.Where(f => f.Attributes.Where(a => a.Name == "name" && a.Value == "list").Any()).Count() == 1))
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

        private void ValidatePlan(string lessonPlanPath)
        {
            var validationResult = IsValidPlan(lessonPlanPath);
            if (!validationResult.value)
            {
                throw new Exception(validationResult.message);
            }
        }

        public Dictionary<string, string> GetClassIds(string lessonPlanPath)
        {
            ValidatePlan(lessonPlanPath);

            var listUri = new Uri(new Uri(lessonPlanPath), "lista.html");
            var file = new WebClient().DownloadString(listUri);
            var document = BrowsingContext.New().OpenAsync(r => r.Content(file)).Result;

            var classSelect = document.All.Where(e => e.TagName.ToLower() == "select").Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "oddzialy").Any()).First();
            var classes = new Dictionary<string, string>();

            return classSelect.Children.Where(o => o.Attributes.Any()).ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value);
        }
    }
}