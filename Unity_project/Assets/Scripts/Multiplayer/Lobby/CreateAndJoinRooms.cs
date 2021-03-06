using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public List<GameObject> players;

    public void CreateRoom()
    {
        if (createInput.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(createInput.text);
        }
    }

    public void JoinRoom()
    {
        if (joinInput.text.Length >=1)
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Assets/Scenes/spaceship.unity");
        //a remplacer
        if (PhotonNetwork.PlayerList.Length == 4)
        {
            foreach (var hero in GameObject.FindGameObjectsWithTag("Heros"))
            {
                players.Add(hero);
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
