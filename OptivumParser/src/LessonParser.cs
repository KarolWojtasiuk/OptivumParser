using System;
using System.Linq;
using System.Collections.Generic;

namespace OptivumParser
{
    public static class LessonParser
    {
        public enum PlanType
        {
            Class, Teacher, Room
        }
        public static List<Lesson> GetLessonsForClass(PlanProvider provider, string classId)
        {
            var document = provider.GetClass(classId);

            var lessonTable = document.All.Where(e => e.ClassName == "tabela").First().Children.First();
            var rows = lessonTable.Children.Where(e => e.TagName.ToLower() == "tr").Where(tr => tr.Children.Where(e => e.Attributes.Any()).Any());

            var allLessons = new List<Lesson>();

            foreach (var row in rows)
            {
                var number = Int32.Parse(row.Children.Where(r => r.ClassName == "nr").Select(r => r.InnerHtml).First());

                var textPeriod = row.Children.Where(r => r.ClassName == "g").Select(r => r.InnerHtml).First().Split('-');
                var period = new Period() { Start = TimeSpan.Parse(textPeriod[0]).ToString(), End = TimeSpan.Parse(textPeriod[1]).ToString() };

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
                        string teacherId = null;
                        string roomId = null;

                        if (names.Any())
                        {
                            name = names.First();
                        }

                        if (teachers.Any())
                        {
                            teacherId = new String(teachers.First().Where(c => Char.IsDigit(c)).ToArray());
                        }

                        if (rooms.Any())
                        {

                            roomId = new String(rooms.First().Where(c => Char.IsDigit(c)).ToArray());
                        }

                        allLessons.Add(new Lesson()
                        {
                            Number = number,
                            Period = period,
                            DayOfWeek = dayOfWeek,
                            Name = name,
                            ClassId = classId,
                            TeacherId = teacherId,
                            RoomId = roomId
                        });
                    }
                }
            }
            return allLessons;
        }

        public static List<Lesson> GetLessonsForTeacher(PlanProvider provider, string teacherId)
        {
            var document = provider.GetTeacher(teacherId);

            var lessonTable = document.All.Where(e => e.ClassName == "tabela").First().Children.First();
            var rows = lessonTable.Children.Where(e => e.TagName.ToLower() == "tr").Where(tr => tr.Children.Where(e => e.Attributes.Any()).Any());

            var allLessons = new List<Lesson>();

            foreach (var row in rows)
            {
                var number = Int32.Parse(row.Children.Where(r => r.ClassName == "nr").Select(r => r.InnerHtml).First());

                var textPeriod = row.Children.Where(r => r.ClassName == "g").Select(r => r.InnerHtml).First().Split('-');
                var period = new Period() { Start = TimeSpan.Parse(textPeriod[0]).ToString(), End = TimeSpan.Parse(textPeriod[1]).ToString() };

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
                        var classes = group.Children.Where(e => e.ClassName == "o").Select(e => e.Attributes[0].Value);
                        var rooms = group.Children.Where(e => e.ClassName == "s").Select(e => e.Attributes[0].Value);

                        //? Protection against lessons without property. In my school, lessons without setted teacher are popular.

                        string name = null;
                        string classId = null;
                        string roomId = null;

                        if (names.Any())
                        {
                            name = names.First();
                        }

                        if (classes.Any())
                        {
                            classId = new String(classes.First().Where(c => Char.IsDigit(c)).ToArray());
                        }

                        if (rooms.Any())
                        {

                            roomId = new String(rooms.First().Where(c => Char.IsDigit(c)).ToArray());
                        }

                        allLessons.Add(new Lesson()
                        {
                            Number = number,
                            Period = period,
                            DayOfWeek = dayOfWeek,
                            Name = name,
                            ClassId = classId,
                            TeacherId = teacherId,
                            RoomId = roomId
                        });
                    }
                }
            }
            return allLessons;
        }

        public static List<Lesson> GetLessonsForRoom(PlanProvider provider, string roomId)
        {
            var document = provider.GetRoom(roomId);

            var lessonTable = document.All.Where(e => e.ClassName == "tabela").First().Children.First();
            var rows = lessonTable.Children.Where(e => e.TagName.ToLower() == "tr").Where(tr => tr.Children.Where(e => e.Attributes.Any()).Any());

            var allLessons = new List<Lesson>();

            foreach (var row in rows)
            {
                var number = Int32.Parse(row.Children.Where(r => r.ClassName == "nr").Select(r => r.InnerHtml).First());

                var textPeriod = row.Children.Where(r => r.ClassName == "g").Select(r => r.InnerHtml).First().Split('-');
                var period = new Period() { Start = TimeSpan.Parse(textPeriod[0]).ToString(), End = TimeSpan.Parse(textPeriod[1]).ToString() };

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
                        var classes = group.Children.Where(e => e.ClassName == "o").Select(e => e.Attributes[0].Value);
                        var teachers = group.Children.Where(e => e.ClassName == "n").Select(e => e.Attributes[0].Value);

                        //? Protection against lessons without property. In my school, lessons without setted teacher are popular.

                        string name = null;
                        string classId = null;
                        string teacherId = null;

                        if (names.Any())
                        {
                            name = names.First();
                        }

                        if (classes.Any())
                        {
                            classId = new String(classes.First().Where(c => Char.IsDigit(c)).ToArray());
                        }

                        if (teachers.Any())
                        {

                            teacherId = new String(teachers.First().Where(c => Char.IsDigit(c)).ToArray());
                        }

                        allLessons.Add(new Lesson()
                        {
                            Number = number,
                            Period = period,
                            DayOfWeek = dayOfWeek,
                            Name = name,
                            ClassId = classId,
                            TeacherId = teacherId,
                            RoomId = roomId
                        });
                    }
                }
            }
            return allLessons;
        }
    }
}