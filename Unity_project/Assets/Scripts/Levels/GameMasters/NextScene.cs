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
            SceneManager.LoadScene(scene);
        }

    }
}



