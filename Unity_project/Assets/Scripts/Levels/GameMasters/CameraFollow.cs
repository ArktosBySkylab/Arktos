using System;
using Levels;
using Levels.DataManager;
using Photon.Pun;
using UnityEngine;

namespace Playground.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] protected float limitX =0f;
        [SerializeField] protected float limitY =0f;
        private Transform playerT;
        private Vector3 offset;
        
        void Start ()
        {
            LoadLevelInfos infos = FindObjectOfType<LoadLevelInfos>();
            if (infos.multiplayer && NextScene.Players.Count>0)
                playerT = NextScene.Players[PhotonNetwork.CurrentRoom.PlayerCount-1].transform;
            else
                playerT = GameObject.FindGameObjectWithTag("Heros").GetComponent<Transform>();

        }

        private void Update()
        {
        }

        
        void LateUpdate ()
        {
            Vector3 temp = transform.position;
            
            LoadLevelInfos infos = FindObjectOfType<LoadLevelInfos>();
            if (infos.multiplayer && NextScene.Players.Count>0)
                playerT = NextScene.Players[PhotonNetwork.CurrentRoom.PlayerCount-1].transform;
            else
                playerT = GameObject.FindGameObjectWithTag("Heros").GetComponent<Transform>();
            
            
            if (playerT.position.x > -limitX && playerT.position.x < limitX)
                temp.x = playerT.position.x;
            else
                temp.x = playerT.position.x > 0 ? limitX : -limitX;
            if (playerT.position.y > -limitY && playerT.position.y < limitY)
                temp.y = playerT.position.y;
            else
                temp.y = playerT.position.y > 0 ? limitY : -limitY;
            temp.z = playerT.position.z - 15;
            transform.position = temp;
        }
    }
    
}