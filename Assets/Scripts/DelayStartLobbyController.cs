
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject delayStartButton;//button used for creating and joining a game
    [SerializeField] private GameObject delayCancelButton;//button used to top searching for a game to join
    [SerializeField] private int roomSize; //manuel set the number of player in the room at one time

    public override void OnConnectedToMaster()//callback function for wwhen the first connection is estblished
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        delayStartButton.SetActive(true);
    }

    public void DelayStart()//paird to the delay start button
    {
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("delay start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();//if it fails to join a room then  it will tery to create a room
    }
    // Start is called before the first frame update
    void CreateRoom()
    {
        Debug.Log("creating room now");
        int randomRoomNumber = Random.Range(0, 10000);//crating a random name for the room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte) roomSize; //set max player
        roomOptions.IsVisible = true; // all players can see room
        roomOptions.IsOpen = true;//allow indivduals to join room after it is created{ }
        PhotonNetwork.CreateRoom("Room " + randomRoomNumber, roomOptions);
        Debug.Log(randomRoomNumber);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room...trying again");
        CreateRoom();//retrying to create a room with a different name
    }
    // Update is called once per frame
    public void DelayCancel()
    {
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();

    }
}
