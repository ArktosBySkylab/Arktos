using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    public float Xpos;
    public float Ypos;

    private void Start()
    {
        Vector2 SpawnPos = new Vector2(Xpos, Ypos);
        PhotonNetwork.Instantiate(playerPrefab.name, SpawnPos, Quaternion.identity);
    }
}
