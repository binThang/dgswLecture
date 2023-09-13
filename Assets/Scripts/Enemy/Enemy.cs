using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyState currentState;

    public Rigidbody2D rigid;
    [SerializeField] private Animator anim;

    [SerializeField] public GameObject Target;
    [SerializeField] public float movePower;
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public float findRange;
    [SerializeField] public float attackRange;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangeState(new IdleState(this));
    }

    public void ChangeState(EnemyState enemyState)
    {
        if (currentState != null)
            currentState.OnExitState();
        currentState = enemyState;
        currentState.OnEnterState();
    }

    public void TakeDamage(float damage)
    {
        //HP -= damage;

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
        if (rigid.velocity.x != 0)
        {
            Vector3 localScale = Vector3.one;
            localScale.x = Mathf.Sign(rigid.velocity.x);
            transform.localScale = localScale;
        }

        currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdateState();
        //switch (state)
        //{
        //    case EnemyState.Idle:
        //        if (Vector2.Distance(Target.transform.position,
        //            transform.position) < findRange)
        //            state = EnemyState.Chasing;
        //        break;
        //    case EnemyState.Chasing:
        //        if (Vector2.Distance(Target.transform.position,
        //            transform.position) < attackRange)
        //        {
        //            rigid.velocity = new Vector2(0, 0);
        //            state = EnemyState.Attack;
        //        }

        //        float moveDirection = Target.transform.position.x - transform.position.x;
        //        Vector2 moveForce = new Vector2(Mathf.Sign(moveDirection) * movePower, 0);

        //        rigid.AddForce(moveForce, ForceMode2D.Impulse);

        //        if (Mathf.Abs(rigid.velocity.x) > maxMoveSpeed)
        //            rigid.velocity = new Vector2(Mathf.Sign(moveDirection)
        //                * maxMoveSpeed, rigid.velocity.y);
        //        break;
        //    case EnemyState.Attack:
        //        Debug.Log("Attack");
        //        break;
        //}
    }

    [SerializeField] float attackPoint = 1f;
    [SerializeField] float attackPointRange = 2f;

    public void SetTriggerAnimation(string triggername)
    {
        anim.SetTrigger(triggername);
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position + new Vector3(transform.localScale.x * attackPoint, 0), attackPointRange);
        if (hit.gameObject.tag == "Player")
        {
            Vector2 direction = hit.gameObject.transform.position - transform.position;
            hit.GetComponent<PlayerController>().TakeDamage(10, 5 * direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(transform.localScale.x * attackPoint, 0), attackPointRange);
    }
}
