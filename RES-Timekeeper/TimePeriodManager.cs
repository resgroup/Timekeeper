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

        private const int LUNCH_PROJECTID = 1;

        public bool Paused { get; set; }
        public TimePeriodManager(OptionsForm.TimeTrackingLevels timeLevel)
        {
            TimingInterval = timeLevel;
            Paused = false;

            // If we're being constructed within MINUTES_IN_PERIOD of the end of the last period
            // then we'll start this period at the end of the end of the last period. This covers
            // the case of a quick reboot; it's thus recorded as continual working
            var lastItem = Item.GetMostRecentItem();
            if (lastItem != null && lastItem.EndTime > (DateTime.Now.AddMinutes(-MINUTES_IN_PERIOD)))
            {
                _currentPeriodStartTime = lastItem.EndTime;
            }
            else
            {
                _currentPeriodStartTime = DateTime.Now;
            }
        }


        /// <summary>
        /// Called often, e.g. once a second. Sees if the current period has come to an end.
        /// </summary>
        public Item TimerTick(string message)
        {
            var now = DateTime.Now;
            var lengthofPeriod = now - _currentPeriodStartTime;

            if (now > ExpectedTimeOfPeriodEnd() && lengthofPeriod.TotalSeconds > MINIMUM_SECONDS_IN_PERIOD)
            {
                // OK - we're beyond the end of the period, and the period is longer than 20% of the standard length
                // (avoid short periods just after starting up). So create and save a new period
                if (Paused)
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
            var now = DateTime.Now;
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
            var now = DateTime.Now;
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
            var now = DateTime.Now;
            var nowDate = DateTime.Now.Date;
            nowDate = nowDate.AddHours(now.Hour);
            while (nowDate <= _currentPeriodStartTime)
            {
                nowDate = nowDate.AddMinutes(MINUTES_IN_PERIOD);
            }
            return nowDate;
        }


        private Item CreateAndSaveNewItem(string description, int projectIDForPeriod)
        {
            var newItem = Item.CreateNewItem(_currentPeriodStartTime, DateTime.Now);
            var lastItem = Item.GetMostRecentItem();
            newItem.ProjectID = projectIDForPeriod;
            if (!string.IsNullOrEmpty(description))
            {
                newItem.Notes = description;
            }
            newItem.Save();
            return newItem;
        }

        private Item CreateAndSaveNewItem(string description)
        {
            var lastItem = Item.GetMostRecentItem();
            return CreateAndSaveNewItem(description, lastItem != null ? lastItem.ProjectID : LUNCH_PROJECTID);
        }

        private int MINIMUM_SECONDS_IN_PERIOD
        {
            get
            {
                return (int)(MINUTES_IN_PERIOD * 60 * 0.2);
            }
        }

        public OptionsForm.TimeTrackingLevels TimingInterval
        {
            get;
            set;
        }


        public int MINUTES_IN_PERIOD
        {
            get
            {
                switch (TimingInterval)
                {
                    case OptionsForm.TimeTrackingLevels.M1:  return  1;
                    case OptionsForm.TimeTrackingLevels.M15: return 15;
                    case OptionsForm.TimeTrackingLevels.M30: return 30;
                    case OptionsForm.TimeTrackingLevels.H1:  return 60;
                }
                return 15;
            }
        }
    }
}
