using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnItemTotheWorld : MonoBehaviour
{
  
    public DropItem itemDrops;
    
    public void ItemDrop()
    {
       // for (int i = 0; i < itemDrops.Length; i++)
        //{
            Instantiate(itemDrops.gameObject, new UnityEngine.Vector2(0, 2), UnityEngine.Quaternion.identity);
        //}
    }

}
