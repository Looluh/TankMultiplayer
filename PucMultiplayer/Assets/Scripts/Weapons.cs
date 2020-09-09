using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public PhotonView pview;
    public GameObject firePoint;
    float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pview.IsMine)
        {
            cooldown -= Time.deltaTime;
            if (Input.GetButtonDown("Jump") && cooldown < 0)
            {
                GameObject ob = (GameObject)
                     PhotonNetwork.Instantiate("Bullet", firePoint.transform.position,
                     firePoint.transform.rotation, 0);
                cooldown = 3;
            }
        }
    }
}
