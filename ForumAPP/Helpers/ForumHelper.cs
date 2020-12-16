using System;

namespace ForumAPP.Helpers
{
    public class ForumHelper : IForumHelper
    {
        public string GetRelativeDate(DateTime date)
        {
            var now = DateTime.Now;
            var timeSpan = (now - date);
            if (timeSpan.Days > 1)
                return date.ToString("yyyy-MM-dd HH:mm");
            else if (timeSpan.Minutes > 59)
                return $"{timeSpan.TotalHours} godzin temu";
            else if (timeSpan.Minutes < 60)
                return $"{timeSpan.Minutes} minut/y temu";
            
            return date.ToString("yyyy-MM-dd HH:mm");
        }
    }
}