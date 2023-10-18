using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    //[SerializeField] private GameObject bullet;
    [SerializeField] BulletPool bulletPool;

    public float HP;
    [SerializeField] float MaxHP;

    private Rigidbody2D rigid;
    private SpriteRenderer sr;

    private float h;
    private int jumpsRemaining;

    bool isGrounded = false;

    PlayerControl playerControlMap;
    InputAction moveInput;
    InputAction jumpInput;
    InputAction fireInput;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;

        playerControlMap = new PlayerControl();

        moveInput = playerControlMap.Player.Move;
        moveInput.started += OnMoveStarted;
        moveInput.performed += OnMovePerfomed;
        moveInput.canceled += OnMoveCanceled;

        jumpInput = playerControlMap.Player.Jump;
        jumpInput.started += OnJumpStarted;

        fireInput = playerControlMap.Player.Fire;
        fireInput.started += OnFireStarted;

        HP = MaxHP;

        //int level = StaticManager.GameData.PlayerData.Level;
    }

    public void TakeDamage(float damage, Vector2 hitDirection)
    {
        HP -= damage;
        Debug.Log(HP);

        if(HP <= 0)
        {
            // 죽음
            Debug.Log("Die");
        }

        rigid.AddForce(hitDirection, ForceMode2D.Impulse);

        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 입력이 시작 되었을 때
    public void OnMoveStarted(InputAction.CallbackContext context)
    {
        //Debug.Log("Input Started");

    }

    // 입력이 값이 바뀌었을 때
    public void OnMovePerfomed(InputAction.CallbackContext context)
    {
        Vector2 moveInputVector = context.ReadValue<Vector2>();
        h = moveInputVector.x;
        //Debug.Log("Input Performed");
    }

    // 입력이 취소되었을 때 
    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        h = 0;
        //Debug.Log("Input Canceled");
    }

    public void OnFireStarted(InputAction.CallbackContext context)
    {
        //GameObject tempBullet = Instantiate(bullet);
        GameObject tempBullet = bulletPool.getBullet();
        // fire
        float direction = Mathf.Sign(transform.localScale.x);
        tempBullet.transform.position = transform.position
            + new Vector3(direction * 0.5f, 0, 0);

        var bulletRigid = tempBullet.GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(direction * 10 * Vector2.right,
            ForceMode2D.Impulse);

        SoundManager.Instance.PlaySFX(SoundManager.SFX.Attack);
    }

    public void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (isGrounded || jumpsRemaining > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpsRemaining--;
        }
    }

   // Start is called before the first frame update
   void Start()
    {
        moveInput.Enable();
        jumpInput.Enable();
        fireInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded) jumpsRemaining = maxJumps;

        // h = Input.GetAxis("Horizontal")

        // 이동 방향에 따라 오브젝트 반전
        if (h != 0)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);
        //if (h > 0) // right
        //    sr.flipX = false;
        //else if (h < 0) // left
        //    sr.flipX = true;

        // 이동
        rigid.AddForce(new Vector2(h * maxMoveSpeed, 0));

        //Vector3 move = new Vector3(h, v, 0);
        //transform.position += move * Time.deltaTime * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rigid.velocity.x) > maxMoveSpeed && h != 0)
            rigid.velocity = new Vector2(Mathf.Sign(h) * maxMoveSpeed, rigid.velocity.y);
    }
}
