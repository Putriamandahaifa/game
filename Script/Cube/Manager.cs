using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubePuzzleManager : MonoBehaviour
{
    public List<string> correctOrder = new List<string> { "5", "3", "2", "4", "1" };
    private List<string> clickedOrder = new List<string>();

    public GameObject clearCube;
    public GameObject darkCube;
    public GameObject Quad;
    public GameObject finalPanel;
    public TMP_Text finalText;
    public TMP_Text Hint;

    private bool isCorrect = false;
    private bool resultReady = false;
    private bool cubeClicked = false;

    void Start()
    {
        clearCube.SetActive(false);
        darkCube.SetActive(false);
        finalPanel.SetActive(false);
    }

    public void RegisterClick(string quadID)
    {
        if (clickedOrder.Contains(quadID)) return;

        clickedOrder.Add(quadID);
        Debug.Log($" Klik terdaftar: {quadID}");

        if (clickedOrder.Count == correctOrder.Count)
        {
            Debug.Log("Semua quad diklik, memeriksa urutan...");
            CheckResult();
        }
    }

    private void CheckResult()
    {
        isCorrect = true;
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (clickedOrder[i] != correctOrder[i])
            {
                isCorrect = false;
                break;
            }
        }

        // Nonaktifkan semua quad (pivot)
        foreach (StandOnClick pivot in FindObjectsOfType<StandOnClick>())
        {
            pivot.gameObject.SetActive(false);
        }
        Quad.SetActive(false);
        Hint.text = "";
        // Munculkan cube
        if (isCorrect)
            darkCube.SetActive(true);
        else
            clearCube.SetActive(true);

        resultReady = true;
    }

    void Update()
    {
        if (!resultReady) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == darkCube || hit.collider.gameObject == clearCube)
                {
                    darkCube.SetActive(false);
                    clearCube.SetActive(false);
                    
                    finalPanel.SetActive(true);
                    finalText.text = isCorrect
                        ? "Berpejamlah, lalu lihat baik-baik arti semua ini"
                        : "Cobalah bernafas untuk mengingat kehidupan dan merasakan setiap kelahiran.";
                }
            }
        }
    }

    public void OnFinalPanelClicked()
    {
        if (isCorrect)
        {
            PlayerPrefs.SetInt("HasReturnedFromRoll", 1);
            PlayerPrefs.Save();
            Debug.Log("FLAG DISIMPAN: HasReturnedFromRoll= 1");
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            clickedOrder.Clear();
            resultReady = false;
            cubeClicked = false;
            finalPanel.SetActive(false);

            // 🔧 Cari SEMUA StandOnClick (yang nempel di pivot)
            foreach (StandOnClick pivotScript in FindObjectsOfType<StandOnClick>(true)) // true = termasuk yang inactive
            {
                GameObject pivot = pivotScript.gameObject;
                pivot.SetActive(true); // WAJIB aktifin pivot (biar kelihatan di scene)
                Debug.Log(" Reset pivot: " + pivot.name);
                pivotScript.ResetState(); // Rotasi balik ke awal

                // Aktifkan semua anak juga (misal Quad-nya)
                foreach (Transform child in pivot.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            Hint.text = "Hint :\r\n1. Manusia terlahir dari rahim seorang ibu\r\n2. Membawa darah di sekujur tubuhnya ketika tangan penjemput menariknya keluar\r\n3. Tangis pertama keluar dari matanya\r\n4. Dadanya terisi nafas langit, bernafas\r\n5. Kulitnya merasakan denyut memercikan gercik hangat";
            Quad.SetActive(true);
        }
    }

    public void ResetPuzzle()
    {
        clickedOrder.Clear();

        foreach (StandOnClick pivot in FindObjectsOfType<StandOnClick>())
        {
            pivot.gameObject.SetActive(true);
            pivot.ResetState();
        }
        Hint.text = "Hint :\r\n1. Manusia terlahir dari rahim seorang ibu\r\n2. Membawa darah di sekujur tubuhnya ketika tangan penjemput menariknya keluar\r\n3. Tangis pertama keluar dari matanya\r\n4. Dadanya terisi nafas langit, bernafas\r\n5. Kulitnya merasakan denyut memercikan gercik hangat";
        clearCube.SetActive(false);
        darkCube.SetActive(false);
        
        resultReady = false;
        cubeClicked = false;
    }

    public bool IsAnswerCorrect()
    {
        return isCorrect;
    }
}
