# Server
 
Сервер запускается из кода Program.cs. В Main вызов Server.Start(макс. кол-во игроков, порт).<br/>
Для оптимизации работы Unity с сервером надо синхронизировать вызовы FixedUpdate и TPS сервера. TPS записан в константах, а Time Unity должен быть 1/TPS.<br/>
###Вид пакетов:###<br/>
 Посылаемые на сервер:<br/>
  id пакета             метод приёма     содержание<br/>
  1 welcomeReceived     WelcomeReceived  id клиента, username<br/>
  2 playerGrid          PlayerGrid       длинна массива(2 двумерных массива), массив int(вообще их 2, но записаны подряд)<br/>
  3 cursorGrid          CursorGrid       длинна массива(двумернq массив), массив int<br/>
 Посылаемые сервером:<br/>
  id пакета             метод отправки    содержание<br/>
  1 welcome             Welcome  (tcp)    строка, id клиента<br/>
  2 spawnPlayer         SpawnPlayer(tcp)  id клиента, username, vector3 pos, quaternion rot<br/>
  3 gridBuilding        BuildingGrid(tcp) id клиента, длинна массива, массив int<br/>
  4 gridStage           StageGrid(udp)    id клиента, длинна массива, массив int<br/>
  5 gridCursor          CursorGrid(udp)   id клиента, длинна массива, массив int<br/>
