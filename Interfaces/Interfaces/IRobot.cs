using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRobot
    {
        public string GetInfo();


        public void GetComponents();

        public string GetRobotType()
        {
            return "I am a simple robot";
        }

    }
}
