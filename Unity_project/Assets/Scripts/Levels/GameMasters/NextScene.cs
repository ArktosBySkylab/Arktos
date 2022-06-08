using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class NextScene : MonoBehaviour
    {
        public Animator transition;
        public string scene;

        void OnTriggerEnter2D(Collider2D obj)
        {
            if (obj.CompareTag("Heros"))
            {
                if (Physics2D.gravity.y > 0)
                    Physics2D.gravity = -Physics2D.gravity;
                Debug.Log(SceneManager.GetActiveScene().buildIndex + 1 );
                Debug.Log(PlayerPrefs.GetInt("reachedLevel", 0));
                if (SceneManager.GetActiveScene().buildIndex + 1 > PlayerPrefs.GetInt("reachedLevel", 0))
                {
                    Debug.Log("new level");
                    PlayerPrefs.SetInt("reachedLevel", SceneManager.GetActiveScene().buildIndex + 1 );
                }
                StartCoroutine(LoadLevel(scene));
            }
        }

        IEnumerator LoadLevel(string sceneName)//utilisé pour  faire la transition
        {
            transition.SetTrigger("start");
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(sceneName);
        }
    }
}



