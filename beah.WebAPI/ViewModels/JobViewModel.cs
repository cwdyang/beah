﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using beah.Domain.Models;

namespace beah.WebAPI.ViewModels
{
    public class JobViewModel
    {
        public Job Job { get; set; }

        //always need this!!
        public JobViewModel()
        {
            
        }

        public JobViewModel(Job job)
        {
            Job = job;            
        }
    }
}