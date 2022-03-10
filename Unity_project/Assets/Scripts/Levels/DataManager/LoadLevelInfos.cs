using System;
using System.Linq.Expressions;
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

        /// <summary>
        /// If true, debug options will be activated in different scripts
        /// </summary>
        [SerializeField] public bool debugOption = false;

        public bool debug => debugOption;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PersonnalDebug(string message)
        {
            Debug.Log($"<color=orange>DEBUG LOG MESSAGE:</color> {message}");
        }
    }
}