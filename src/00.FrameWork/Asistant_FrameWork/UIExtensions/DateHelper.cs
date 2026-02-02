using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistant_FrameWork.UIExtensions
{
    public static class DateHelper
    {
    private static readonly PersianCalendar _persianCalendar = new();
       
        public static DateTime ConvertToGregorian(this string persianDate)
        {
            var input = NormalizePersianNumbers(persianDate);
            var pc = new PersianCalendar(); var parts = input.Split('/');
            int year = int.Parse(parts[0]); int month = int.Parse(parts[1]); 
            int day = int.Parse(parts[2]); return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
        public static DateTime ToGregorianDate(this string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate))
                throw new ArgumentNullException(nameof(persianDate));

            // حذف زمان اگر وجود دارد
            var dateOnly = persianDate.Split(' ')[0];
            dateOnly = NormalizePersianNumbers(dateOnly);

            var parts = dateOnly.Split('/', '-');
            if (parts.Length != 3)
                throw new FormatException($"فرمت تاریخ '{persianDate}' صحیح نیست");

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            return _persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        // تاریخ + زمان
        public static DateTime ToGregorianDateTime(this string persianDate, string time = "00:00:00")
        {
            if (string.IsNullOrWhiteSpace(persianDate))
                throw new ArgumentNullException(nameof(persianDate));

            // جدا کردن تاریخ
            var dateOnly = persianDate.Split(' ')[0];
            dateOnly = NormalizePersianNumbers(dateOnly);

            var dateParts = dateOnly.Split('/', '-');
            if (dateParts.Length != 3)
                throw new FormatException($"فرمت تاریخ '{persianDate}' صحیح نیست");

            int year = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int day = int.Parse(dateParts[2]);

            // تجزیه زمان
            time = NormalizePersianNumbers(time);
            var timeParts = time.Split(':');
            int hour = timeParts.Length > 0 ? int.Parse(timeParts[0]) : 0;
            int minute = timeParts.Length > 1 ? int.Parse(timeParts[1]) : 0;
            int second = timeParts.Length > 2 ? int.Parse(timeParts[2]) : 0;

            return _persianCalendar.ToDateTime(year, month, day, hour, minute, second, 0);
        }
        // تبدیل رشته شمسی به DateTime میلادی
        public static DateTime ToGregorianDateTime(this string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate))
                throw new ArgumentNullException(nameof(persianDate));

            var parts = persianDate.GetPersianDateParts();

            return _persianCalendar.ToDateTime(
                parts.Year, parts.Month, parts.Day,
                parts.Hour, parts.Minute, parts.Second, 0);
        }

        // تبدیل DateTime به رشته شمسی
        public static string ToPersianDateString(this DateTime date, string format = "yyyy/MM/dd")
        {
            var year = _persianCalendar.GetYear(date);
            var month = _persianCalendar.GetMonth(date);
            var day = _persianCalendar.GetDayOfMonth(date);
            var hour = _persianCalendar.GetHour(date);
            var minute = _persianCalendar.GetMinute(date);
            var second = _persianCalendar.GetSecond(date);

            return format
                .Replace("yyyy", year.ToString("0000"))
                .Replace("MM", month.ToString("00"))
                .Replace("dd", day.ToString("00"))
                .Replace("HH", hour.ToString("00"))
                .Replace("mm", minute.ToString("00"))
                .Replace("ss", second.ToString("00"))
                .Replace("M", month.ToString())
                .Replace("d", day.ToString());
        }
        public static string ToPersianDateString2(this DateTime date, string format = "yyyy/MM/dd")
        {
            var persianCalendar = new PersianCalendar();

            // تاریخ میلادی را مستقیماً به شمسی تبدیل نمی‌توان کرد
            // PersianCalendar روی DateTime میلادی عمل می‌کند
            var year = persianCalendar.GetYear(date);
            var month = persianCalendar.GetMonth(date);
            var day = persianCalendar.GetDayOfMonth(date);

            // ساعت‌ها از DateTime اصلی گرفته می‌شوند (چون تقویم شمسی روی ساعت تاثیری ندارد)
            var hour = date.Hour;
            var minute = date.Minute;
            var second = date.Second;

            return format
                .Replace("yyyy", year.ToString("0000"))
                .Replace("MM", month.ToString("00"))
                .Replace("dd", day.ToString("00"))
                .Replace("HH", hour.ToString("00"))
                .Replace("mm", minute.ToString("00"))
                .Replace("ss", second.ToString("00"))
                .Replace("yy", year.ToString().Substring(Math.Max(0, year.ToString().Length - 2)))
                .Replace("M", month.ToString())
                .Replace("d", day.ToString());
        }
        // اعتبارسنجی تاریخ شمسی
        public static bool IsValidPersianDate(this string persianDate)
        {
            try
            {
                var parts = persianDate.GetPersianDateParts();

                // بررسی محدوده‌های منطقی
                if (parts.Year < 1300 || parts.Year > 1500)
                    return false;
                if (parts.Month < 1 || parts.Month > 12)
                    return false;
                if (parts.Day < 1 || parts.Day > 31)
                    return false;

                // بررسی تعداد روزهای ماه
                int daysInMonth = GetDaysInPersianMonth(parts.Year, parts.Month);
                return parts.Day <= daysInMonth;
            }
            catch
            {
                return false;
            }
        }

        // گرفتن تعداد روزهای ماه شمسی
        private static int GetDaysInPersianMonth(int year, int month)
        {
            if (month <= 6) return 31;
            if (month <= 11) return 30;

            // ماه 12 (اسفند) - بررسی سال کبیسه
            return IsPersianLeapYear(year) ? 30 : 29;
        }

        // بررسی سال کبیسه شمسی
        private static bool IsPersianLeapYear(int year)
        {
            // الگوریتم کبیسه‌گیری گاهشماری هجری خورشیدی
            int[] leapYears = new int[] { 1, 5, 9, 13, 17, 22, 26, 30 };
            int remainder = year % 33;
            return leapYears.Contains(remainder);
        }

        // تجزیه رشته تاریخ
        private static (int Year, int Month, int Day, int Hour, int Minute, int Second)
            GetPersianDateParts(this string persianDate)
        {
            // جدا کردن تاریخ و زمان
            var dateTimeParts = persianDate.Split(' ');
            var datePart = dateTimeParts[0];
            var timePart = dateTimeParts.Length > 1 ? dateTimeParts[1] : "00:00:00";

            // تبدیل اعداد
            datePart = NormalizePersianNumbers(datePart);
            timePart = NormalizePersianNumbers(timePart);

            // تجزیه تاریخ
            var dateParts = datePart.Split('/', '-');
            if (dateParts.Length != 3)
                throw new FormatException("فرمت تاریخ صحیح نیست");

            // تجزیه زمان
            var timeParts = timePart.Split(':');

            return (
                Year: int.Parse(dateParts[0]),
                Month: int.Parse(dateParts[1]),
                Day: int.Parse(dateParts[2]),
                Hour: timeParts.Length > 0 ? int.Parse(timeParts[0]) : 0,
                Minute: timeParts.Length > 1 ? int.Parse(timeParts[1]) : 0,
                Second: timeParts.Length > 2 ? int.Parse(timeParts[2]) : 0
            );
        }

        // تبدیل اعداد فارسی/عربی به انگلیسی
        private static string NormalizePersianNumbers(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var result = new StringBuilder(input);

            var persianToEnglish = new Dictionary<char, char>
        {
            {'۰', '0'}, {'۱', '1'}, {'۲', '2'}, {'۳', '3'}, {'۴', '4'},
            {'۵', '5'}, {'۶', '6'}, {'۷', '7'}, {'۸', '8'}, {'۹', '9'},
            {'٠', '0'}, {'١', '1'}, {'٢', '2'}, {'٣', '3'}, {'٤', '4'},
            {'٥', '5'}, {'٦', '6'}, {'٧', '7'}, {'٨', '8'}, {'٩', '9'}
        };

            for (int i = 0; i < result.Length; i++)
            {
                if (persianToEnglish.ContainsKey(result[i]))
                {
                    result[i] = persianToEnglish[result[i]];
                }
            }

            return result.ToString();
        }
    }
}

