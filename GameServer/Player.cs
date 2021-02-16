using System;
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

        public int[,] CurrentBuilding;
        public int[,] Stage;
        public int[,] Cursor;

        //Далее код не особо нужен для моей игры. Но я переделываю.
        public Vector3 position;
        public Quaternion rotation;

        bool Changes = false;

        public Player(int _id, string _username, Vector3 _spawnPosition, int _size)//constructor
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
            CurrentBuilding = new int[_size, _size];
            Stage = new int[_size, _size];
            Cursor = new int[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    CurrentBuilding[i, j] = -1;
                    Stage[i, j] = -1;
                    Cursor[i, j] = -1;
                }
            }
        }

        public void Update()//works every tps
        {
            ServerSend.BuildingGrid(this, Changes);
            //ServerSend.CursorGrid(this);  пока нормально не работает, да и не очень оно надо, поэтому не делаю.
            ServerSend.StageGrid(this);
        }

        public void SetGrid(int[,] _grid, int[,] _stage, bool _changes)
        {
            Changes = _changes;
            CurrentBuilding = _grid;
            Stage = _stage;
        }

        public void SetCursor(int[,] _grid)
        {
            Cursor = _grid;
        }
    }
}
