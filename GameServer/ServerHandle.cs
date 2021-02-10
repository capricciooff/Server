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
            int k = _packet.ReadInt() / 2;
            int[,] _grid = new int[Convert.ToInt16(Math.Sqrt(k)), Convert.ToInt16(Math.Sqrt(k))];
            for (int i = 0; i < Convert.ToInt16(Math.Sqrt(k)); i++)
            {
                for (int j = 0; j < Convert.ToInt16(Math.Sqrt(k)); j++)
                {
                    _grid[i, j] = _packet.ReadInt();
                }
            }

            int[,] _stage = new int[Convert.ToInt16(Math.Sqrt(k)), Convert.ToInt16(Math.Sqrt(k))];
            for (int i = 0; i < Convert.ToInt16(Math.Sqrt(k)); i++)
            {
                for (int j = 0; j < Convert.ToInt16(Math.Sqrt(k)); j++)
                {
                    _stage[i, j] = _packet.ReadInt();
                }
            }

            Server.clients[_fromClient].player.SetGrid(_grid, _stage);
        }

        public static void CursorGrid(int _fromClient, Packet _packet)
        {
            int k = _packet.ReadInt();
            int[,] _grid = new int[Convert.ToInt16(Math.Sqrt(k)), Convert.ToInt16(Math.Sqrt(k))];
            for (int i = 0; i < Convert.ToInt16(Math.Sqrt(k)); i++)
            {
                for (int j = 0; j < Convert.ToInt16(Math.Sqrt(k)); j++)
                {
                    _grid[i, j] = _packet.ReadInt();
                }
            }

            Server.clients[_fromClient].player.SetCursor(_grid);
        }
    }
}
