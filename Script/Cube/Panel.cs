using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanelClick : MonoBehaviour
{
    public CubePuzzleManager puzzleManager;

    void OnMouseDown()
    {
        if (puzzleManager == null) return;

        if (puzzleManager.IsAnswerCorrect())
        {
            // Jawaban benar → Ganti scene
            SceneManager.LoadScene("SampleScene"); // Ganti ke scene tujuan
        }
        else
        {
            // Jawaban salah → Reset
            puzzleManager.ResetPuzzle();
            gameObject.SetActive(false); // Sembunyikan panel
        }
    }
}
