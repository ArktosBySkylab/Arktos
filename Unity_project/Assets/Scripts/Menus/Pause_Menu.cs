using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{

    public class change_menu : MonoBehaviour
    {
        public static bool Gamelaunched;

        public GameObject PauseMenu;
        public GameObject MainMenu;
        public GameObject RestartButton;
        public GameObject startButton;

        public void Resume()
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        public void Pause()
        {
            PauseMenu.SetActive(true);
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
    }
}