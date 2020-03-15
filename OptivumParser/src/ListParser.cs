using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using AngleSharp;

namespace OptivumParser
{
    public static class ListParser
    {
        public static Dictionary<string, string> GetClassIds(PlanProvider provider)
        {
            var document = provider.GetList();

            var classSelect = document.All.Where(e => e.TagName.ToLower() == "select").Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "oddzialy").Any()).First();
            return classSelect.Children.Where(o => o.Attributes.Any()).ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value);
        }

        public static Dictionary<string, string> GetTeacherIds(PlanProvider provider)
        {
            var document = provider.GetList();

            var teacherSelect = document.All.Where(e => e.TagName.ToLower() == "select").Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "nauczyciele").Any()).First();
            return teacherSelect.Children.Where(o => o.Attributes.Any()).ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value);
        }

        public static Dictionary<string, string> GetRoomIds(PlanProvider provider)
        {
            var document = provider.GetList();

            var roomSelect = document.All.Where(e => e.TagName.ToLower() == "select").Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "sale").Any()).First();
            return roomSelect.Children.Where(o => o.Attributes.Any()).ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value);
        }

        public static (Dictionary<string, string> classes, Dictionary<string, string> teachers, Dictionary<string, string> rooms) GetAllIds(PlanProvider provider)
        {
            var document = provider.GetList();

            var classSelect = document.All.Where(e => e.TagName.ToLower() == "select").Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "oddzialy").Any()).First();
            var teacherSelect = document.All.Where(e => e.TagName.ToLower() == "select").Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "nauczyciele").Any()).First();
            var roomSelect = document.All.Where(e => e.TagName.ToLower() == "select").Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "sale").Any()).First();

            return (
                classes: classSelect.Children.Where(o => o.Attributes.Any()).ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value),
                teachers: teacherSelect.Children.Where(o => o.Attributes.Any()).ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value),
                rooms: roomSelect.Children.Where(o => o.Attributes.Any()).ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value)
            );
        }
    }
}