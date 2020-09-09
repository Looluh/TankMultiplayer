using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class TankID : MonoBehaviour
{
    public TMP_Text name;
    public PhotonView pview;
    public int hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        name.text = pview.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        //name.transform.LookAt(Camera.main.transform);
        name.transform.forward = transform.position - Camera.main.transform.position;
        if (pview.IsMine)
        {
            if (hp <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }

        }

    }

    public void DamageTaken(Vector3 pos)
    {
        if (pview.IsMine)
        {

            float distance = Vector3.Distance(pos, transform.position);
            hp -= 10;

            pview.RPC("DamageCall", RpcTarget.All, pos, hp);
            GetComponent<Rigidbody>().AddExplosionForce(1000000, pos, 20);
        }
    }

    [PunRPC]
    void DamageCall(Vector3 pos, int hpRemain)
    {
        hp = hpRemain;
        name.text = pview.Owner.NickName + " " + hp.ToString();
    }
}
