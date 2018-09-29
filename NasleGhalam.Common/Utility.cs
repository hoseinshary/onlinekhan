using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NasleGhalam.Common
{
    public static class Utility
    {
        #region ### DateTime ###

        public static string ToPersianDate(this DateTime mDateTime)
        {
            PersianCalendar pc = new PersianCalendar();

            DateTime mDate = DateTime.Parse(mDateTime.ToString());
            int day = pc.GetDayOfMonth(mDate);
            int month = pc.GetMonth(mDate);
            int year = pc.GetYear(mDate);

            return String.Format("{0:0000}/{1:00}/{2:00}", year, month, day);
        }

        public static string ToPersianDateTime(this DateTime mDateTime)
        {
            PersianCalendar pc = new PersianCalendar();

            DateTime mDate = DateTime.Parse(mDateTime.ToString());
            int day = pc.GetDayOfMonth(mDate);
            int month = pc.GetMonth(mDate);
            int year = pc.GetYear(mDate);

            return String.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}", year, month, day, mDate.Hour, mDate.Minute, mDate.Second);
        }

        public static DateTime? ToMiladiDateTime(this string pDateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime thisDate = DateTime.Now;

            string[] arr_dateTime = pDateTime.Split(new Char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (arr_dateTime.Length == 2) // date and time
            {
                try
                {
                    string[] arr_date = arr_dateTime[0].Split('/', '\\', '-');
                    string[] arr_time = arr_dateTime[1].Split(':');

                    int pDay;
                    int pYear;
                    if (arr_date[2].Length == 4)
                    {
                        pDay = Convert.ToInt16(arr_date[0]);
                        pYear = Convert.ToInt16(arr_date[2]);
                    }
                    else
                    {
                        pDay = Convert.ToInt16(arr_date[2]);
                        pYear = Convert.ToInt16(arr_date[0]);
                    }
                    int pMonth = Convert.ToInt16(arr_date[1]);

                    thisDate = pc.ToDateTime(pYear, pMonth, pDay,
                        Convert.ToInt32(arr_time[0]), Convert.ToInt32(arr_time[1]), Convert.ToInt32(arr_time[2]), 0);

                }
                catch { return null; }
            }
            else if (arr_dateTime.Length == 1) // only date
            {
                try
                {
                    string[] arr_date = arr_dateTime[0].Split('/', '\\', '-');

                    int pDay;
                    int pYear;
                    if (arr_date[2].Length == 4)
                    {
                        pDay = Convert.ToInt16(arr_date[0]);
                        pYear = Convert.ToInt16(arr_date[2]);
                    }
                    else
                    {
                        pDay = Convert.ToInt16(arr_date[2]);
                        pYear = Convert.ToInt16(arr_date[0]);
                    }
                    int pMonth = Convert.ToInt16(arr_date[1]);

                    thisDate = pc.ToDateTime(pYear, pMonth, pDay, 0, 0, 0, 0);

                }
                catch { return null; }
            }
            return thisDate;
        }
        #endregion


        #region ### Access ###
        public static String SumBinary(String a, String b)
        {
            int a_len = a.Length;
            int b_len = b.Length;

            int max = Math.Max(a_len, b_len);

            StringBuilder sum = new StringBuilder("");
            int carry = 0;

            for (int i = 0; i < max; i++)
            {
                int m = GetBit(a, a_len - i - 1);
                int n = GetBit(b, b_len - i - 1);
                int add = m + n + carry;
                sum.Insert(0, add % 2);
                carry = add / 2;
            }

            if (carry == 1)
                sum.Insert(0, "1");

            return sum.ToString();
        }


        /// <summary>
        /// Find index of action bit from sumOfAction and replace it to 1
        /// </summary>
        /// <param name="sumOfAction"></param>
        /// <param name="actionBit"></param>
        /// <returns></returns>
        public static String AddAccess(String sumOfAction, int actionBit)
        {
            StringBuilder strSumAction = new StringBuilder(sumOfAction);
            while (strSumAction.Length <= actionBit)
            {
                strSumAction.Insert(0, "0");
            }

            // پیدا کردن اندکس بیت و جاگذاری آن با یک
            strSumAction.Replace('0', '1', strSumAction.Length - actionBit - 1, 1);

            return strSumAction.ToString();
        }

        /// <summary>
        /// Find index of action bit from sumOfAction and replace it to 0
        /// </summary>
        /// <param name="sumOfAction"></param>
        /// <param name="actionBit"></param>
        /// <returns></returns>
        public static String RemoveAccess(String sumOfAction, int actionBit)
        {
            StringBuilder strSumAction = new StringBuilder(sumOfAction);
            while (strSumAction.Length <= actionBit)
            {
                strSumAction.Insert(0, "0");
            }
            strSumAction.Replace('1', '0', strSumAction.Length - actionBit - 1, 1);

            return strSumAction.ToString();
        }

        /// <summary>
        /// Add two binary string
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static String AndBinary(String a, String b)
        {
            int a_len = a.Length;
            int b_len = b.Length;

            int max = Math.Max(a_len, b_len);

            StringBuilder and = new StringBuilder("");
            for (int i = 0; i < max; i++)
            {
                int m = GetBit(a, a_len - i - 1);
                int n = GetBit(b, b_len - i - 1);
                and.Insert(0, m & n);
            }

            return and.ToString();
        }

        private static int GetBit(String s, int index)
        {
            if (index < 0 || index >= s.Length)
                return 0;

            if (s[index] == '0')
                return 0;
            else
                return 1;
        }


        /// <summary>
        /// Check if index of action bit from sum of action be 1
        /// </summary>
        /// <param name="sumOfAction"></param>
        /// <param name="actionBit"></param>
        /// <returns></returns>
        public static bool HasAccess(String sumOfAction, short actionBit)
        {
            String strActionBit = ConvertIntToBit(actionBit, sumOfAction.Length);
            String result = AndBinary(sumOfAction, strActionBit);

            return result == strActionBit;
        }

        public static bool HasAccess(String sumOfAction, short[] actionBits)
        {
            foreach (short actionBit in actionBits)
            {
                string strActionBit = ConvertIntToBit(actionBit, sumOfAction.Length);
                string result = AndBinary(sumOfAction, strActionBit);

                if (result == strActionBit)
                    return true;
            }

            return false;
        }

        private static String ConvertIntToBit(int actionBit, int len)
        {
            StringBuilder strBulderAction = new StringBuilder("1");
            for (int i = 0; i < actionBit; i++)
            {
                strBulderAction.Append("0");
            }

            while (len > strBulderAction.Length)
            {
                strBulderAction.Insert(0, "0");
            }

            return strBulderAction.ToString();
        }
        #endregion


        #region ### Return Message ###
        public static MessageResult NotFoundMessage()
        {
            return new MessageResult
            {
                FaMessage = "رکورد مورد نظر یافت نگردید.",
                MessageType = MessageType.NotFound
            };
        }


        //public static MessageResultApi UnauthorizedMessage()
        //{
        //    return new MessageResultApi
        //    {
        //        Message = "عدم دسترسی.",
        //        MessageType = MessageType.Unauthorized
        //    };
        //} 
        #endregion

        public static bool CheckImageExtention(String extention)
        {
            extention = extention.ToLower();
            return (extention == ".jpg" || extention == ".gif" || extention == ".jpeg"
                    || extention == ".png" || extention == ".bmp" || extention == ".icn");
        }

        public static bool CheckWordFileExtention(String extention)
        {
            extention = extention.ToLower();
            return (extention == ".doc" || extention == ".docx");
        }

        public static bool CheckExcelFileExtention(String extention)
        {
            extention = extention.ToLower();
            return (extention == ".xls" || extention == ".xlsx");
        }

        #region ### utility ###
        public static bool isExistInArray<T>(IEnumerable<T> list , T key ) // todo: hossein, remove
        {
            foreach(T temp in list)
            {
                if(temp.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }


        #endregion
    }
}
