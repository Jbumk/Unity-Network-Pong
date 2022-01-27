using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;
    
    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "Connecting To Master Server...";
    }
    
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "Online : Connected To Master Server"; 
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()}";

        PhotonNetwork.ConnectUsingSettings();
        //재접속
    }
    
    public void Connect()
    {
        joinButton.interactable = false;
        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connecting to Random Room...";
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            connectionInfoText.text = "Offline : Connection Disabled - Try Reconnecting";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "There is no empty room, Create New Room";
        PhotonNetwork.CreateRoom(null,new RoomOptions { MaxPlayers = 2});

    }
    
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Connected with Room";
        PhotonNetwork.LoadLevel("Main");
    }
}