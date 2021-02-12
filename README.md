# Server
 
Сервер запускается из кода Program.cs. В Main вызов Server.Start(макс. кол-во игроков, порт).\n
Для оптимизации работы Unity с сервером надо синхронизировать вызовы FixedUpdate и TPS сервера. TPS записан в константах, а Time Unity должен быть 1/TPS.\n
Вид пакетов:\n
 \tПосылаемые на сервер:\n
  \t\tid пакета             метод приёма     содержание\n
  \t\t1 welcomeReceived     WelcomeReceived  id клиента, username\n
  \t\t2 playerGrid          PlayerGrid       длинна массива(2 двумерных массива), массив int(вообще их 2, но записаны подряд)\n
  \t\t3 cursorGrid          CursorGrid       длинна массива(двумернq массив), массив int\n
 \tПосылаемые сервером:\n
  \t\tid пакета             метод отправки    содержание\n
  \t\t1 welcome             Welcome  (tcp)    строка, id клиента\n
  \t\t2 spawnPlayer         SpawnPlayer(tcp)  id клиента, username, vector3 pos, quaternion rot\n
  \t\t3 gridBuilding        BuildingGrid(tcp) id клиента, длинна массива, массив int\n
  \t\t4 gridStage           StageGrid(udp)    id клиента, длинна массива, массив int\n
  \t\t5 gridCursor          CursorGrid(udp)   id клиента, длинна массива, массив int\n
