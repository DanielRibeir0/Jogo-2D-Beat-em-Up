using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;
    private Animator _playerAnimator;

    public float _playerSpeed = 3f;
    public Vector2 _playerDirection;

    private bool _playerFaceRight = true;
    private bool _isWalk;
    private int _punchCount;
    private Coroutine _punchCoroutine;

    [Header("Ataque")]
    public int attackDamage = 10;
    public float attackRadius = 0.8f;
    public Transform attackPoint;

    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMove();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        _isWalk = (_playerDirection.x != 0 || _playerDirection.y != 0);

        _playerRigidbody2D.MovePosition(
            _playerRigidbody2D.position +
            _playerDirection.normalized * _playerSpeed * Time.fixedDeltaTime
        );
    }

    void PlayerMove()
    {
        _playerDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (_playerDirection.x < 0 && _playerFaceRight)
            Flip();
        else if (_playerDirection.x > 0 && !_playerFaceRight)
            Flip();

        if (Input.GetKeyDown(KeyCode.X) && !_isWalk)
        {
            if (_punchCoroutine != null)
                StopCoroutine(_punchCoroutine);

            _punchCoroutine = StartCoroutine(PunchController());

            if (_punchCount < 2)
            {
                PlayerJab();
                _punchCount++;
            }
            else
            {
                PlayerPunch();
                _punchCount = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && !_isWalk)
        {
            PlayerKick();
        }
    }

    void UpdateAnimator()
    {
        _playerAnimator.SetBool("isWalk", _isWalk);
    }

    void Flip()
    {
        _playerFaceRight = !_playerFaceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void PlayerJab()
    {
        _playerAnimator.SetTrigger("isJab");
        DealDamage();
    }

    void PlayerPunch()
    {
        _playerAnimator.SetTrigger("isPunch");
        DealDamage();
    }

    void PlayerKick()
    {
        _playerAnimator.SetTrigger("isKick");
        DealDamage();
    }

    void DealDamage()
    {
        if (attackPoint == null)
        {
            Debug.LogWarning("AttackPoint não foi atribuído no Inspector.");
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);

        foreach (Collider2D hit in hits)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
                Debug.Log("Player acertou o inimigo: " + hit.name);
            }
        }
    }

    IEnumerator PunchController()
    {
        yield return new WaitForSeconds(0.75f);
        _punchCount = 0;
        _punchCoroutine = null;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}