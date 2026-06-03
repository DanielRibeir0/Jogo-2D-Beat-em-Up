using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Detecção")]
    public float detectionRange = 6f;
    public float attackRange = 1.36f;
    public float stopDistance = 1f;

    [Header("Movimento")]
    public float moveSpeed = 1.8f;

    [Header("Ataque")]
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;
    public float attackHitDelay = 0.3f;

    [Header("Referências")]
    public Transform player;

    private Rigidbody2D _rb;
    private Animator _anim;
    private PlayerHealth _playerHealth;
    private PlayerHealth _enemyHealth;

    private bool _facingRight = true;
    private bool _isAttacking = false;
    private bool _isDead = false;
    private float _attackTimer = 0f;
    private Coroutine _attackCoroutine;

    private enum State { Idle, Chase, Attack, Dead }
    private State _state = State.Idle;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _enemyHealth = GetComponent<PlayerHealth>();

        if (_enemyHealth != null)
            _enemyHealth.onDeath.AddListener(OnDeath);

        if (player == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");

            if (go != null)
            {
                player = go.transform;
                _playerHealth = go.GetComponent<PlayerHealth>();
            }
        }
        else
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
        }

        if (player != null)
            _facingRight = player.position.x > transform.position.x;
    }

    void Update()
    {
        if (_isDead || player == null)
            return;

        _attackTimer -= Time.deltaTime;

        float dist = Vector2.Distance(transform.position, player.position);

        if (_isAttacking)
        {
            if (dist > attackRange)
                CancelAttack();

            return;
        }

        if (dist <= attackRange && _attackTimer <= 0f)
        {
            _state = State.Attack;
        }
        else if (dist <= detectionRange)
        {
            _state = State.Chase;
        }
        else
        {
            _state = State.Idle;
        }

        switch (_state)
        {
            case State.Idle:
                HandleIdle();
                break;

            case State.Chase:
                HandleChase();
                break;

            case State.Attack:
                HandleAttack();
                break;
        }
    }

    void FixedUpdate()
    {
        if (_isDead || player == null)
            return;

        if (_state == State.Chase && !_isAttacking)
        {
            float dist = Vector2.Distance(transform.position, player.position);

            if (dist > stopDistance)
            {
                Vector2 dir = (player.position - transform.position).normalized;

                _rb.MovePosition(
                    _rb.position + dir * moveSpeed * Time.fixedDeltaTime
                );
            }
        }
    }

    void HandleIdle()
    {
        _anim.SetBool("isWalk", false);
    }

    void HandleChase()
    {
        _anim.SetBool("isWalk", true);
        FacePlayer();
    }

    void HandleAttack()
    {
        if (_isAttacking)
            return;

        _anim.SetBool("isWalk", false);
        FacePlayer();

        _attackCoroutine = StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _attackTimer = attackCooldown;

        _anim.ResetTrigger("isPunch");
        _anim.SetTrigger("isPunch");

        yield return new WaitForSeconds(attackHitDelay);

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange && _playerHealth != null)
        {
            _playerHealth.TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(0.4f);

        _isAttacking = false;
        _state = State.Idle;
        _attackCoroutine = null;

        _anim.ResetTrigger("isPunch");
        _anim.SetBool("isWalk", false);
        _anim.Play("Enemy_Idle");
    }

    void CancelAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _isAttacking = false;
        _state = State.Chase;

        _anim.ResetTrigger("isPunch");
        _anim.SetBool("isWalk", true);
        _anim.Play("Enemy_Idle");
    }

    public void TakeDamage(int amount)
    {
        if (_isDead)
            return;

        if (_enemyHealth != null)
            _enemyHealth.TakeDamage(amount);
    }

    public void OnDeath()
    {
        if (_isDead)
            return;

        _isDead = true;
        _state = State.Dead;

        StopAllCoroutines();

        _anim.SetBool("isWalk", false);
        _anim.ResetTrigger("isPunch");
        _anim.SetTrigger("isHurt");

        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Static;
        }

        StartCoroutine(DestroyAfterDeath());
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    void FacePlayer()
    {
        if (player == null)
            return;

        bool playerOnRight = player.position.x > transform.position.x;

        if (playerOnRight && !_facingRight)
            Flip();

        if (!playerOnRight && _facingRight)
            Flip();
    }

    void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}