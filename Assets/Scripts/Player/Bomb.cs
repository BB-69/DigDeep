using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float Speed = 4;

    public float Damage = 10;

    public float Splash = 1.5f;

    public Vector3 LaunchOffSet;

    public GameObject ExplodeEffect;

    public bool Thrown, hasExploded = false;



    public Transform effectOffSet;
    void Start()
    {
        Animator animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.Log("Animator not found!");
            return;
        }

        if (Thrown)
        {

            var direction = -transform.right + Vector3.up;

            GetComponent<Rigidbody2D>().AddForce(direction * Speed, ForceMode2D.Impulse);
        }

    }
    void Update()
    {
        if (!Thrown)
        {
            transform.position += -transform.right * Speed * Time.deltaTime;
        }
        if (!hasExploded)
        {
            hasExploded = true;
            StartCoroutine(ExplodeAfterDelay(.5f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if (Splash > 0)
        //{
        //    var hitColliders = Physics2D.OverlapCircleAll(transform.position, Splash);

        //    foreach (var hitCollider in hitColliders)
        //    {
        //        var block = hitCollider.GetComponent<GetDamage>();
        //        if (block)
        //        {
        //            var closePoint = hitCollider.ClosestPoint(transform.position);
        //            var distance = Vector3.Distance(closePoint, transform.position);

        //            var damagePercent = Mathf.InverseLerp(Splash, 0, distance);
        //            block.TakeHit(damagePercent * Damage);
        //        }
        //    }
        //}
        //else
        //{
        //    var Damage = collision.collider.GetComponent<GetDamage>();

        //    if (Damage)
        //    {
        //        Damage.TakeHit(1);
        //        Debug.Log("ทำดาเมจแล้วอีเหี้ย");
        //    }
        //    Destroy(gameObject);
        //    Debug.Log("ยิงแล้วอีสัส");
        //}
    }
    IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Splash > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, Splash);

            foreach (var hitCollider in hitColliders)
            {
                var block = hitCollider.GetComponent<GetDamage>();
                if (block)
                {
                    var closePoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closePoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(Splash, 0, distance);
                    block.TakeHit(damagePercent * Damage);
                }
            }
        }


        GameObject Explosion = Instantiate(ExplodeEffect, effectOffSet.position, Quaternion.identity);


        Animator explosionAnim = Explosion.GetComponent<Animator>();
        if (explosionAnim != null)
        {
            yield return null;
            float animLength = explosionAnim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(Explosion, animLength);
        }
        else
        {
            Destroy(Explosion, 0.5f);
        }

        Destroy(gameObject);

    }
}
