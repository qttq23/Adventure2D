using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;


public class myws : MonoBehaviour
{

    public GameObject player;
    public GameObject otherPrefab;

    public MyConfig config;
    public GameObject enemyParent;
    List<GameObject> enemies;

    WebSocket ws;
    MyCharacterController controller;
    MyCharacterController otherController;
    List<IdPreviousMove> previousMoves;
    MyMessage messageInstantiate;
    MyMessage message;
    string eventName;
    bool isInstantiateDone = false;

    // Start is called before the first frame update
    void Start()
    {
        // ignore coin with hero, enemies
        Physics2D.IgnoreLayerCollision(9, 8, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);
        // two players/enemies can go through each other
        Physics2D.IgnoreLayerCollision(8, 8, true);
        Physics2D.IgnoreLayerCollision(11, 11, true);


        // init list previous moves
        previousMoves = new List<IdPreviousMove>(new IdPreviousMove[100]);
        previousMoves[99] = new IdPreviousMove(99, -8888, null);
        previousMoves[90] = new IdPreviousMove(90, -8888, null);
        StartCoroutine(checkAndApplyMove(previousMoves[99]));
        StartCoroutine(checkAndApplyMove(previousMoves[90]));

        // get list enemies from gameobject parent
        enemies = new List<GameObject>();
        foreach (Transform enemyTransform in enemyParent.transform)
        {
            enemies.Add(enemyTransform.gameObject);
        }
        print("myws.cs: enemy count: " + enemies.Count);
        foreach (var enemy in enemies)
        {
            var eController = enemy.GetComponent<MyCharacterController>();
            int eid = eController.Id;
            previousMoves[eid] = new IdPreviousMove(eid, -8888, eController);
            StartCoroutine(checkAndApplyMove(previousMoves[eid]));

        }

        controller = player.GetComponent<MyCharacterController>();


        // check if play offline
        if (!config.isPlayOnline)
        {
            print("myws.cs: play offline");
            player.GetComponent<MyCharacterController>().isUsedByRemote = false;
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<MyCharacterController>().isUsedByRemote = false;
            }

            return;
        }


        print("myws.cs: play online");

        // create/join room
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();

        MyMessage messageInit;
        if (config.isHost)
        {
            print("myws.cs: isHost");
            messageInit = new MyMessage();
            messageInit.eventName = MyMessageEventName.create;
            messageInit.roomName = config.roomName;
        }
        else
        {
            print("myws.cs: isclient");
            messageInit = new MyMessage();
            messageInit.eventName = MyMessageEventName.join;
            messageInit.roomName = config.roomName;
        }
        ws.Send(JsonUtility.ToJson(messageInit));

        print("myws.cs: waiting events...");

