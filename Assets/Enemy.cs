using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;

    [SerializeField] float movePower;
    [SerializeField] float maxMoveSpeed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);

        EnemyManager.GetInsance().returnEnemy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // (1, 1, 1)
        Vector3 localScale = Vector3.one;
        localScale.x = Mathf.Sign(movePower);
        transform.localScale = localScale;
    }

    private void FixedUpdate()
    {
        rigid.AddForce(new Vector2(movePower, 0), ForceMode2D.Impulse);

        if (Mathf.Abs(rigid.velocity.x) > maxMoveSpeed)
            rigid.velocity = new Vector2(Mathf.Sign(movePower)
                * maxMoveSpeed, rigid.velocity.y);
    }
}
