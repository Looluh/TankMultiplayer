using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rdb;
    public PhotonView pview;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rdb.AddForce(transform.forward * 100, ForceMode.Impulse);
        if(pview.IsMine)
        Invoke("SelfDestruct", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SelfDestruct()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 20, Vector3.up);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
               //hit.collider.gameObject.
                hit.collider.gameObject.SendMessage("DamageTaken", transform.position);

            }
        }

        Instantiate(explosion, transform.position, Quaternion.identity);

        if(pview.IsMine)
        PhotonNetwork.Destroy(gameObject);
    }
}
