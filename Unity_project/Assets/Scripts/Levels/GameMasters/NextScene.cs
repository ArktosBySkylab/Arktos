using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class NextScene : MonoBehaviour
    {
        public int index;

        void OnTriggerEnter2D(Collider2D obj)
        {

            SceneManager.LoadSceneAsync(index);
            
        }

    }
}



