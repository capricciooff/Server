using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class MyEvents
    {
        private static buildingEvents myevent;

        //public buildingEvents[] possibleEvent;//сюда наверное надо послать что-то, или хз зачем

        public static void StartEvent(int _num, string _type)
        {
            myevent = (buildingEvents)_num;
            InitializeEvent(myevent);
            Console.WriteLine($"Starting event {myevent} in {_type} building");
        }

        public static void InitializeEvent(buildingEvents _event)
        {
            //
        }
    }
}
