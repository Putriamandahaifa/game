using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEverCome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteKey("HasReturnedFromJigsaw");
            PlayerPrefs.Save();
            Debug.Log("🔄 Flag HasReturnedFromJigsaw telah dihapus!");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteKey("HasReturnedFromMaze");
            PlayerPrefs.Save();
            Debug.Log("🔄 Flag HasReturnedFromMaze telah dihapus!");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayerPrefs.DeleteKey("HasReturnedFromRoll");
            PlayerPrefs.Save();
            Debug.Log("🔄 Flag HasReturnedFromRoll telah dihapus!");
        }
    }
}
