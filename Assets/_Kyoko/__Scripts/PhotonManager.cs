using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject gameView = null;
    private bool isConnecting = false;


    public void Start()
    {
        PhotonPeer.RegisterType(typeof(BallSettings), 13, BallSettings.Serialize, BallSettings.Deserialize);
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom(roomName: null, maxPlayers: 2);
    }


    public override void OnJoinedRoom()
    {
        isConnecting = false;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SetRoomReady();
        }
        else
        {
            MainMenu.Log("Waiting opponent...");
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SetRoomReady();
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        MainMenu.Log("Closing room...");
        LeaveRoom();
    }

    public void FindOpponent()
    {
        isConnecting = true;
        if (PhotonNetwork.InRoom)
        {
            Debug.LogError("InRoomAlready");
            return;
        }
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }
        MainMenu.Log("Finding opponent...");
    }
    public void SetRoomReady()
    {
        MainMenu.OnRoomReady();
        gameView.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        //Application is playing not working
        try
        {
            gameView.SetActive(false);
            MainMenu.OnLeftRoom();
        }
        catch { }
    }
    public void CreateRoom(string roomName, byte maxPlayers, bool isOpen = true, bool isVisible = true)
    {
        if (isConnecting)
        {
            RoomOptions options = new RoomOptions()
            {
                IsOpen = isOpen,
                IsVisible = isVisible,
                MaxPlayers = maxPlayers
            };
            PhotonNetwork.CreateRoom(roomName, options);
        }
    }
}
