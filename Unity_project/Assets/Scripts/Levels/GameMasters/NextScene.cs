using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class NextScene : MonoBehaviour
    {
        public string scene;

        void OnTriggerEnter2D(Collider2D obj)
        {
            if (obj.CompareTag("Heros"))
            {
                if (Physics2D.gravity.y > 0)
                    Physics2D.gravity = -Physics2D.gravity;

                SceneManager.LoadScene(scene);
            }
        }
    }
}



