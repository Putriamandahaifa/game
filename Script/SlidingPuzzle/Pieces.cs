//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Pieces : MonoBehaviour
//{
//    public int index; // posisi yang benar
//    public bool isEmpty = false;

//    private Button button;

//    // Start is called before the first frame update
//    void Start()
//    {
//        button = GetComponent<Button>();
//        if (button != null)
//        {
//            button.onClick.AddListener(OnPieceClicked);
//        }
//    }

//    void OnPieceClicked()
//    {
//        SlidingPuzzleManager.Instance.MovePiece(this);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pieces : MonoBehaviour
{
    public int index;         // Posisi yang benar
    public int originalIndex;  // Posisi seharusnya (urutan benar)
    public bool isEmpty;       // Apakah ini bagian kosong
    private Button button;

    void Start()
    {

        Debug.Log("Pieces Start called on: " + gameObject.name);

        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnPieceClicked);
            Debug.Log("Listener added to: " + gameObject.name);
        }
        else
        {
            Debug.LogWarning("Button not found on: " + gameObject.name);
        }
    }

    public void OnPieceClicked()
    {
        Debug.Log("Piece clicked: " + index);
        SlidingPuzzleManager.Instance.MovePiece(this);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked at: " + Input.mousePosition);
        }
    }

}
