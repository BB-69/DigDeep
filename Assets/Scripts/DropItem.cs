using System.ComponentModel;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/DropItem")]
public class DropItem : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase title;
    public GameObject gameObject;
    public int xp;
    [Header("Only UI")]
    public bool stackAble;
    [Header("Both")]
    public Sprite image;

}
