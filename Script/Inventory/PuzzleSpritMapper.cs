using UnityEngine;
using System.Collections.Generic;

public class PuzzleSpriteMapper : MonoBehaviour
{
    public static PuzzleSpriteMapper instance;

    [System.Serializable]
    public class PuzzleSprite
    {
        public string id;
        public Sprite sprite;
    }

    public List<PuzzleSprite> spriteList;
    private Dictionary<string, Sprite> spriteDict;

    void Awake()
    {
        instance = this;
        spriteDict = new Dictionary<string, Sprite>();
        foreach (var item in spriteList)
        {
            spriteDict[item.id] = item.sprite;
        }
    }

    public Sprite GetSpriteByID(string id)
    {
        if (spriteDict.ContainsKey(id))
        {
            return spriteDict[id];
        }
        return null;
    }
}