using System;
using System.Linq.Expressions;
using Playground.Characters.Heros;
using Playground.Weapons;
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

        public WeaponsNames firstHand;
        public WeaponsNames secondHand;

        /// <summary>
        /// If true, debug options will be activated in different scripts
        /// </summary>
        [Header("See GameMaster.cs for further informations")]
        [Header("The debug option will override every options of the data manager")]
        [SerializeField] public bool debugOption = false;

        public bool debug => debugOption;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}