using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class launcher : MonoBehaviourPunCallbacks
{
    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        connect();
    }

    public void connect()
    {
        PhotonNetwork.GameVersion = "0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        join();
        base.OnConnectedToMaster();
    }

    public void join()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        startGame();
        base.OnJoinedRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        create();
        base.OnJoinRandomFailed(returnCode, message);
    }

    public void create()
    {
        PhotonNetwork.CreateRoom("placeholder");
    }

    public void startGame()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
}
