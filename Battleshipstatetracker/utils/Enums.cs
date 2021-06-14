using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Battleshipstatetracker.utils
{
   
        public enum boardStatus
        {
            [Description("O")]
            empty,

            [Description("S")]
            ship,

            [Description("H")]
            hit,

            [Description("M")]
            miss,

            [Description("S")]
            sunk
        }
       public enum CONSTANTS
        {
            boardDimension = 10
        }
}
