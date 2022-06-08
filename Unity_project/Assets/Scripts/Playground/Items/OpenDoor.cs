using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Playground.Items
{
    public class OpenDoor : MonoBehaviour
    {
        private GameObject Door;
        private bool IsOpen = true;
        
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
                if (!elem.CompareTag("Heros")) continue;
                nbr_hero += 1;
            }

            Debug.Log("nbr hero:"+nbr_hero);
            if (nbr_hero == GameObject.FindGameObjectsWithTag("Heros").Length)
            {
                if (IsOpen)
                {
                    Destroy(Door);
                }
                
            }
        }
    }
}