using UnityEditor;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public string pieceID = "Piece_01";
    private bool isInRange = false;
    //public GameObject hint;
    //public GameObject warta;
    //public bool isHintShown = false;
    //public bool isWartaShown = false;
    public HintHandler hintHandler; 
    public GameObject intro;

    void Start()
    {
        // Atur apakah potongan ini perlu dimunculkan saat scene dimulai
        bool sudahDiambil = InventoryManager.instance.GetPuzzlePieces().Contains(pieceID);
        bool punyaPiece01 = InventoryManager.instance.GetPuzzlePieces().Contains("Piece_01");
        bool punyaPiece02 = InventoryManager.instance.GetPuzzlePieces().Contains("Piece_02");
        bool punyaPiece03 = InventoryManager.instance.GetPuzzlePieces().Contains("Piece_03");
        bool punyaPiece04 = InventoryManager.instance.GetPuzzlePieces().Contains("Piece_04");
        int hasReturned = PlayerPrefs.GetInt("HasReturnedFromJigsaw", 0);
        int hasMaze = PlayerPrefs.GetInt("HasReturnedFromMaze", 0);
        int hasRoll = PlayerPrefs.GetInt("HasReturnedFromRoll", 0);
        //hint.SetActive(false);  
        if(hasReturned == 0 || hasMaze == 0 || hasRoll == 0)
        {
            intro.SetActive(true);
        }
        

        // Khusus untuk Piece_01, muncul kalau belum diambil
        if (pieceID == "Piece_01")
        {
            gameObject.SetActive(!sudahDiambil);
        }
        // Khusus untuk Piece_02, muncul hanya kalau Piece_01 sudah ada, dan Piece_02 belum
        else if (pieceID == "Piece_02")
        {
            Debug.Log($"[AutoActivator] Cek HasReturnedFromJigsaw = {hasReturned}");

            if (hasReturned == 1)
            {
                hintHandler.intro.SetActive(false);
                gameObject.SetActive(true);
                Debug.Log("Pieces_02 AKTIF ");
                if (punyaPiece02 == true)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
            else
            {
                gameObject.SetActive(false);
                Debug.Log("Pieces_02 NONAKTIF ");
            }
        }
        else if (pieceID == "Piece_03")
        {
            //gameObject.SetActive(punyaPiece02 && !sudahDiambil);

            //if (punyaPiece02 == true)
            //{
            //    gameObject.SetActive(true);
            //}
            //else
            //{
            //    gameObject.SetActive(false);
            //}

            
            Debug.Log($"[AutoActivator] Cek HasReturnedFromJigsaw = {hasMaze}");

            if (hasMaze == 1)
            {
                intro.SetActive(false);
                gameObject.SetActive(true);
                Debug.Log("Pieces_03 AKTIF");
                if (punyaPiece03 == true)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
            else
            {
                gameObject.SetActive(false);
                Debug.Log("Pieces_03 NONAKTIF");
            }
        }
        else if (pieceID == "Piece_04")
        {
            //gameObject.SetActive(punyaPiece03 && !sudahDiambil);

            //if (punyaPiece03 == true)
            //{
            //    gameObject.SetActive(true);
            //}
            //else
            //{
            //    gameObject.SetActive(false);
            //}
           
            Debug.Log($"[AutoActivator] Cek HasReturnedFromRoll = {hasRoll}");

            if (hasRoll== 1)
            {
                intro.SetActive(false);
                gameObject.SetActive(true);
                Debug.Log("Pieces_04 AKTIF");
                if (punyaPiece04 == true)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
            else
            {
                gameObject.SetActive(false);
                Debug.Log("Pieces_04 NONAKTIF");
            }
        }


        // Tambahkan aturan lain kalau mau lanjut ke Piece_03, dst
    }

    void Update()
    {
        // Klik E kalau dalam jangkauan
        if (Input.GetMouseButtonDown(0))
        {
            intro.SetActive(false);
        }
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();

            if (hintHandler != null)
            {
                hintHandler.ShowHint(pieceID); // munculkan hint
            }
            //hint.SetActive(true);
            //isHintShown = true;
        }
        //else if (isHintShown && Input.GetMouseButtonDown(0))
        //{
        //    hint.SetActive(false);
        //    warta.SetActive(true);
        //    isHintShown = false;
        //    isWartaShown = true;
        //}
        //else if (isWartaShown && Input.GetMouseButtonDown(0))
        //{
        //    warta.SetActive(false);
        //    isWartaShown = false;
        //}
    }

    public void PickUp()
    {
        if (InventoryManager.instance == null)
        {
            Debug.LogError("[PuzzlePiece] InventoryManager.instance = NULL!");
            return;
        }

        InventoryManager.instance.AddPiece(pieceID);
        Debug.Log($"Potongan {pieceID} telah diambil!");
        // Destroy(gameObject);
        gameObject.SetActive(false); // Tetap jalanin Update() sampai panel selesai
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player dalam jangkauan. Tekan 'E' untuk ambil puzzle.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}