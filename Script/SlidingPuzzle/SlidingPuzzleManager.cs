using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlidingPuzzleManager : MonoBehaviour
{
    public static SlidingPuzzleManager Instance;

    public GameObject puzzlePiecePrefab;
    public Transform puzzleGrid;
    public Sprite[] puzzleSprites;
    public int gridSize = 6;

    private Pieces[] puzzlePieces;
    private int emptyIndex;

    void Awake()
    {
        Instance = this;
        Debug.Log("SlidingPuzzleManager Instance set.");
    }

    void Start()
    {
        SetupPuzzle();
    }

    void SetupPuzzle()
    {
        int totalPieces = gridSize * gridSize;
        puzzlePieces = new Pieces[totalPieces];

        for (int i = 0; i < totalPieces; i++)
        {
            GameObject piece = Instantiate(puzzlePiecePrefab, puzzleGrid);
            Pieces pieceScript = piece.GetComponent<Pieces>();
            pieceScript.index = i;
            pieceScript.originalIndex = i;

            Image img = piece.GetComponent<Image>();
            img.sprite = puzzleSprites[i];

            piece.transform.localPosition = Vector3.zero;
            puzzlePieces[i] = pieceScript;
        }

        ShufflePuzzle(); // ✅ BARU DIPANGGIL DI SINI, setelah semua potongan dibuat
    }

    void ShufflePuzzle()
    {
        int total = puzzlePieces.Length;

        // Acak posisi array
        for (int i = 0; i < total; i++)
        {
            int randomIndex = Random.Range(0, total);
            SwapPieces(i, randomIndex);
        }

        // Reset semua jadi bukan kosong dulu
        for (int i = 0; i < total; i++)
        {
            puzzlePieces[i].isEmpty = false;

            // Pastikan semuanya terlihat dan bisa diklik dulu
            var img = puzzlePieces[i].GetComponent<Image>();
            var btn = puzzlePieces[i].GetComponent<Button>();
            if (img != null) img.enabled = true;
            if (btn != null) btn.interactable = true;
        }

        // Cari siapa yang punya sprite ke-35 (row 6 col 6)
        Sprite targetSprite = puzzleSprites[35];

        for (int i = 0; i < total; i++)
        {
            if (puzzlePieces[i].GetComponent<Image>().sprite == targetSprite)
            {
                puzzlePieces[i].isEmpty = true;
                emptyIndex = i;

                var img = puzzlePieces[i].GetComponent<Image>();
                var btn = puzzlePieces[i].GetComponent<Button>();
                if (img != null) img.enabled = false;
                if (btn != null) btn.interactable = false;

                Debug.Log("Empty piece set at index: " + i);
                break;
            }
        }
    }

    void SwapPieces(int indexA, int indexB)
    {
        Pieces temp = puzzlePieces[indexA];
        puzzlePieces[indexA] = puzzlePieces[indexB];
        puzzlePieces[indexB] = temp;

        // Tukar posisi visual di grid UI
        puzzlePieces[indexA].transform.SetSiblingIndex(indexA);
        puzzlePieces[indexB].transform.SetSiblingIndex(indexB);

        // Tukar nilai index logikanya
        int tempIndex = puzzlePieces[indexA].index;
        puzzlePieces[indexA].index = puzzlePieces[indexB].index;
        puzzlePieces[indexB].index = tempIndex;
    }

    public void MovePiece(Pieces piece)
    {
        int pieceIndex = piece.index;
        if (IsAdjacent(pieceIndex, emptyIndex))
        {
            SwapPieces(pieceIndex, emptyIndex);
            emptyIndex = pieceIndex;

            CheckPuzzleSolved(); // panggil di sini, setelah swap
        }
        else
        {
            Debug.Log("Move blocked (not adjacent)");
        }
    }

    int GetPieceIndex(Pieces piece)
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (puzzlePieces[i] == piece)
                return i;
        }
        return -1;
    }

    bool IsAdjacent(int indexA, int indexB)
    {
        int rowA = indexA / gridSize;
        int colA = indexA % gridSize;

        int rowB = indexB / gridSize;
        int colB = indexB % gridSize;

        return (Mathf.Abs(rowA - rowB) + Mathf.Abs(colA - colB)) == 1;
    }

    void CheckPuzzleSolved()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (puzzlePieces[i].index != puzzlePieces[i].originalIndex)
                return;
        }

        Debug.Log("Puzzle solved!");

        PlayerPrefs.SetInt("HasReturnedFromJigsaw", 1);
        PlayerPrefs.Save();
        Debug.Log("FLAG DISIMPAN: HasReturnedFromJigsaw = 1");
        SceneManager.LoadScene("SampleScene");
    }

}
