using UnityEngine;


public class MonsterController : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;
    public int attackDamage = 1;
    public int maxHealth = 3;

    private int currentHealth;
    private Transform player;
    private Animator animator;
    private int playerCollisions = 0;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 플레이어를 감지하면 공격
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // 플레이어와의 거리가 공격 범위 이내인지 확인
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                // 플레이어를 공격
                Attack();
            }
            else
            {
                // 플레이어를 향해 이동
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // 몬스터를 플레이어 방향으로 이동
        transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);

        // 이동 방향에 따라 좌우 반전
        if (transform.position.x > player.position.x)
        {
            // 플레이어가 왼쪽에 있을 때
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // 플레이어가 오른쪽에 있을 때
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // 애니메이터에 이동 관련 파라미터 설정
        animator.SetBool("isWalking", true);
    }

    void Attack()
    {
        // 플레이어에게 데미지 주기
        // 여기에 플레이어에게 데미지를 주는 코드 추가

        // 애니메이터에 공격 관련 파라미터 설정
        animator.SetTrigger("isAttack");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌했을 때
            playerCollisions++;

            if (playerCollisions >= 2)
            {
                // 여기에 죽음 처리 및 애니메이터 설정 추가

                // 애니메이터에 Die 관련 파라미터 설정
                animator.SetTrigger("isDie");

                // 몬스터 오브젝트 비활성화 또는 제거
                gameObject.SetActive(false);
                // 또는 Destroy(gameObject);
            }
            else
            {
                // If the monster hasn't collided with the player twice yet, reset its health
                currentHealth = maxHealth;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // 몬스터가 피해를 입었을 때 호출되는 함수
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 몬스터가 죽을 때 호출되는 함수
        playerCollisions++;

        if (playerCollisions >= 2)
        {
            // 여기에 죽음 처리 및 애니메이터 설정 추가

            // 애니메이터에 Die 관련 파라미터 설정
            animator.SetTrigger("isDie");

            // 몬스터 오브젝트 비활성화 또는 제거
            gameObject.SetActive(false);
            // 또는 Destroy(gameObject);

        }
        else
        {
            // If the monster hasn't collided with the player twice yet, reset its health
            currentHealth = maxHealth;
        }
    }

}