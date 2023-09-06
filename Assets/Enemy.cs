using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chasing,
        Attack
    }

    EnemyState state;

    Rigidbody2D rigid;

    [SerializeField] GameObject Target;
    [SerializeField] float movePower;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float findRange;
    [SerializeField] float attackRange;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        state = EnemyState.Idle;
    }

    //private void OnEnable()
    //{
    //    StartCoroutine(Die());
    //}

    //IEnumerator Die()
    //{
    //    yield return new WaitForSeconds(5);

    //    EnemyManager.GetInsance().returnEnemy(gameObject);
    //}

    // Update is called once per frame
    void Update()
    {
        // (1, 1, 1)
        Vector3 localScale = Vector3.one;
        localScale.x = Mathf.Sign(movePower);
        transform.localScale = localScale;

        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chasing:
                break;
            case EnemyState.Attack:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Idle:
                if (Vector2.Distance(Target.transform.position,
                    transform.position) < findRange)
                    state = EnemyState.Chasing;
                break;
            case EnemyState.Chasing:
                if (Vector2.Distance(Target.transform.position,
                    transform.position) < attackRange)
                {
                    rigid.velocity = new Vector2(0, 0);
                    state = EnemyState.Attack;
                }

                float moveDirection = Target.transform.position.x - transform.position.x;
                Vector2 moveForce = new Vector2(Mathf.Sign(moveDirection) * movePower, 0);

                rigid.AddForce(moveForce, ForceMode2D.Impulse);

                if (Mathf.Abs(rigid.velocity.x) > maxMoveSpeed)
                    rigid.velocity = new Vector2(Mathf.Sign(moveDirection)
                        * maxMoveSpeed, rigid.velocity.y);
                break;
            case EnemyState.Attack:
                Debug.Log("Attack");
                break;
        }
    }
}
