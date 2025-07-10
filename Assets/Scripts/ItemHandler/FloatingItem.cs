using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 private float baseY;
    public float floatAmount = 0.1f;
    public float floatSpeed = 2f;

    void Start()
    {
        baseY = transform.position.y;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = baseY + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = pos;
    }
}
