using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Levels.DataManager;
using Photon.Pun;
using Playground.Characters.Monsters;
using UnityEngine;

namespace Levels
{
    public class SpawnPoints : MonoBehaviour
    {
        /// <summary>
        /// Store the type and the number of monsters in this spawn point
        /// can be an infinity spawn
        /// </summary>
        [Header("Monsters that will appear from this spawn point")]
        [SerializeField] protected Monster[] _monsters; // Monsters that will spawn

        [Header("This list MUST be the exact same length as the previous one")]
        [Header("Number of monster that will appear")]
        [SerializeField] protected float[] _number; // Number of monster that will spawn
        protected List<Tuple<Monster, float>> monstersList;
        /// <summary>
        /// Time between two spawns in seconds
        /// </summary>
        [Header("Time in seconds between two monsters spawns")]
        public float gapSpawn;
        private bool alreadyActivated = false;
        private bool multiplayer;

        public void Start()
        {
            monstersList = new List<Tuple<Monster, float>>();
            for (int i = 0; i < _monsters.Length; i++)
            {
                try
                {
                    monstersList.Add(new Tuple<Monster, float>(_monsters[i], _number[i]));
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.LogWarning($"The monsters list is shorter than the number list. All monsters after {_monsters[i-1]} have been ignored.");
                    break;
                }
            }
            multiplayer = FindObjectOfType<LoadLevelInfos>().multiplayer;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!alreadyActivated)
            {
                StartCoroutine(SpawnMonsters());
            }
            alreadyActivated = true;
        }

        /// <summary>
        /// Will stop make monsters spawing from that spawn point
        /// </summary>
        public void StopSpawning()
        {
            StopCoroutine(SpawnMonsters());
        }

        private IEnumerator SpawnMonsters()
        {
            foreach (Tuple<Monster, float> pair in monstersList)
            {
                float number = pair.Item2;
                
                while (number > 0)
                {
                    number--;
                    if (multiplayer)
                    {
                        PhotonNetwork.Instantiate($"Prefabs/Monsters/{pair.Item1.name}", gameObject.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(pair.Item1, gameObject.transform.position, Quaternion.identity);
                    }

                    yield return new WaitForSeconds(gapSpawn);
                }
            }
        }
    }
}