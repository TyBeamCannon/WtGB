using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadNewSceneTrigger : MonoBehaviour
{
    enum SceneLoad { main, shop, cards };
    bool inTrigger;
    [SerializeField] SceneLoad sceneType;

    private void Update()
    {
        if (inTrigger && (Input.GetKey(key: KeyCode.S) || Input.GetKey(key: KeyCode.W)))
        {
            switch (sceneType)
            {
                case SceneLoad.main:
                    {
                        SceneManager.LoadScene("MainGame");
                        break;
                    }
                case SceneLoad.shop:
                    {
                        SceneManager.LoadScene("Shop");
                        break;
                    }
                case SceneLoad.cards:
                    {
                        break;
                    }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }
} 
