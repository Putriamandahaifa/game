using UnityEngine;

public class PuzzlePieceActivator : MonoBehaviour
{
    public GameObject piece02Object;

    void Start()
    {
        // Kalau player sudah pernah kembali dari JigsawScene, aktifkan Pieces_02
        if (PlayerPrefs.GetInt("HasReturnedFromJigsaw", 0) == 1)
        {
            piece02Object.SetActive(true);
        }
        else
        {
            piece02Object.SetActive(false);
        }
    }
}
