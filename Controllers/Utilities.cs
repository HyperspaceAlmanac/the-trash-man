using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Controllers
{
    public static class Utilities
    {
        public static string DayNumToWord(DayOfWeek val)
        {
            switch (val)
            {
                case DayOfWeek.Monday:
                    return "Monday";
                case DayOfWeek.Tuesday:
                    return "Tuesday";
                case DayOfWeek.Wednesday:
                    return "Wednesday";
                case DayOfWeek.Thursday:
                    return "Thursday";
                case DayOfWeek.Friday:
                    return "Friday";
                case DayOfWeek.Saturday:
                    return "Saturday";
                case DayOfWeek.Sunday:
                    return "Sunday";
                default:
                    return "Monday";
            }
        }
        public static int DayofWeekOffset(DayOfWeek val)
        {
            switch (val)
            {
                case DayOfWeek.Monday:
                    return 0;
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
                default:
                    return 0;
            }
        }
        public static string DayNumToWord(int val)
        {
            switch (val)
            {
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                case 7:
                    return "Sunday";
                default:
                    return "Monday";
            }
        }
        public static string MonthString(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "January";
            }
        }
        // left > right: 1, left < right: - 1, left = right: 0
        public static int CompareDays(DateTime left, DateTime right)
        {
            if (left.Year > right.Year)
            {
                return 1;
            }
            else if (left.Year < right.Year)
            {
                return -1;
            }
            if (left.Month > right.Month)
            {
                return 1;
            }
            else if (left.Month < right.Month)
            {
                return -1;
            }
            if (left.Day > right.Day)
            {
                return 1;
            }
            else if (left.Day < right.Day)
            {
                return -1;
            }
            return 0;
        }
        public static string DateToString(DateTime dateValue)
        {
            int year = dateValue.Date.Year;
            string month = (dateValue.Month > 9 ? "" : "0") + dateValue.Month.ToString();
            string day = (dateValue.Day > 9 ? "" : "0") + dateValue.Day.ToString();
            string date = $"{year}-{month}-{day}";
            return date;
        }
        public static SelectList GenerateDaysSelectList(int day)
        {
            if (day == 0)
            {
                day = 1;
            }
            List<SelectListItem> days = new List<SelectListItem>();
            days.Add(new SelectListItem() { Text = "Monday", Value = "1", Selected = false });
            days.Add(new SelectListItem() { Text = "Tuesday", Value = "2", Selected = false });
            days.Add(new SelectListItem() { Text = "Wednesday", Value = "3", Selected = false });
            days.Add(new SelectListItem() { Text = "Thursday", Value = "4", Selected = false });
            days.Add(new SelectListItem() { Text = "Friday", Value = "5", Selected = false });
            days.Add(new SelectListItem() { Text = "Saturday", Value = "6", Selected = false });
            days.Add(new SelectListItem() { Text = "Sunday", Value = "7", Selected = false });
            days[day - 1].Selected = true;
            return new SelectList(days, "Value", "Text");
        }
    }
}
