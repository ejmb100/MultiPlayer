using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using Photon.Pun;

public class XRGrabNetworkInteractable : XRGrabInteractable
{
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame


    void OnSelectEnter(XRBaseInteractor interactor)//check with Mondae's script did he have protected override 
    {
        photonView.RequestOwnership();
        //base.OnSelectEnter(interactor);
    }
    
}
