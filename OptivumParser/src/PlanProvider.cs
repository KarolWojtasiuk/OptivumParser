using System;
using System.Net;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom;

namespace OptivumParser
{
    public class PlanProvider
    {
        public Uri PlanUri { get; }

        public PlanProvider(string url)
        {
            PlanUri = new Uri(url);
            VerifyLessonPlan();
        }

        public void VerifyLessonPlan()
        {
            if (PlanUri.Scheme == Uri.UriSchemeHttp || PlanUri.Scheme == Uri.UriSchemeHttps)
            {
                var file = new WebClient().DownloadString(PlanUri);
                var document = BrowsingContext.New().OpenAsync(r => r.Content(file)).Result;

                var frames = document.All.Where(e => e.TagName.ToLower() == "frame");
                if (frames.Count() == 2)
                {
                    if ((frames.Where(f => f.Attributes.Where(a => a.Name == "name" && a.Value == "plan").Any()).Count() != 1)
                    && (frames.Where(f => f.Attributes.Where(a => a.Name == "name" && a.Value == "list").Any()).Count() != 1))
                    {
                        throw new Exception("Unsupported or invalid lesson plan page. Make sure you provide the root address of the plan.");
                    }
                }
                else
                {

                    throw new Exception("Unsupported or invalid lesson plan page. Make sure you provide the root address of the plan.");
                }
            }
            else
            {
                throw new Exception("Unsupported URI scheme. Make sure you provide URI with scheme HTTP or HTTPS.");
            }
        }

        public IDocument GetList()
        {
            var file = new WebClient().DownloadString(new Uri(PlanUri, "lista.html"));
            return BrowsingContext.New().OpenAsync(r => r.Content(file)).Result;
        }
    }
}