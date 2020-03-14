using System;
using System.Linq;
using System.Collections.Generic;
using AngleSharp.Dom;

namespace OptivumParser
{
    public static class LessonParser
    {
        public enum PlanType
        {
            Class, Teacher, Room
        }
        public static List<Lesson> GetLessons(string lessonPlanPath, PlanType planFor, string id)
        {
            var provider = new PlanProvider(lessonPlanPath);
            IDocument document;

            if (planFor == PlanType.Class)
            {
                document = provider.GetClass(id);
            }
            else if (planFor == PlanType.Teacher)
            {
                document = provider.GetTeacher(id);
            }
            else if (planFor == PlanType.Room)
            {
                document = provider.GetRoom(id);
            }
            else
            {
                throw new Exception("Unsupported plan type.");
            }

            var lessonTable = document.All.Where(e => e.ClassName == "tabela").First().Children.First();
            var rows = lessonTable.Children.Where(e => e.TagName.ToLower() == "tr").Where(tr => tr.Children.Where(e => e.Attributes.Any()).Any());

            var allLessons = new List<Lesson>();

            foreach (var row in rows)
            {
                var number = Int32.Parse(row.Children.Where(r => r.ClassName == "nr").Select(r => r.InnerHtml).First());

                var textPeriods = row.Children.Where(r => r.ClassName == "g").Select(r => r.InnerHtml).First().Split('-');
                var periods = (start: TimeSpan.Parse(textPeriods[0]), end: TimeSpan.Parse(textPeriods[1]));

                var lessons = row.Children.Where(r => r.ClassName == "l").ToList();

                foreach (var lesson in lessons)
                {
                    //? I using `QuerySelectorAll` instead of Linq `Where` because sometimes the items are packed into a blank parent `span` element.
                    //? I have no influence on this, it is the Optivum plan generator's result.

                    var dayOfWeek = lessons.IndexOf(lesson) + 1;
                    var names = lesson.QuerySelectorAll(".p").Select(e => e.InnerHtml).ToList();
                    var teachers = lesson.QuerySelectorAll(".n").Select(e => e.Attributes[0].Value).ToList();
                    var rooms = lesson.QuerySelectorAll(".s").Select(e => e.Attributes[0].Value).ToList();

                    foreach (var group in Enumerable.Range(0, names.Count()))
                    {
                        teachers = teachers.Select(s => new String(s.Where(c => Char.IsDigit(c)).ToArray())).ToList();
                        rooms = rooms.Select(s => new String(s.Where(c => Char.IsDigit(c)).ToArray())).ToList();
                    }

                    foreach (var groupNumber in Enumerable.Range(0, names.Count()))
                    {
                        allLessons.Add(new Lesson(number, periods, dayOfWeek, names[groupNumber], id, teachers[groupNumber], rooms[groupNumber]));
                    }
                }
            }
            return allLessons;
        }
    }
}