using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class MyEvents
    {
        private static buildingEvents _event;

        //public buildingEvents[] possibleEvent;//сюда наверное надо послать что-то, или хз на+хуя мне это

        public static void StartEvent(int _num, string _type)
        {
            _event = (buildingEvents)_num;
            InitializeEvent(_num);
            //Console.WriteLine($"In {_type} building is {_event}");
        }

        public static void InitializeEvent(int _num)
        {

        }
    }
}
