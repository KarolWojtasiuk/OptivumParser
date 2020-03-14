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

                var textPeriod = row.Children.Where(r => r.ClassName == "g").Select(r => r.InnerHtml).First().Split('-');
                var period = (start: TimeSpan.Parse(textPeriod[0]), end: TimeSpan.Parse(textPeriod[1]));

                var lessons = row.Children.Where(r => r.ClassName == "l").ToList();

                foreach (var lesson in lessons)
                {
                    //? I using `QuerySelectorAll` instead of Linq `Where` because sometimes the items are packed into a blank parent `span` element.
                    //? I have no influence on this, it is the Optivum plan generator's result.

                    var dayOfWeek = lessons.IndexOf(lesson) + 1;
                    var groups = lesson.QuerySelectorAll("*").ToList();
                    groups.Add(lesson); //? Sometimes the lessons doesn't have a separate div.
                    groups = groups.Where(e => e.Children.Where(c => c.ClassName == "p").Any()).ToList();

                    foreach (var group in groups)
                    {
                        var names = group.Children.Where(e => e.ClassName == "p").Select(e => e.InnerHtml);
                        var teachers = group.Children.Where(e => e.ClassName == "n").Select(e => e.Attributes[0].Value);
                        var rooms = group.Children.Where(e => e.ClassName == "s").Select(e => e.Attributes[0].Value);

                        //? Protection against lessons without property. In my school, lessons without setted teacher are popular.

                        string name = null;
                        string teacher = null;
                        string room = null;

                        if (names.Any())
                        {
                            name = names.First();
                        }

                        if (teachers.Any())
                        {
                            teacher = new String(teachers.First().Where(c => Char.IsDigit(c)).ToArray());
                        }

                        if (rooms.Any())
                        {

                            room = new String(rooms.First().Where(c => Char.IsDigit(c)).ToArray());
                        }

                        allLessons.Add(new Lesson(number, period, dayOfWeek, name, id, teacher, room));
                    }
                }
            }
            return allLessons;
        }
    }
}