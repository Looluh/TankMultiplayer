using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRoom : MonoBehaviour
{
    public GameObject roomPanel;
    public PlayerName[] players;

    // Start is called before the first frame update
    void Start()
    {
        roomPanel.SetActive(true);
        players = FindObjectsOfType<PlayerName>();
        foreach(PlayerName player in players)
        {
            player.Reset();
        }
        InvokeRepeating("CheckAllReady", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckAllReady()
    {
        print("Checando");
        bool allReady = true;

        if (players.Length > 1)
        {
            allReady = players.All(x => x.ready);

            if (allReady)
            {
                print("allplayersready");
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel("Level1");
            }
        }
    }

}
