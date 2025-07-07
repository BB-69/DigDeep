using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileLib : MonoBehaviour
{
    [System.Serializable]
    public class NamedTile
    {
        public string name;
        public TileBase tile;
    }
    public List<NamedTile> tiles;
    private Dictionary<string, TileBase> tileDict;

    void Awake()
    {
        tileDict = new Dictionary<string, TileBase>();
        foreach (var entry in tiles)
        {
            if (!tileDict.ContainsKey(entry.name))
                tileDict.Add(entry.name, entry.tile);
        }
    }

    public TileBase GetTile(string name)
    {
        if (tileDict.TryGetValue(name, out var tile))
            return tile;
        return null;
    }
}