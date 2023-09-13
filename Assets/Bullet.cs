using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator anim;
    public BulletPool pool;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroySelf", 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("OnHit");
        StopCoroutine("DestroySelf");
        StartCoroutine("DestroySelf", 0.5f);
        //Destroy(this.gameObject, 0.5f);

        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<Enemy>().TakeDamage()

            Debug.Log("On Collision");
        }
    }

    IEnumerator DestroySelf(float time)
    {
        yield return new WaitForSeconds(time);

        //Destroy(gameObject);
        pool.returnBullet(gameObject);
    }
}
