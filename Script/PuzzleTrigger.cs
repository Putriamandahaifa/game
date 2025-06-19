using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//public class ToJigsawScene : MonoBehaviour
//{
//    public string puzzleSceneName = "JigsawScene"; // nama scene puzzle

//    private bool playerNearby = false;

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (playerNearby && Input.GetKeyDown(KeyCode.E))
//        {
//            SceneManager.LoadScene(puzzleSceneName);
//        }
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            playerNearby = true;
//            Debug.Log("Tekan E untuk main puzzle");
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            playerNearby = false;
//        }
//    }
//}

public class PuzzleTrigger : MonoBehaviour
{
    [Header("Nama Scene yang Akan Dimuat")]
    public string sceneToLoad = "MazeScene";

    [Header("UI Prompt (opsional)")]
    public GameObject interactionUI; // Panel/teks seperti "Tekan E untuk masuk"

    private bool playerNearby = false;

    void Start()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (interactionUI != null)
            {
                interactionUI.SetActive(true);
            }

            Debug.Log("Player dekat: Tekan E untuk masuk ke " + sceneToLoad);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }
        }
    }
}
