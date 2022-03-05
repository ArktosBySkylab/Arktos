using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool Gamelaunched;
        public static bool IsGamePaused = false;

        public GameObject pauseMenu;
        public GameObject MainMenu;
        public GameObject RestartButton;
        public GameObject startButton;

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
        
        
        public void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        public void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void RestartLevel() //Restarts the level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void PlayCampain()
        {
            //SceneManager.LoadScene("Campain");
            MainMenu.SetActive(false);
            pauseMenu.SetActive(false);
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
    }
}