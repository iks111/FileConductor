﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FileConductor.Schedule
{
    public class SpecifiedTimeSchedule : ISchedule
    {
        private readonly int[] _days;
        private readonly TimeSpan _executionTime;
        private Timer _interval;
        private int _previousExecutionDay = -1;
        private readonly ElapsedEventHandler _scheduleElapsed;


        public SpecifiedTimeSchedule(int[] days, TimeSpan executionTime)
        {
            _days = days;
            _executionTime = executionTime;
        }

        private int Today => (int) DateTime.Now.DayOfWeek;

        private void CalculateNextRequiredTime()
        {
            var closestDay = FindClosestDay();
            var nextExecutionTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, closestDay, _executionTime.Hours,
                _executionTime.Minutes, _executionTime.Seconds);
            var timeDiffrence = nextExecutionTime.Subtract(DateTime.Now);
            if(timeDiffrence.TotalMilliseconds < 0)
            {
                _previousExecutionDay = 0;
                CalculateNextRequiredTime();
            }
            else
            {
                _interval = new Timer(timeDiffrence.TotalMilliseconds);
                _interval.Start();
            }   
        }

        private int FindClosestDay()
        {
            List<int> daysToClosestDay = new List<int>(_days);
            for (var i = 0; i < daysToClosestDay.Count; i++)
            {
                daysToClosestDay[i] -= Today;
                daysToClosestDay[i] = daysToClosestDay[i];
            }
            daysToClosestDay.ToList().Sort();
            int closestDay = daysToClosestDay.First(x=>x != _previousExecutionDay && x >= 0);
            return DateTime.Now.Day + closestDay;
        }

        public void StartSchedule(Action action)
        {
            CalculateNextRequiredTime();
            _interval.Elapsed += (s, e) =>
            {
                _interval.Stop();
                _previousExecutionDay = 0;
                action();
                CalculateNextRequiredTime();
            };
        }

        public void StopSchedule()
        {
            _interval.Stop();
        }
    }
}