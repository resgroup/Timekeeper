using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RES_Timekeeper.Data;

namespace RES_Timekeeper
{
    public class TimePeriodManager
    {
        private DateTime _currentPeriodStartTime;

#if DEBUG
        private const int PERIODS_IN_HOUR = 60;
        private const int MINUTES_IN_PERIOD = 1;
#else
        private const int PERIODS_IN_HOUR = 4;
        private const int MINUTES_IN_PERIOD = 15;
#endif
        private const int MINIMUM_SECONDS_IN_PERIOD = (int)(MINUTES_IN_PERIOD * 60 * 0.2);
        private const int LUNCH_PROJECTID = 1;


        public TimePeriodManager()
        {
            // If we're being constructed within MINUTES_IN_PERIOD of the end of the last period
            // then we'll start this period at the end of the end of the last period. This covers
            // the case of a quick reboot; it's thus recorded as continual working
            Item lastItem = Item.GetMostRecentItem();
            if (lastItem != null && lastItem.EndTime > (DateTime.Now.AddMinutes(-MINUTES_IN_PERIOD)))
                _currentPeriodStartTime = lastItem.EndTime;
            else
                _currentPeriodStartTime = DateTime.Now;

            Paused = false;
        }


        /// <summary>
        /// Called often, e.g. once a second. Sees if the current period has come to an end.
        /// </summary>
        public Item TimerTick(string message)
        {
            TimeSpan lengthofPeriod = DateTime.Now - _currentPeriodStartTime;
            DateTime now = DateTime.Now;
            if (now > ExpectedTimeOfPeriodEnd() && lengthofPeriod.TotalSeconds > MINIMUM_SECONDS_IN_PERIOD)
            {
                // OK - we're beyond the end of the period, and the period is longer than 20% of the standard length
                // (avoid short periods just after starting up). So create and save a new period
                if (IsPeriodOutOfWorkingHours()  ||  Paused)
                {
                    // Don't create a new period - let the current one extend on as we're out of hours
                }
                else
                {
                    Item newItem = CreateAndSaveNewItem(message);
                    _currentPeriodStartTime = now;
                    return newItem;
                }
            }

            return null;
        }


        /// <summary>
        /// The work session is being suspended or ended. Create and save a period up to now
        /// </summary>
        public void SessionEnding(string message)
        {
            DateTime now = DateTime.Now;
            // Don't bother creating short work periods
            if ((now - _currentPeriodStartTime).TotalSeconds > MINIMUM_SECONDS_IN_PERIOD)
            {
                CreateAndSaveNewItem(message);
                _currentPeriodStartTime = now;
            }
        }

        
        /// <summary>
        /// The work session is resuming.  Create a period covering the time of the suspension and start a new period
        /// </summary>
        public void SessionResuming()
        {
            DateTime now = DateTime.Now;
            if ((now - _currentPeriodStartTime).TotalSeconds > MINIMUM_SECONDS_IN_PERIOD)
            {
                CreateAndSaveNewItem("The workstation was locked during this period.", LUNCH_PROJECTID);
                _currentPeriodStartTime = now;
            }
        }


        /// <summary>
        /// Returns the expected end of period time based on the start time and fixed length of period
        /// </summary>
        /// <returns></returns>
        public DateTime ExpectedTimeOfPeriodEnd()
        {
            DateTime now = DateTime.Now;
            DateTime d = DateTime.Now.Date;
            d = d.AddHours(now.Hour);
            while (d <= _currentPeriodStartTime)
                d = d.AddMinutes(MINUTES_IN_PERIOD);

            return d;
        }

        public bool Paused
        {
            get;
            set;
        }


        private bool IsPeriodOutOfWorkingHours()
        {
            // Working hours are from 8AM to 8PM
            DateTime now = DateTime.Now;
            return (_currentPeriodStartTime.TimeOfDay.TotalHours > 20  &&  (now.Date == _currentPeriodStartTime.Date || now.TimeOfDay.TotalHours < 8.02) );
        }


        private Item CreateAndSaveNewItem(string description, int projectIDForPeriod)
        {
            Item newItem = Item.CreateNewItem(_currentPeriodStartTime, DateTime.Now);
            Item lastItem = Item.GetMostRecentItem();
            newItem.ProjectID = projectIDForPeriod;
            if (!string.IsNullOrEmpty(description))
                newItem.Notes = description;
            newItem.Save();
            return newItem;
        }


        private Item CreateAndSaveNewItem(string description)
        {
            Item lastItem = Item.GetMostRecentItem();
            return CreateAndSaveNewItem(description, lastItem != null ? lastItem.ProjectID : LUNCH_PROJECTID);
        }
    }
}
