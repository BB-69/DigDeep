
using UnityEngine;
using TMPro;

public class ItemToolTipUI : MonoBehaviour
{
  public static ItemToolTipUI instance;

  [Header("TMP Texts")]
  public TextMeshProUGUI nameText;
  public TextMeshProUGUI descriptionText;
  public TextMeshProUGUI xpText;
  private bool isVisible = false;


  private void Awake()
  {

    instance = this;  // 👈 ตั้งค่านี้ให้ instance ใช้ได้
    gameObject.SetActive(false); // ซ่อน tooltip ตอนเริ่มเกม
  }

  public void ShowToolTip(DropItem item, Vector3 position)
  {
    nameText.text = item.itemName;
    descriptionText.text = item.description;
    xpText.text = "XP: " + item.xp;

    // แก้จุดนี้: ขยับ Tooltip ออกห่างจากเมาส์เล็กน้อย
    Vector3 offset = new Vector3(-40f, -20f, 0f);
    transform.position = position + offset;

    gameObject.SetActive(true);
    isVisible = true;

  }

  public void HideToolTip()
  {
    gameObject.SetActive(false);
    isVisible = false;

  }
    
     private void Update()
    {
        if (isVisible)
        {
            transform.position = Input.mousePosition + new Vector3(-40f, -20f, 0f);
        }
    }
}