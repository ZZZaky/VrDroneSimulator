using System;
namespace Tools.Timer
{
    public struct TimerTime
    {
        #region Fields

        public int Hours;
        public int Minutes;
        public int Seconds;
        public int Milliseconds;

        #endregion

        #region Constructors

        public TimerTime(int hours, int minutes, int seconds, int milliseconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public TimerTime(string time)
        {
            string[] timeComponents = time.Split(':');
            Hours = 0;
            Minutes = Convert.ToInt32(timeComponents[0]);
            Seconds = Convert.ToInt32(timeComponents[1]);
            Milliseconds = Convert.ToInt32(timeComponents[2]);
        }

        #endregion

        #region Public Methods

        public float ToSeconds()
        {
            return Hours * 3600 + Minutes * 60 + Seconds + Milliseconds / 1000;
        }

        public override string ToString()
        {
            return $"{Minutes.ToString("D2")}:{Seconds.ToString("D2")}:{Milliseconds.ToString("D3")}";
        }

        #endregion

        #region Operators

        public static bool operator >(TimerTime ln1, TimerTime ln2)
        {
            return ln1.ToSeconds() > ln2.ToSeconds();
        }

        public static bool operator <(TimerTime ln1, TimerTime ln2)
        {
            return ln1.ToSeconds() < ln2.ToSeconds();
        }

        public static bool operator >=(TimerTime ln1, TimerTime ln2)
        {
            return ln1.ToSeconds() >= ln2.ToSeconds();
        }

        public static bool operator <=(TimerTime ln1, TimerTime ln2)
        {
            return ln1.ToSeconds() <= ln2.ToSeconds();
        }

        #endregion
    }

}