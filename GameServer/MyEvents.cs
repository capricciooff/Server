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

        //public buildingEvents[] possibleEvent;//сюда наверное надо послать что-то, или хз на+хуя мне это

        public static void StartEvent(int _num, string _type)
        {
            myevent = (buildingEvents)_num;
            InitializeEvent(myevent);
            Console.WriteLine($"In {_type} building is {myevent}");
        }

        public static void InitializeEvent(buildingEvents _event)
        {
            Console.WriteLine(_event);
        }
    }
}
