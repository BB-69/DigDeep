using System.ComponentModel;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/DropItem")]
public class DropItem : ScriptableObject
{
    public int xp;
    public Sprite image;
    public string itemName;

}
