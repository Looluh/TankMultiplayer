using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public TMP_Text nameText;
    public PhotonView pview;
    public Toggle readyToggle;
    public bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = pview.Owner.NickName;

        GameObject content = GameObject.FindGameObjectWithTag("Playerlist");

        if (content)
            transform.parent = content.transform;

        if (pview.IsMine)
        {
            readyToggle.interactable = true;
        }
    }

    public void Reset()
    {
        nameText.text = pview.Owner.NickName;
        GameObject content = GameObject.FindGameObjectWithTag("Playerlist");
        transform.parent = content.transform;
        readyToggle.isOn = false;
        if (pview.IsMine)
        {
            readyToggle.interactable = true;
        }
        else
        {
            readyToggle.interactable = false;
        }

    }

    public void ReadyChange()
    {
        //readyToggle.isOn = !readyToggle.isOn;
        if (ready != readyToggle.isOn)
        {
            ready = readyToggle.isOn;
            pview.RPC("StatusChanged", RpcTarget.OthersBuffered, ready);
        }
    }
    [PunRPC]
    void StatusChanged(bool myReady)
    {
        readyToggle.isOn = myReady;
        ready = myReady;
    }


}
