# Server
 
Сервер запускается из кода Program.cs. В Main вызов Server.Start(макс. кол-во игроков, порт).
Для оптимизации работы Unity с сервером надо синхронизировать вызовы FixedUpdate и TPS сервера. TPS записан в константах, а Time Unity должен быть 1/TPS.
Вид пакетов:
 Посылаемые на сервер:
  id пакета             метод приёма     содержание
  1 welcomeReceived     WelcomeReceived  id клиента, username
  2 playerGrid          PlayerGrid       длинна массива(2 двумерных массива), массив int(вообще их 2, но записаны подряд)
  3 cursorGrid          CursorGrid       длинна массива(двумернq массив), массив int
 Посылаемые сервером:
  id пакета             метод отправки    содержание
  1 welcome             Welcome  (tcp)    строка, id клиента
  2 spawnPlayer         SpawnPlayer(tcp)  id клиента, username, vector3 pos, quaternion rot
  3 gridBuilding        BuildingGrid(tcp) id клиента, длинна массива, массив int
  4 gridStage           StageGrid(udp)    id клиента, длинна массива, массив int
  5 gridCursor          CursorGrid(udp)   id клиента, длинна массива, массив int
