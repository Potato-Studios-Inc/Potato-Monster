using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Scripts
{
    public class ChangeScene : MonoBehaviour
    {
        public String sceneName;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}