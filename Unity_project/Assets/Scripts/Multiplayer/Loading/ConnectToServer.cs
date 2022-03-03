using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using  UnityEngine.SceneManagement;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // add timelapse for no connection 
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    
    void Update()
    {
        
    }
}
