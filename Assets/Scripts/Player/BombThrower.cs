using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BombThrower : MonoBehaviour
{
    public static UnityAction OnFinishThrow;
    [Header("Bomb Thrower Settings")]
    [SerializeField] Transform aimpoint;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] float radius = .1f;
    Camera mainCam;
    public int bombCount { get; private set; } = 10;
    public int BombCount
    {
        get => bombCount;
        set
        {
            bombCount = value;
            if (bombCount < 0) bombCount = 0;
        }
    }

    [Header("Bomb")]
    [SerializeField] GameObject bombPrefab;
    [SerializeField] Rigidbody2D bombRigidbody;
    bool isThrown = false;
    [SerializeField] float explosionForce;
    [SerializeField] float bounceForce = 10f;

    void Awake()
    {
        mainCam = GameManager.instance.MainCamera;
    }
    void Update()
    {
        SetAimPoint();
        if (Input.GetKeyDown(KeyCode.Q) && bombCount > 0 && !isThrown)
        {
            isThrown = true;
            ThrowBomb();
        }
    }
    void OnEnable()
    {
        GameManager.OnLevelStart += SetBombCount;
        OnFinishThrow += DeactivateBomb;
    }
    void OnDisable()
    {
        GameManager.OnLevelStart -= SetBombCount;
        OnFinishThrow -= DeactivateBomb;
    }

    public void AddBomb(int amount)
    {
        BombCount += amount;
    }

    public void SetBombCount()
    {
        if (GameManager.instance.level <= 3)
        {
            bombCount = 5;
        }
        else if (GameManager.instance.level <= 6)
        {
            bombCount = 7;
        }
        else
        {
            bombCount = 10;
        }
        UIManager.Instance.UpdateBombCount(bombCount);
    }

    void ThrowBomb()
    {
        bombCount--;
        UIManager.Instance.UpdateBombCount(bombCount);
        bombPrefab.transform.position = aimpoint.position;
        bombPrefab.SetActive(true);

        // Calculate direction from player to aimpoint in 2D
        Vector2 playerBounceDir = (aimpoint.position - player.transform.position).normalized;
        Vector2 throwDir = ((Vector2)aimpoint.position - (Vector2)player.transform.position).normalized;
        bombRigidbody.AddForce(throwDir * explosionForce, ForceMode2D.Impulse);
        playerRigidbody.AddForce(playerBounceDir * bounceForce, ForceMode2D.Impulse);
    }

    public void DeactivateBomb()
    {
        bombRigidbody.linearVelocity = Vector2.zero;
        bombRigidbody.angularVelocity = 0f;
        bombPrefab.SetActive(false);
        isThrown = false;
    }

    void SetAimPoint()
    {
        Vector2 aimDir = GetMouseWorldPosition();
        if (aimDir.sqrMagnitude > 0.001f)
        {
            Vector3 worldPos = player.transform.position + (Vector3)(aimDir.normalized * radius);
            aimpoint.position = worldPos;
            aimpoint.right = aimDir;
        }
    }

    Vector2 GetMouseWorldPosition()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        return mainCam.ScreenToWorldPoint(mouseScreenPos) - player.transform.position;
    }
}