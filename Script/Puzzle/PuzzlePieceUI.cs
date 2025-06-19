using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceUI : MonoBehaviour
{
    public Image icon;

    public void SetPiece(string pieceID)
    {
        Debug.Log($"[UI] Memuat puzzle: {pieceID}");
        Sprite loadedSprite = Resources.Load<Sprite>($"Sprites/{pieceID}");

        if (loadedSprite != null)
        {
            icon.sprite = loadedSprite;
            Debug.Log($"[UI] icon.sprite sekarang = {icon.sprite}");
        }
        else
        {
            Debug.LogError($"[UI] Gagal memuat sprite dari Resources/Sprites/{pieceID}");
        }
    }
}