        // events
        ws.OnMessage += (sender, e) =>
        {
            message = JsonUtility.FromJson<MyMessage>(e.Data);
            if (message.eventName == MyMessageEventName.newMember && config.isHost)
            {
                print("myws.cs: event: newMember");
                eventName = message.eventName;
            }

            else if (message.eventName == MyMessageEventName.instantiate)
            {
                print("myws.cs: event: instantiate");

                // occur both client & host
                eventName = message.eventName;
                messageInstantiate = (MyMessage)message.Clone();

                // isInstantiate = true;
            }

            else if (message.eventName == MyMessageEventName.instantiateDone
                && !config.isHost)
            {
                print("myws.cs: event: instantiateDone");
                isInstantiateDone = true;
                // eventName = message.eventName;
            }

            else if (message.eventName == MyMessageEventName.move)
            {
                var msgTemp = JsonUtility.FromJson<MyMessage>(e.Data);
                previousMoves[msgTemp.characterId].moveList.Add(
                    new MoveTypePosition(msgTemp.characterMoveType, msgTemp.posX, msgTemp.posY)
                     );


                print("myws.cs: event: move: " + msgTemp.characterId + ", " + msgTemp.characterMoveType);
                // eventName = message.eventName;
            }



        };




    }

    void Update()
    {


        if (eventName == MyMessageEventName.newMember)
        {
            eventName = "";

            try
            {
                // register event local player, enemy move

                controller.Id = 99;
                previousMoves[controller.Id].controller = controller;

                controller.EventMove += sendMove;
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<MyCharacterController>().EventMove += sendMove;
                }
            }
            catch (Exception ex)
            {
                print(ex);
            }


            // signal client about instantiate host player
            MyMessage msg = new MyMessage();
            msg.eventName = MyMessageEventName.instantiate;
            msg.posX = controller.gameObject.transform.position.x;
            msg.posY = controller.gameObject.transform.position.y;
            msg.posZ = controller.gameObject.transform.position.z;
            msg.rotateX = controller.gameObject.transform.localRotation.x;
            msg.rotateY = controller.gameObject.transform.localRotation.y;
            msg.rotateZ = controller.gameObject.transform.localRotation.z;
            msg.rotateW = controller.gameObject.transform.localRotation.w;
            ws.Send(JsonUtility.ToJson(msg));

        }
        else if (eventName == MyMessageEventName.instantiate)
        {
            eventName = "";

            print("myws.cs: update: isInstantiate");

            var dumpPlayer = Instantiate(
                                otherPrefab,
                                new Vector3(messageInstantiate.posX,
                                    messageInstantiate.posY, messageInstantiate.posZ),
                                new Quaternion(messageInstantiate.rotateX, messageInstantiate.rotateY,
                                    messageInstantiate.rotateZ, messageInstantiate.rotateW)
                                );
            otherController = dumpPlayer.GetComponent<MyCharacterController>();
            otherController.isUsedByRemote = true;
            print("myws.cs: done create other: " + dumpPlayer);


            // client tells host to create dump player
            if (!config.isHost)
            {
                print("myws.cs: isclient: send player info");

                otherController.Id = 99;
                previousMoves[otherController.Id].controller = otherController;

                MyMessage msg = new MyMessage();
                msg.eventName = MyMessageEventName.instantiate;
                msg.posX = controller.gameObject.transform.position.x;
                msg.posY = controller.gameObject.transform.position.y;
                msg.posZ = controller.gameObject.transform.position.z;
                msg.rotateX = controller.gameObject.transform.localRotation.x;
                msg.rotateY = controller.gameObject.transform.localRotation.y;
                msg.rotateZ = controller.gameObject.transform.localRotation.z;
                msg.rotateW = controller.gameObject.transform.localRotation.w;
                ws.Send(JsonUtility.ToJson(msg));

                // allow local player moves and send to host
                controller.Id = 90;
                previousMoves[controller.Id].controller = controller;

                controller.EventMove += sendMove;
            }

            // host knows client create done, let host player and anemies move
            else
            {
                print("myws.cs: ishost: allow local move");

                otherController.Id = 90;
                previousMoves[otherController.Id].controller = otherController;

                // allow local player and enemies move
                controller.isUsedByRemote = false;
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<MyCharacterController>().isUsedByRemote = false;
                }

                print("myws.cs: ishost: sinal other to move");

                // sinal client that create dump player done
                MyMessage msg = new MyMessage();
                msg.eventName = MyMessageEventName.instantiateDone;
                ws.Send(JsonUtility.ToJson(msg));

            }
        }



        if (isInstantiateDone)
        {
            isInstantiateDone = false;

            controller.isUsedByRemote = false;
            print("myws.cs: allowed player to move ");
        }


    }



    void sendMove(int id, int moveType, Vector2 position)
    {

        // // find id in previous moves


        // check if same move as previous
        if (previousMoves[id].previousMoveType == moveType)
        {
            return;
        }

        // send
        previousMoves[id].previousMoveType = moveType;

        MyMessage message = new MyMessage();
        message.eventName = MyMessageEventName.move;
        message.characterId = id;
        message.characterMoveType = moveType;

        // send position when idle
        message.posX = position.x;
        message.posY = position.y;

        ws.Send(JsonUtility.ToJson(message));
        print("myws.cs: ishost: " + config.isHost + " sendMove: " + id + "," + moveType);




    }

    IEnumerator checkAndApplyMove(IdPreviousMove item)
    {

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            // check if has new move
            if (item.appliedIndex < item.moveList.Count - 1)
            {
                var temp = item.moveList[item.appliedIndex + 1];
                item.controller.ApplyMove(temp.moveType, new Vector3(temp.posX, temp.posY, 0));
                item.appliedIndex = item.appliedIndex + 1;
            }

        }

    }


}

class MyMessage : ICloneable
{
    public string eventName;  // create, join, move, instantiate
    public string roomName;

    public int characterId;
    public int characterMoveType;

    public float posX;
    public float posY;
    public float posZ;
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    public float rotateW;

    public object Clone()
    {
        return this.MemberwiseClone();
    }


}

class MyMessageEventName
{
    public static string create = "create";
    public static string join = "join";
    public static string newMember = "newMember";
    public static string instantiate = "instantiate";
    public static string instantiateDone = "instantiateDone";
    public static string move = "move";
}


class IdPreviousMove
{
    public int id;
    public int previousMoveType;

    public int appliedIndex;
    public List<MoveTypePosition> moveList;
    public MyCharacterController controller;

    public float posX;
    public float posY;

    public IdPreviousMove(int id, int moveType, MyCharacterController controller)
    {
        this.id = id;
        this.previousMoveType = moveType;

        this.appliedIndex = -1;
        this.moveList = new List<MoveTypePosition>();
        this.controller = controller;
    }

}

class MoveTypePosition
{
    public int moveType;
    public float posX;
    public float posY;

    public MoveTypePosition(int moveType, float posX, float posY){
    	this.moveType = moveType;
    	this.posX = posX;
    	this.posY = posY;
    }
}

