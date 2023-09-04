using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxMoveSpeed;

    [SerializeField]
    private float jumpPower;

    private Rigidbody2D rigid;
    private SpriteRenderer sr;

    float h;

    bool isJumping = false;

    PlayerControl playerControlMap;
    InputAction moveInput;
    InputAction jumpInput;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        playerControlMap = new PlayerControl();
        moveInput = playerControlMap.Player.Move;

        moveInput.started += OnMoveStarted;
        moveInput.performed += OnMovePerfomed;
        moveInput.canceled += OnMoveCanceled;

        jumpInput = playerControlMap.Player.Jump;
    }

    // 입력이 시작 되었을 때
    public void OnMoveStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Input Started");
    }

    // 입력이 값이 바뀌었을 때
    public void OnMovePerfomed(InputAction.CallbackContext context)
    {
        Vector2 moveInputVector = context.ReadValue<Vector2>();
        h = moveInputVector.x;
        Debug.Log("Input Performed");
    }

    // 입력이 취소되었을 때 
    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        h = 0;
        Debug.Log("Input Canceled");
    }

    // Start is called before the first frame update
    void Start()
    {
        moveInput.Enable();
        jumpInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // h = Input.GetAxis("Horizontal")

        if (h > 0) // right
            sr.flipX = false;
        else if (h < 0) // left
            sr.flipX = true;

        if (jumpInput.IsPressed() && !isJumping)
        {
            // Vector2.up = new Vector(0, 1)
            rigid.AddForce(new Vector2(h, jumpPower), ForceMode2D.Impulse);
            isJumping = true;
        }

        //Vector3 move = new Vector3(h, v, 0);
        //transform.position += move * Time.deltaTime * moveSpeed;
    }

    private void FixedUpdate()
    {
        if (!isJumping)
        {
            rigid.AddForce(new Vector2(h, 0), ForceMode2D.Impulse);

            if (Mathf.Abs(rigid.velocity.x) > maxMoveSpeed)
                rigid.velocity = new Vector2(Mathf.Sign(h) * maxMoveSpeed, rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
