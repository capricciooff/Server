using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    /////////////////////////////////////////////////////////////////////
    public class Grid
    {
        public int CurrentBuilding = 0;

        public int Stage;
        public int NumOfStages;

        public buildingEvents[] PossibleEvents;

        public int Comfort;
        public int People;

        public string TypeOfBuilding;

        public virtual void SetEvents(int k)
        {
            PossibleEvents = new buildingEvents[k];
        }
    }

    public class ResidentalBuilding : Grid
    {
        public override void SetEvents(int k)
        {
            base.SetEvents(2);
            TypeOfBuilding = "Residental";
            PossibleEvents[0] = (buildingEvents)0;
            PossibleEvents[1] = (buildingEvents)1;
        }
        public ResidentalBuilding()
        {
            this.SetEvents(1);
        }
    }
    /////////////////////////////////////////////////////////////////////
    ///
    public enum buildingEvents
    {
        Meeteng = 0,
        Сollapse = 1
    }
    ///
}
