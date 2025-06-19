using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class PuzzleData
{
    public List<string> pieces = new List<string>();
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private string savePath;
    public PuzzleData puzzleData = new PuzzleData();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            savePath = "C:/Users/MaulaCell/Dream/puzzle_save.json";
            Debug.Log($"[InventoryManager] Path penyimpanan: {savePath}");
            DontDestroyOnLoad(gameObject);
            LoadInventory();
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("[InventoryManager] Duplikat terdeteksi, objek dihancurkan.");
        }
    }


    public void AddPiece(string pieceID)
    {
        Debug.Log("[Inventory] Menambahkan potongan...");

        if (!puzzleData.pieces.Contains(pieceID))
        {
            puzzleData.pieces.Add(pieceID);
            SaveInventory();
            Debug.Log($"[Inventory] Menyimpan potongan: {pieceID}");
        }
        else
        {
            Debug.Log($"[Inventory] Potongan {pieceID} sudah ada di inventori.");
        }
    }

    public void LoadInventory()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            puzzleData = JsonUtility.FromJson<PuzzleData>(json);
            Debug.Log("[Inventory] Data inventori berhasil dimuat.");
        }
        else
        {
            puzzleData = new PuzzleData();
            Debug.Log("[Inventory] Tidak ada file inventori, membuat baru.");
        }
    }

    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(puzzleData, true);
        File.WriteAllText(savePath, json);
    }

    public List<string> GetPuzzlePieces()
    {
        return puzzleData.pieces;
    }
}
