using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitZoneTrigger : MonoBehaviour
{
    public string sceneToLoad = "SampleScene"; // Ganti dengan nama scene berikutnya

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("HasReturnedFromMaze", 1);
            PlayerPrefs.Save();
            Debug.Log("FLAG DISIMPAN: HasReturnedFromMaze = 1");
            Debug.Log("Player exited maze!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
