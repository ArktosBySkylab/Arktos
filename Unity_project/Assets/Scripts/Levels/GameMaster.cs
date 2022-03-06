using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{

    public class GameMaster : MonoBehaviour
    {
        //[SerializeField] protected List<GameObject> players;
        protected (int, int) startCoords = (0, 0);

        public void Start()
        {
            Debug.Log("COUCOU C'EST MOI TCHOUPI");
            //foreach (GameObject player in players)
            //{
            //    Instantiate(player, new Vector3(startCoords.Item1, startCoords.Item2), Quaternion.identity).SetActive(true);
            //}
            
            // TEMPORARY PART (pour la premiere soutenance uniquement)
            //GameObject character = Resources.Load<GameObject>("Kitsune");
            //Instantiate(character, new Vector3(startCoords.Item1, startCoords.Item2), Quaternion.identity).SetActive(true);
        }
    }
}