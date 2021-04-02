using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        /// <summary>Sends a packet to all clients via TCP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via TCP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }



        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        /// <summary>Sends a packet to all clients via UDP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via UDP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(int _expectClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _expectClient)
                    Server.clients[i].udp.SendData(_packet);
            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)//Здесь будем использовать TCP, т.к. пакет важный и нельзя его терять!!!
        {
            //Console.WriteLine("Created packet to spawn");
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void BuildingGrid(Player _player, bool _changes)
        {
            //if (_changes)   //используется, чтобы посылать только при измененнии, но, чтобы не потерять пакет, лучше пока убрать. :^(
            {
                using (Packet _packet = new Packet((int)ServerPackets.gridBuilding))
                {
                    _packet.Write(_player.id);
                    _packet.Write(_player.Building.Length);
                    foreach (Grid _building in _player.Building)
                    {
                        _packet.Write(_building.CurrentBuilding);
                    }
                    SendTCPDataToAll(_player.id, _packet);//Здесь будем использовать TCP, т.к. пакет важный и нельзя его терять!!!
                }
            }
        }
        /*public static void CursorGrid(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.gridCursor))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.Cursor.Length);
                foreach (int _cursor in _player.Cursor)
                {
                    _packet.Write(_cursor);
                }
                SendUDPDataToAll(_packet);//UDP быстрее
            }
        }*/
        public static void StageGrid(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.gridStage))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.Building.Length);
                foreach (Grid _building in _player.Building)
                {
                    _packet.Write(_building.Stage);
                }
                SendUDPDataToAll(_player.id, _packet);//UDP быстрее
            }
        }
        #endregion
    }
}
