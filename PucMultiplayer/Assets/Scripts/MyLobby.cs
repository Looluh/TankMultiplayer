using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyLobby : MonoBehaviourPunCallbacks
{
    public string PlayerName;
    public GameObject roomPanel;
    public InputField ifieldName;
    public GameObject requiredText;
    public PlayerName[] playerNames;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        if (ifieldName.text.Length > 0)
        {
            PlayerName = ifieldName.text;
            PhotonNetwork.LocalPlayer.NickName = PlayerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            requiredText.SetActive(true);
        }
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        roomPanel.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Não encontrou, criando...");
        string roomName = "Sala00";
        RoomOptions rOp = new RoomOptions();
        rOp.MaxPlayers = 32;
        PhotonNetwork.CreateRoom(roomName, rOp);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("PlayerName", Vector3.zero, Quaternion.identity);

        InvokeRepeating("CheckAllReady", 1, 1);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("O jogador" + newPlayer.NickName + "entrou na sala.");

    }

    void CheckAllReady()
    {
        print("Checando");
        playerNames = FindObjectsOfType<PlayerName>();
        bool allReady = true;

        if (playerNames.Length > 1)
        {
            allReady = playerNames.All(x => x.ready);

            if (allReady)
            {
                print("allplayersready");
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel("Level1");
            }
        }
    }


}
