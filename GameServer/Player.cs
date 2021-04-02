using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;
        public int level;

        public int TimeSpeed;//должно меняться при изменении времени в игре

        public Grid[,] Building;

        //Далее код не особо нужен для моей игры. Но я переделываю.
        public Vector3 position;
        public Quaternion rotation;

        private CheckBuildingInGrid check;
        public List<int[,]> BuildingS;

        bool Changes = false;

        public Player(int _id, string _username, Vector3 _spawnPosition, int _size)//constructor
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
            Building = new Grid[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Building[i, j].CurrentBuilding = -1;
                    Building[i, j].Stage = -1;
                    //Cursor[i, j] = -1;
                }
            }
            check = new CheckBuildingInGrid(_size, this);
        }

        public void Update()//works every tps
        {
            ServerSend.BuildingGrid(this, Changes);
            //ServerSend.CursorGrid(this);  пока нормально не работает, да и не очень оно надо, поэтому не делаю.
            ServerSend.StageGrid(this);
            BuildingS = check.CheckForBuildingS();
        }

        public void SetGrid(int[,] _grid, bool _changes)//in serverhandle
        {
            Changes = _changes;
            bool _stageCh = false;
            int x = 0, y = 0;
            for (int i = 0; i < (_grid.Length) / 2; i++)
            {
                for (int j = 0; j < (_grid.Length) / 2; j++)
                {
                    if (Building[i, j].CurrentBuilding != _grid[i, j] && _grid[i, j] != -1 && Building[i, j].CurrentBuilding == -1)
                    {
                        _stageCh = true;
                        x = i;
                        y = j;
                        break;
                    }
                }
            }


            for (int i = 0; i < (_grid.Length) / 2; i++)
            {
                for (int j = 0; j < (_grid.Length) / 2; j++)
                {
                    Building[i, j].CurrentBuilding = _grid[i, j];
                }
            }


            if (_stageCh)
            {
                ChangeStage(x, y);
            }
        }

        public void SetCursor(int[,] _grid)
        {
            //Cursor = _grid;
        }

        public void ChangeStage(int x, int y)
        {
            var rand = new Random();
            BuildingS = check.CheckForBuildingS();
            int[,] house = check.CheckForBuilding(x, y);

            System.Threading.Thread.Sleep(5000);

            for (int n = 0; n < house.Length; n++)
            {
                for (int k = 0; k < house.Length; k++)
                {
                    if (house[n, k] != -1 && Building[n, k].NumOfStages > Building[n, k].Stage)
                    {
                        int _eventNum = rand.Next(Building[n, k].PossibleEvents.Length) - level + (int)Math.Round((float)(level / rand.Next(level + 1)), MidpointRounding.AwayFromZero);
                        if (_eventNum < 0)
                        {
                            _eventNum = 0;//0 - самое вероятное событие
                        }
                        _eventNum = (int)Building[n, k].PossibleEvents[_eventNum];
                        string _type = Building[n, k].TypeOfBuilding;
                        MyEvents.StartEvent(_eventNum, _type);

                        for (int i = 0; i < house.Length; i++)
                        {
                            for (int j = 0; j < house.Length; j++)
                            {
                                if (house[i, j] != -1)
                                {
                                    Building[i, j].Stage++;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            ChangeStage(x, y);
        }

        /*//Counting changing stage of building.
        IEnumerator StageBuilder(int i)
        {
            yield return new WaitForSecondsRealtime(5f);
            level = GetComponentInParent<PlayerManager>().level;
            for (int n = 0; n < lengthGrid; n++)
            {
                for (int k = lengthGrid; k >= 0; k++)
                {
                    if (checkBuilding.Buildings[i][n, k] != -1 && Grid[n, k].NumOfStages > Grid[n, k].Stage)
                    {
                        //возможен проеб в математике, проверим потом)))
                        int _eventNum = UnityEngine.Random.Range(0, Grid[n, k].PossibleEvents.Length) - level + (int)Math.Round((float)(level / UnityEngine.Random.Range(1, level + 1)), MidpointRounding.AwayFromZero);
                        if (_eventNum < 0)
                        {
                            _eventNum = 0;//0 - самое вероятное событие
                        }
                        _eventNum = (int)Grid[n, k].PossibleEvents[_eventNum];
                        string _type = Grid[n, k].TypeOfBuilding;
                        cityGrid.GetComponent<MyEvents>().StartEvent(_eventNum, _type);
                        for (int f = 0; f < cityGrid.Size; f++)
                        {
                            for (int j = 0; j < cityGrid.Size; j++)
                            {
                                if (checkBuilding.Buildings[i][f, j] != -1)
                                {
                                    Debug.Log($"Building {i} with part on ({f},{j}) new stage.");
                                    Grid[f, j].Stage++;
                                }
                            }
                        }
                        StartCoroutine(StageBuilder(i));
                        break;
                        //break;
                    }
                }
            }
        }*/
    }
}
