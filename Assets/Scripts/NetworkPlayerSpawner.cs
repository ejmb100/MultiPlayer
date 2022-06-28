using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject spawnedPlayerPrefab;
    public override void OnJoinedRoom()//method called when we join a room
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate(spawnedPlayerPrefab.name, 
            transform.position, transform.rotation);//instantiate Network Player prefab. is the name of the game object  so make sure it matches
        Debug.Log("name of spawned object name " + spawnedPlayerPrefab.name); //changed from "Network Player" to spawnedPlayerPrefab.name
        Debug.Log("player joined room: " + PhotonNetwork.CurrentRoom.Players.Count);
    }

    public override void OnLeftRoom()//method called when we leave a room
    {
        base.OnLeftRoom();
        Debug.Log("player left Room");
        //PhotonNetwork.Destroy(spawnedPlayerPrefab);//destroy on server
       

    }
}
