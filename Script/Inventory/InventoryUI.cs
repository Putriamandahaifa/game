using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI: MonoBehaviour
{
    public GameObject PuzzlePieceUI; // drag prefab UI di sini
    public GameObject inventoryPanel;
    // tempat untuk menaruh puzzle (Grid Layout Group)
    public Transform GridContainer;
    void Start()
    {
        ShowInventory();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool active = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(active);

            if (active)
            {
                ShowInventory(); // tampilkan ulang puzzle yang sudah disimpan
            }
        }
    }
    public void ShowInventory()
    {
        // Bersihkan isi sebelumnya
        foreach (Transform child in GridContainer)
        {
            Destroy(child.gameObject);
        }

        List<string> pieces = InventoryManager.instance.GetPuzzlePieces();
        foreach (string pieceID in pieces)
        {
            Debug.Log($"[UI] Memuat puzzle: {pieceID}");
            GameObject uiPiece = Instantiate(PuzzlePieceUI, GridContainer);
            uiPiece.GetComponent<PuzzlePieceUI>().SetPiece(pieceID);
        }
    }
    public void OpenInventoryFromUI()
    {
        this.ToggleInventory();
    }
    public void ToggleInventory()
    {
        bool active = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(active);

        if (active)
        {
            StartCoroutine(DelayedShowInventory());
        }
    }
    private System.Collections.IEnumerator DelayedShowInventory()
    {
        yield return null; // tunggu 1 frame
        ShowInventory();
    }
}