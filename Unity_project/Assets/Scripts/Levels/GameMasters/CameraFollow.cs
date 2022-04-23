using System;
using UnityEngine;

namespace Playground.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] protected int limitX =0;
        [SerializeField] protected int limitY =0;
        public Transform playerT;
        private Vector3 offset;
        
        void Start ()
        {
            playerT = GameObject.FindGameObjectWithTag("Heros").transform;
        }

        private void Update()
        {
        }

        
        void LateUpdate ()
        {
            Vector3 temp = transform.position;
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
