using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 ajust;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.LookAt(player.transform);
            transform.position = Vector3.Lerp(transform.position, player.transform.position - player.transform.forward * ajust.z + Vector3.up*ajust.y, Time.deltaTime);
        }
    }

    public void SetPlayer(GameObject myplayer)
    {
        player = myplayer;
    }
}
