﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileConductor.Schedule
{
    public class IntervalScheduler
    {
        private readonly Timer _interval;

        /// <param name="interval">Schedulding time in miliseconds</param>
        /// <param name="scheduleElapsed">Handler for action after each interval</param>
        /// <param name="startImmediately">If true, starts scheduling just after constructing, otherwise waits for Start method's first call</param>
        public IntervalScheduler(int interval, ElapsedEventHandler scheduleElapsed,
            bool startImmediately = true)
        {
            _interval = new Timer(interval);
            _interval.Elapsed += scheduleElapsed;
            if (startImmediately)
                _interval.Start();
        }

        public void Start()
        {
            _interval.Start();
        }

        public void Stop()
        {
            _interval.Stop();
        }
    }
}