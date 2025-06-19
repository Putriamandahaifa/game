using UnityEngine;

public class HintHandler : MonoBehaviour
{
    public GameObject hint1;
    public GameObject warta1;
    public GameObject hint2;
    public GameObject warta2;
    public GameObject hint3;
    public GameObject warta3;
    public GameObject hint4;
    public GameObject warta4;

    public GameObject intro;

    private GameObject currentHint;
    private GameObject currentWarta;

    private bool isHintShown = false;
    private bool isWartaShown = false;
    private bool isIntroShown = false;


    void Start()
    {
        // Cek apakah Piece_01 sudah dimiliki player
        bool sudahPunyaPiece01 = InventoryManager.instance.GetPuzzlePieces().Contains("Piece_01");

        if (!sudahPunyaPiece01)
        {
            intro.SetActive(true);
            isIntroShown = true;
        }
        else
        {
            intro.SetActive(false);
            isIntroShown = false;
        }
    }

    void Update()
    {
        if (isHintShown && Input.GetMouseButtonDown(0))
        {
            currentHint?.SetActive(false);
            currentWarta?.SetActive(true);
            isHintShown = false;
            isWartaShown = true;
        }
        else if (isWartaShown && Input.GetMouseButtonDown(0))
        {
            currentWarta?.SetActive(false);
            isWartaShown = false;
        }
        else if (isIntroShown && Input.GetMouseButtonDown(0))
        {
            intro.SetActive(false);
            isIntroShown = false;
        }
    }
   
    // Kita tambahkan pieceID sebagai parameter
    public void ShowHint(string pieceID)
    {
        switch (pieceID)
        {
            case "Piece_01":
                currentHint = hint1;
                currentWarta = warta1;
                break;

            case "Piece_02":
                currentHint = hint2;
                currentWarta = warta2;
                break;
            case "Piece_03":
                currentHint = hint3;
                currentWarta = warta3;
                break;

            case "Piece_04":
                currentHint = hint4;
                currentWarta = warta4;
                break;
            default:
                Debug.LogWarning("Tidak ada hint untuk: " + pieceID);
                return;
        }

        currentHint?.SetActive(true);
        isHintShown = true;
    }
}
