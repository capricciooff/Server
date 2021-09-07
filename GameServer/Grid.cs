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
        public int[,] CurrentBuilding;

        public int Stage;
        public int NumOfStages;

        public buildingEvents[] PossibleEvents;

        public int Comfort;
        public int People;

        public string TypeOfBuilding;

        public int TimeTillStage = -1;//sec remain to next stage
        private int standartStageTime = -1;//sec betwen stages

        /*public virtual void SetEvents(int k)
        {
            PossibleEvents = new buildingEvents[k];
            PossibleEvents[0] = (buildingEvents)0;
        }*/
        public void InitResidental()
        {
            TypeOfBuilding = "Residental";
            PossibleEvents = new buildingEvents[2];
            PossibleEvents[0] = (buildingEvents)0;
            PossibleEvents[1] = (buildingEvents)1;
            NumOfStages = 2;
            Stage = 1;
            standartStageTime = 5;
            StartStage();
        }
        public void StartStage()
        {
            TimeTillStage = standartStageTime * Constants.TICKS_PER_SEC;
        }
    }

    /*public class ResidentalBuilding : Grid
    {
        public override void SetEvents(int k)
        {
            base.SetEvents(k);
            TypeOfBuilding = "Residental";
            PossibleEvents[0] = (buildingEvents)0;
            PossibleEvents[1] = (buildingEvents)1;
        }
        public ResidentalBuilding()
        {
            this.SetEvents(2);
            NumOfStages = 2;
        }
    }*/
    /////////////////////////////////////////////////////////////////////
    ///
    public enum buildingEvents
    {
        None = 0,
        Meeteng = 1,
        Сollapse = 2
    }
    ///
}
