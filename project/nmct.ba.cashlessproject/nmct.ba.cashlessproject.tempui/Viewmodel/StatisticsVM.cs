﻿using nmct.ba.cashlessproject.tempui.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.tempui.Viewmodel
{
    class StatisticsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "STATISTICS"; }
        }
    }
}