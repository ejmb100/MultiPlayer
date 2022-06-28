using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;
using System.IO;
using TMPro;



public class NetworkPlayer : MonoBehaviour //make it so head and hands are following
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    private PhotonView photonView;

    private Transform headRig;
    private Transform rightHandRig;
    private Transform leftHandRig;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    // private string userName;


    [SerializeField]
    private InputActionProperty flex;//copied from handvisual

    [SerializeField]
    private InputActionProperty point;//copied from handvisual

    //public GameObject setUsernamePanel;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        XROrigin rig = FindObjectOfType<XROrigin>();
        headRig = rig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find("Camera Offset/Left Hand");
        rightHandRig = rig.transform.Find("Camera Offset/Right Hand");

        if (photonView.IsMine)//added this bit and deactivate rightHand, lefthand, Head in update
            foreach (var item in GetComponentsInChildren<Renderer>())//disables renderer all children of this game object
            {
                item.enabled = false;
            }

        //setUsername();//or should i add this to the network manager script

    }

    //public void OnSetUserNameValueChanged(string input)
    //{
    //    print(input);

    //}
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)//to determine was spawned by me or another. if so, hide from me but keep in view for others
        {

            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);

        }

    }

    void MapPosition(Transform target, Transform rigTransform)//need namespace unityEngine.XR to access XR Node; to synchrone head with head and hands with the hand prefabs
    {
        //uses world coordinate not local
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

    //added this section  last
    void UpdateHandAnimation(UnityEngine.XR.InputDevice inputDevice, Animator handAnimator)//code from HandVisuals script which i changed method name from update to AnimatorUpdate
    {
        handAnimator.SetFloat("ControllerSelectValue", flex.action.ReadValue<float>());
        handAnimator.SetFloat("ControllerActivateValue", point.action.ReadValue<float>());
    }

    //public void setUsername()
    //{ 
    //    if(PlayerPrefs.GetString("name") == null)
    //    {
    //        setUsernamePanel.SetActive(true);
    //    }

    //    else
    //    {
    //        setUsernamePanel.SetActive(false);

    //    }

    //}

    //private void OnApplicationQuit()
    //{
    //    if ("name" != null)
    //    {
    //        PlayerPrefs.SetString(name, name);
    //        Debug.Log("setstring name executed" + name);


    //    }
    //}
}
