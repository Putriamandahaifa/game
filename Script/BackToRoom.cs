using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public string mainSceneName = "SampleScene"; // ganti sesuai nama scene utama kamu

    public void GoBack()
    {
        PlayerPrefs.SetInt("HasReturnedFromJigsaw", 1);
        PlayerPrefs.Save();
        Debug.Log("FLAG DISIMPAN: HasReturnedFromJigsaw = 1");
        SceneManager.LoadScene(mainSceneName);
    }
}
