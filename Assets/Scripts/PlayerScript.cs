using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                for (int i = 1; i < 8; i++)
                {
                    SceneManager.LoadSceneAsync("Area" + i, LoadSceneMode.Additive);
                }
            }

        }
        else Destroy(gameObject);

    }
}
