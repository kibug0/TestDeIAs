using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Internas
    private Rigidbody2D _rb;
    private Animator _anim;
    private Vector2 _movement;
    #endregion

    #region Calibracao
    [SerializeField] float walkSpeed = 10f;
    #endregion

    // //////////////////////////////////////////////////////////////////

    #region Unity Methods
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetInputs();
        AnimPlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    #endregion

    // //////////////////////////////////////////////////////////////////

    #region Methods
    private void GetInputs()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        _rb.MovePosition(_rb.position + _movement.normalized * walkSpeed * Time.fixedDeltaTime);
    }

    private void AnimPlayer()
    {
        if (_movement != Vector2.zero)
        {
            _anim.SetFloat("X", _movement.x);
            _anim.SetFloat("Y", _movement.y);
            _anim.SetBool("IsWalking", true);
        }
        else
        {
            _anim.SetBool("IsWalking", false);
        }
    }
    #endregion
}
