using UnityEngine;
using UnityEngine.UI;

public class setcheckpointtoTrue : MonoBehaviour
{
    public Button setCheckpointButton;
    public GameObject inventoryManager;

	void Start () {
		Button btn = setCheckpointButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
        inventoryManager.GetComponent<InventoryManager>().isOnCheckpoint = true;
	}
    
}
