using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class CheckBuildingInGrid
    {
        private int[,] grid;
        public List<int[,]> Buildings = new List<int[,]>();

        private int[,] notReady;
        private int[,] building;

        private int Size;

        int CurrentBuilding;

        public CheckBuildingInGrid(int _size, Player _player)
        {
            Size = _size;
            notReady = new int[Size, Size];
            building = new int[Size, Size];
            grid = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    notReady[i, j] = -1;
                    building[i, j] = -1;
                    grid[i, j] = _player.grid[i, j];
                }
            }
        }

        public int[,] CheckForBuilding(int x, int y)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    notReady[i, j] = grid[i, j];
                }
            }

            for (int n = 0; n < Size; n++)
            {
                for (int k = Size - 1; k >= 0; k--)
                {
                    building[n, k] = -1;
                }
            }

            for (int i = 0; i < Size; i++)
            {
                for (int j = Size - 1; j >= 0; j--)
                {
                    if (notReady[i, j] != -1)
                    {
                        CurrentBuilding = notReady[i, j];
                    }
                }
            }

            CheckBench(x, y);

            return building;
        }

        public List<int[,]> CheckForBuildingS()
        {
            Buildings.Clear();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    notReady[i, j] = grid[i, j];
                }
            }

            for (int i = 0; i < Size; i++)
            {
                for (int j = Size - 1; j >= 0; j--)
                {
                    if (notReady[i, j] != -1)
                    {
                        CurrentBuilding = notReady[i, j];
                        CheckBench(i, j);
                        Buildings.Add(building);
                        for (int n = 0; n < Size; n++)
                        {
                            for (int k = Size - 1; k >= 0; k--)
                            {
                                building[n, k] = -1;
                            }
                        }
                    }
                }
            }

            return Buildings;
        }

        public void CheckBench(int i, int j)
        {
            if (notReady[i, j] != -1)
            {
                notReady[i, j] = -1;
                building[i, j] = CurrentBuilding;
                if (notReady[i - 1, j] == CurrentBuilding && i - 1 >= 0 && notReady[i - 1, j] != building[i - 1, j] && i - 1 >= 0)
                {
                    CheckBench(i - 1, j);
                }
                if (notReady[i, j - 1] == CurrentBuilding && notReady[i, j - 1] != building[i, j - 1] && j - 1 >= 0)
                {
                    CheckBench(i, j - 1);
                }
                if (notReady[i, j + 1] == CurrentBuilding && notReady[i, j + 1] != building[i, j + 1] && j + 1 < Size)
                {
                    CheckBench(i, j + 1);
                }
                if (notReady[i + 1, j] == CurrentBuilding && notReady[i + 1, j] != building[i + 1, j] && i + 1 < Size)
                {
                    CheckBench(i + 1, j);
                }
            }
        }
    }
}
