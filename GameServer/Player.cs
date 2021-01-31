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

        //Далее код не для моей игры. Надо переделать это в данные о сетках. Для начала оставлю так.
        public Vector3 position;
        public Quaternion rotation;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
        }
    }
}
