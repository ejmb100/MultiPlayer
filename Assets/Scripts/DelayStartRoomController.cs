using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{

    [SerializeField] private int waitingRoomSceneIndex;

    // Start is called before the first frame update
   public override void OnEnable()
    {
        //register to photon callback functions
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        //register to photon callback functions
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()//callback function for when we successfully create  or join a room
    {
        //called when our player joins the room
        //load into waiting room scene
        SceneManager.LoadScene(waitingRoomSceneIndex);

    }
}
