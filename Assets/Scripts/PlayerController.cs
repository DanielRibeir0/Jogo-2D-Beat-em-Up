using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private Rigidbody2D _playerRigidbody2D;
    public float        _playerSpeed;
    public Vector2      _playerDirection;

    private Animator    _playerAnimator;
    private bool        _playerFaceRight = true;
    private bool        _isWalk;

    private int         _punchCount;
    private float       _timePunch = 0.75f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdateAnimator();
    }

    void PlayerMove()
    {
      
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_playerDirection.x < 0 && _playerFaceRight)
        {
            Flip();
        }
        else if(_playerDirection.x > 0 && !_playerFaceRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_isWalk == false)
            {
                StartCoroutine(PunchController());

                if(_punchCount < 2)
                {
                    PlayerJab();
                    _punchCount++;
                }
                else if(_punchCount >= 2)
                {
                    PlayerPunch();
                    _punchCount = 0;
                }
                StopCoroutine(PunchController());
                
            }
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_isWalk == false)
            {
                PlayerKick();
                
            }
        }



    }

    private void FixedUpdate()
    {
        if (_playerDirection.x != 0 || _playerDirection.y != 0) 
        {
            _isWalk = true;
        }
        else
        {
            _isWalk = false;
        }

        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerDirection.normalized * _playerSpeed * Time.fixedDeltaTime);

    }


    void UpdateAnimator()
    {
        _playerAnimator.SetBool("isWalk",_isWalk); // true ou false
    }

    void Flip()
    {
        _playerFaceRight = !_playerFaceRight;
        transform.Rotate(0f, 180, 0f);
    }

    void PlayerJab()
    {

        _playerAnimator.SetTrigger("isJab");
    }

    void PlayerPunch()
    {
        _playerAnimator.SetTrigger("isPunch");
    }

    IEnumerator PunchController()
    {
        yield return new WaitForSeconds(_timePunch);
        _punchCount = 0;
    }

    void PlayerKick()
    {
        _playerAnimator.SetTrigger("isKick");
    }

}
