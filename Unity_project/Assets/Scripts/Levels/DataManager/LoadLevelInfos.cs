using System;
using Playground.Characters.Heros;
using UnityEngine;

namespace Levels.DataManager
{
    public class LoadLevelInfos : MonoBehaviour
    {
        /// <summary>
        /// Store here if the current mode is multiplayer or campaign
        /// </summary>
        public bool multiplayer = false;
        
        /// <summary>
        /// Store here the name of the chosen hero to play
        /// </summary>
        public HerosNames hero;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}