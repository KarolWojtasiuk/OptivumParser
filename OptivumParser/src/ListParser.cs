using System;
using System.Linq;
using System.Collections.Generic;

namespace OptivumParser
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message)
        {
        }
    }

    public static class ListParser
    {
        public static Dictionary<string, string> GetClasses(PlanProvider provider)
        {
            var document = provider.GetList();

            var classSelect = document.All.Where(e => e.TagName.ToLower() == "select").First(s =>
                s.Attributes.Where(a => a.Name == "name" && a.Value == "oddzialy").Any());
            return classSelect.Children.Where(o => o.Attributes.Any())
                .ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value);
        }

        public static Dictionary<string, string> GetTeachers(PlanProvider provider)
        {
            var document = provider.GetList();

            var teacherSelect = document.All.Where(e => e.TagName.ToLower() == "select").First(s =>
                s.Attributes.Where(a => a.Name == "name" && a.Value == "nauczyciele").Any());
            return teacherSelect.Children.Where(o => o.Attributes.Any())
                .ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value);
        }

        public static Dictionary<string, string> GetRooms(PlanProvider provider)
        {
            var document = provider.GetList();

            var roomSelect = document.All.Where(e => e.TagName.ToLower() == "select")
                .Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "sale").Any()).First();
            return roomSelect.Children.Where(o => o.Attributes.Any())
                .ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value);
        }

        public static string GetClass(PlanProvider provider, string className)
        {
            var classes = GetClasses(provider);
            if (classes.ContainsKey(className))
            {
                return classes[className];
            }
            else
            {
                throw new InvalidNameException("Class with this name doesn't exist.");
            }
        }

        public static string GetTeacher(PlanProvider provider, string teacherName)
        {
            var classes = GetTeachers(provider);
            if (classes.ContainsKey(teacherName))
            {
                return classes[teacherName];
            }
            else
            {
                throw new InvalidNameException("Teacher with this name doesn't exist.");
            }
        }

        public static string GetRoom(PlanProvider provider, string roomName)
        {
            var classes = GetRooms(provider);
            if (classes.ContainsKey(roomName))
            {
                return classes[roomName];
            }
            else
            {
                throw new InvalidNameException("Room with this name doesn't exist.");
            }
        }

        public static Dictionary<string, Dictionary<string, string>> GetAll(PlanProvider provider)
        {
            var document = provider.GetList();

            var classSelect = document.All.Where(e => e.TagName.ToLower() == "select")
                .Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "oddzialy").Any()).First();
            var teacherSelect = document.All.Where(e => e.TagName.ToLower() == "select").Where(s =>
                s.Attributes.Where(a => a.Name == "name" && a.Value == "nauczyciele").Any()).First();
            var roomSelect = document.All.Where(e => e.TagName.ToLower() == "select")
                .Where(s => s.Attributes.Where(a => a.Name == "name" && a.Value == "sale").Any()).First();

            return new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "classes",
                    classSelect.Children.Where(o => o.Attributes.Any())
                        .ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value)
                },
                {
                    "teachers",
                    teacherSelect.Children.Where(o => o.Attributes.Any())
                        .ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value)
                },
                {
                    "rooms",
                    roomSelect.Children.Where(o => o.Attributes.Any())
                        .ToDictionary(o => o.InnerHtml, o => o.Attributes.First().Value)
                }
            };
        }
    }
}