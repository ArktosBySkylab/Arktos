using System;
using System.Collections;
using Levels;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Playground.Items
{
    public class OpenDoor : MonoBehaviour
    {
        private GameObject Door;
        private bool IsOpen = true;
        private bool LightBall = false;
        
        public void Awake()
        {
            Door = GameObject.FindGameObjectWithTag("Door");
        }


        public void Update()
        {
            Collider2D[] detectHeros= Physics2D.OverlapCircleAll(Door.transform.position, 5f);
            int nbr_hero = 0;
            foreach (var elem in detectHeros)
            {
                if (elem.CompareTag("Heros"))
                    nbr_hero += 1;
            }

            if (nbr_hero == GameObject.FindGameObjectsWithTag("Heros").Length)
            {
                if (IsOpen && GameObject.FindGameObjectsWithTag("BossMonster").Length == 0 && LightBall)
                {
                    Destroy(Door);
                    foreach (GameObject o in GameObject.FindGameObjectsWithTag("Light"))
                    {
                        Destroy(o);
                    }
                }
            }
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Light"))
                GameObject.FindObjectOfType<GameMaster>().CanPassToNextLevel = true;
        }
        
        public void OnTriggerStay2D(Collider2D col)
        {
            if (col.CompareTag("Light"))
                GameObject.FindObjectOfType<GameMaster>().CanPassToNextLevel = true;
        }
    }
}