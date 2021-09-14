using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;
        public int level;

        public int TimeSpeed;//должно меняться при изменении времени в игре

        //public Grid[,] Building;
        public int[,] grid;

        //Далее код не особо нужен для моей игры. Но я переделываю(нет, это будет всегда, я сделаю на нем спавн камеры, хахахахах).
        public Vector3 position;
        public Quaternion rotation;

        private CheckBuildingInGrid check;
        public List<Grid> Building;

        private bool Changes = false;
        private int changedX = -1, changedY = -1;
        /*private int MaxX = 0;
        private int MaxY = 0;
        private int MinX = 0;
        private int MinY = 0;*/

        private int size;//size of sqrt(grid.lenght)

        public Player(int _id, string _username, Vector3 _spawnPosition, int _size)//constructor
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
            Building = new List<Grid>();
            size = _size;
            grid = new int[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    grid[i, j] = -1;
                }
            }
            check = new CheckBuildingInGrid(_size, this);
        }

        public void AddToBuildings(int x, int y)//с удалением сделай потом
        {
            check = new CheckBuildingInGrid(size, this);
            int[,] new_building = check.CheckForBuilding(x, y);

            int diff = 0;

            if (new_building.Length > 1)
            {
                for (int k = 0; k > Building.Count; k++)
                {
                    for (int i = 0; i < Client.GridSize; i++)
                    {
                        for (int j = 0; j < Client.GridSize; j++)
                        {
                            if (Building[k].CurrentBuilding[i, j] != new_building[i, j])
                            {
                                diff++;
                            }
                        }
                    }
                    if (diff >= 1)
                    {
                        for (int i = 0; i < Client.GridSize; i++)
                        {
                            for (int j = 0; j < Client.GridSize; j++)
                            {
                                Building[k].CurrentBuilding[i, j] = new_building[i, j];
                            }
                        }
                        break;
                    }
                    diff = 0;
                }
            }
            else
            {
                //Инициализация типа нового здания
                Grid tmp_grid = new Grid();
                tmp_grid.CurrentBuilding = new int[Client.GridSize, Client.GridSize];
                for (int i = 0; i < Client.GridSize; i++)
                {
                    for (int j = 0; j < Client.GridSize; j++)
                    {
                        tmp_grid.CurrentBuilding[i, j] = new_building[i, j];
                    }
                }
                Console.WriteLine("c");
                Building.Add(tmp_grid);
                for (int i = 0; i < Client.GridSize; i++)
                {
                    for (int j = 0; j < Client.GridSize; j++)
                    {
                        switch (Building[Building.Count - 1].CurrentBuilding[i, j])//change building type
                        {
                            case 0:
                                Building[Building.Count - 1].InitResidental();//и не надо никаких классов в массиве)
                                break;
                        }
                    }
                }
            }
        }

        public void Update()//works every tps
        {
            ServerSend.BuildingGrid(this, Changes);
            //ServerSend.CursorGrid(this);  пока нормально не работает, да и не очень оно надо, поэтому не делаю.
            ServerSend.StageGrid(this);

            /*for (int i = MinX; i <= MaxX; i++)
            {
                for (int j = MinY; j <= MaxY; j++)
                {*///где-то накосячил с минимумом и максимумом, наверное по Y
            for (int k = 0; k < Building.Count; k++)
            {
                if (Building[k].TimeTillStage > -1)
                {
                    if (Building[k].TimeTillStage == 0)
                    {
                        ChangeStage(k);
                    }
                    Building[k].TimeTillStage--;
                }
            }
        }

        public void SetGrid(int[,] _grid, bool _changes)//in serverhandle
        {
            Changes = _changes;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)//Надо оптимизировать эти циклы, а то их многовато становится
                {
                    if (grid[i, j] != _grid[i, j] && _grid[i, j] != -1 && grid[i, j] == -1)
                    {
                        grid[i, j] = _grid[i, j];
                        changedX = i;
                        changedY = j;
                        /*if (i > MaxX)//Later i need to clear this values, but idk how, now..... anyway
                        {
                            MaxX = i;
                        }
                        else if(j > MaxY)
                        {
                            MinY = j;
                        }
                        else if (i < MinX)
                        {
                            MinX = i;
                        }
                        else if (j < MinY)
                        {
                            MinY = j;
                        }*/

                        //_stageCh = true;
                        //x = i;
                        //y = j;
                        break;
                    }
                }
            }
            AddToBuildings(changedX, changedY);
        }

        public void SetCursor(int[,] _grid)
        {
            //Cursor = _grid;
        }

        /*public void StartStage(int x, int y)
        {
            Building[x, y].TimeTillStage = 1;//потом сюда время пихну как переменную
        }*/

        public void ChangeStage(int num)
        {
            Console.WriteLine("h");
            if (Building[num].NumOfStages > Building[num].Stage)
            {
                Console.WriteLine("a");
                int _eventNum = RandomEvent(level, Building[num].PossibleEvents.Length);
                Console.WriteLine(_eventNum);
                _eventNum = (int)Building[num].PossibleEvents[_eventNum];
                string _type = Building[num].TypeOfBuilding;
                MyEvents.StartEvent(_eventNum, _type);
                Building[num].Stage++;
            }
            Building[num].StartStage();
        }

        private int RandomEvent(int _lvl, int _lenght)
        {
            var rand = new Random();
            int n = rand.Next(1, 101);
            if (_lenght == 1)
            {
                return 0;
            }
            else if (_lenght == 2)
            {
                n = rand.Next(1, 61);
            }
            else if (_lenght == 3)
            {
                n = rand.Next(1, 76);
            }
            else if (_lenght == 4)
            {
                n = rand.Next(1, 86);
            }
            else if (_lenght == 5)
            {
                n = rand.Next(1, 93);
            }
            else if (_lenght == 6)
            {
                n = rand.Next(1, 97);
            }


            if (rand.Next(3) > 0 && n > _lvl)//sometimes bigger lvl make more easyer events
            {
                n -= _lvl;
            }


            if (n > 0 && n <= 40) return 0;
            else if (n > 40 && n <= 60) return 1;
            else if (n > 60 && n <= 75) return 2;
            else if (n > 75 && n <= 85) return 3;
            else if (n > 85 && n <= 92) return 4;
            else if (n > 92 && n <= 96) return 5;
            return 6;
        }
    }
}
