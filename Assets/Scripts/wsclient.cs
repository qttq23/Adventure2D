using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;


public class wsclient : MonoBehaviour
{

    // public bool isHost = false;
    // public GameManagerOnline gameManager;

    // WebSocket ws;
    // List<CharacterInfo> listCharacters;
    // string nameOfOtherPlayerCharacter;

    // void Start()
    // {
    //     // connect to room & get info: other player's character?
    //     ws = new WebSocket("ws://localhost:8080");


    //     ws.OnMessage += (sender, e) =>
    //     {
    //         // Console.WriteLine("Laputa says: " + e.Data);
    //         print("myws.cs: message: " + e.Data);
    //     };

    //     ws.Connect();
    //     ws.Send("BALUS2g");



    //     if (isHost)
    //     {

    //         // game starts like normal, create dump player1
    //         gameManager.DeleteDefaultCharacters();
    //         gameManager.CreatePlayer();
    //         gameManager.CreateEnemies();

    //         gameManager.createDumpPlayer(nameOfOtherPlayerCharacter);

    //         // listen to all actions from: player host, enemies
    //         // send actions to other player
    //         listCharacters = gameManager.getListCharacters();
    //         foreach (var character in listCharacters)
    //         {
    //             var controller = character.myInstance.GetComponent<MyCharacterController>();
    //             controller.EventMove += SendMove;
    //         }


    //         // receive actions from: player 1
    //         // apply action to dump player1
    //         ws.OnMessage += (sender, e) =>
    //         {

    //             MyMessage message = JsonUtility.FromJson<MyMessage>(e.Data);
    //             if (message.from == "player1")
    //             {
    //                 applyMove(message.characterId, message.characterMoveType);
    //             }

    //         };

    //     }
    //     else
    //     {

    //         // game start, except player, enemies will be disabled and controlled by others in host
    //         // also create dump player host
    //         gameManager.DeleteDefaultCharacters();
    //         gameManager.CreatePlayer();
    //         gameManager.CreateEnemies(false);

    //         gameManager.createDumpPlayer(nameOfOtherPlayerCharacter);

    //         // listen to all actions from only player
    //         // send to host
    //         listCharacters = gameManager.getListCharacters();
    //         var controller = listCharacters[0].myInstance.GetComponent<MyCharacterController>();
    //         controller.EventMove += SendMove;


    //         // receive actions from host: player host & enemies
    //         // pass action to dump player host, enemies
    //         ws.OnMessage += (sender, e) =>
    //         {

    //             MyMessage message = JsonUtility.FromJson<MyMessage>(e.Data);
    //             if (message.from == "host")
    //             {
    //                 applyMove(message.characterId, message.characterMoveType);
    //             }

    //         };

    //     }

    // }

    // void SendMove(int id, int moveType)
    // {

    // }

    // void applyMove(int id, int moveType)
    // {

    // }




}


// class MyMessage
// {

//     string from;
//     string to;
//     string roomId;

//     int characterId;
//     int characterMoveType;
// }
