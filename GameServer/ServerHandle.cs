using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            //sending player into game
            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void PlayerGrid(int _fromClient, Packet _packet)
        {
            bool _changes = false;
            int k = _packet.ReadInt();
            short _length = Convert.ToInt16(Math.Sqrt(k));
            int[,] _grid = new int[_length, _length];
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    _grid[i, j] = _packet.ReadInt();
                    if ((Server.clients[_fromClient].player.grid[i, j]) != _grid[i, j])
                    {
                        _changes = true;
                    }
                }
            }

            Server.clients[_fromClient].player.SetGrid(_grid, _changes);
        }

        public static void CursorGrid(int _fromClient, Packet _packet)
        {
            int k = _packet.ReadInt();
            short _length = Convert.ToInt16(Math.Sqrt(k));
            int[,] _grid = new int[_length, _length];
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    _grid[i, j] = _packet.ReadInt();
                }
            }

            Server.clients[_fromClient].player.SetCursor(_grid);
        }
    }
}
