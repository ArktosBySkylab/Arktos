using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{

    public class GameMaster : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> players;
        [SerializeField] protected (int, int) startCoords = (0, 0);

        public void Start()
        {
            Debug.Log("COUCOU C'EST MOI TCHOUPI");
            foreach (GameObject player in players)
            {
                Instantiate(player, new Vector3(startCoords.Item1, startCoords.Item2), Quaternion.identity).SetActive(true);
            }
        }


        #region Pause Menu

        [SerializeField] protected GameObject PauseMenu;
        [SerializeField] protected GameObject MainMenu;
        [SerializeField] protected GameObject RestartButton;
        [SerializeField] protected GameObject startButton;
        [SerializeField] protected static bool Gamelaunched = true;
        private bool IsGamePaused = false;

        protected void Resume()
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            IsGamePaused = false;
        }

        protected void Pause()
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            IsGamePaused = true;
        }

        public void RestartLevel() //Restarts the level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void PlayCampain()
        {
            //SceneManager.LoadScene("Campain");
            MainMenu.SetActive(false);
            PauseMenu.SetActive(false);
            Gamelaunched = true;
        }

        public void closegame() //ne marche pas sur Unity (uniquement quand le jeu et lancé)
        {
            Application.Quit();
        }

        public void DisplayMainMenu()
        {
            MainMenu.SetActive(true);
            startButton.SetActive(!Gamelaunched);
            RestartButton.SetActive(Gamelaunched);
        }

        #endregion


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsGamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
}