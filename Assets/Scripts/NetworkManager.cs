using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using TMPro;

//add libraries Photon.Pun, Photon.Realtime

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int sceneIndex;
    public int maxPlayer;
}
public class NetworkManager : MonoBehaviourPunCallbacks //monoBehaviorPunCallbacks be able to override initial function when someone joins server, room
{
    private bool nameInputed;

    [SerializeField] private TextMeshProUGUI m_nameText;
   
    private string m_name;

    public GameObject nameInputPanel;
    public GameObject welcomeBackPanel;

    public List<DefaultRoom> defaultRooms;
    // Start is called before the first frame update
    public GameObject roomUI;

    public void ConnectToServer()// to access from outside this script
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connecting to Server");
    }


    //public override void OnConnectedToMaster()//
    //{
    //    Debug.Log("Connected to Server");//confirm connection
    //    base.OnConnectedToMaster();
    //    PhotonNetwork.JoinLobby();//special room to look at others

    //    if (PlayerPrefs.GetString("username") == null)
    //    {
    //        nameInputPanel.SetActive(true);
    //        //welcomeBackPanel.SetActive(false);

    //    }

    //    else
    //    {//welcome back comes on despite not string in username and no legnth.  I only want this to populate if a name had been set in player  prefs
    //        nameInputPanel.SetActive(false);

    //        if (nameInputed == true)
    //        {
    //            welcomeBackPanel.SetActive(true);
    //            Debug.Log("length of m_name" + m_name.Length);

    //            m_name = PlayerPrefs.GetString("username");

    //            Debug.Log(nameInputed);

    //            welcomeBackPanel.GetComponent<TextMeshProUGUI>().text = "Welcome Back " + m_name;
    //        }
    //    }
    //}

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("connectedd to master joining random room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("random room failed to join random room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("we joined lobby");
        roomUI.SetActive(true);
       
    }

    public override void OnJoinedRoom()//alert when we joined the room
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public void OnSetUserNameValueChanged(string input)
    {
        print(input);
        PlayerPrefs.SetString("username", input);
        nameInputed = true;
        Debug.Log("username is " + input);

    }

    //private void onapplicationquit()
    //{
    //    if ("username" != null)
    //    {
    //        PlayerPrefs.SetString(name, input);
    //        Debug.Log("savedString" + name);

    //    }
    //}
    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];//brings in roomindex

        //Load scene
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);//to load the scene with particular settings
        

        //Create the room
        RoomOptions roomOptions = new RoomOptions(); //create new room options to set parameters below
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer; //set max player
        roomOptions.IsVisible = true; // all players can see room
        roomOptions.IsOpen = true;//allow indivduals to join room after it is created

        //to share data with another  player  must  be in the same room
        PhotonNetwork.JoinOrCreateRoom(roomSettings.Name, roomOptions, TypedLobby.Default);
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)//alert when someone new joined the room
    {
        Debug.Log("New Player has joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }


}
