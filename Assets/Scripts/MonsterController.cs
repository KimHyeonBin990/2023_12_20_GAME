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
        // �÷��̾ �����ϸ� ����
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // �÷��̾���� �Ÿ��� ���� ���� �̳����� Ȯ��
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                // �÷��̾ ����
                Attack();
            }
            else
            {
                // �÷��̾ ���� �̵�
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // ���͸� �÷��̾� �������� �̵�
        transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);

        // �̵� ���⿡ ���� �¿� ����
        if (transform.position.x > player.position.x)
        {
            // �÷��̾ ���ʿ� ���� ��
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // �÷��̾ �����ʿ� ���� ��
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // �ִϸ����Ϳ� �̵� ���� �Ķ���� ����
        animator.SetBool("isWalking", true);
    }

    void Attack()
    {
        // �÷��̾�� ������ �ֱ�
        // ���⿡ �÷��̾�� �������� �ִ� �ڵ� �߰�

        // �ִϸ����Ϳ� ���� ���� �Ķ���� ����
        animator.SetTrigger("isAttack");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �浹���� ��
            playerCollisions++;

            if (playerCollisions >= 2)
            {
                // ���⿡ ���� ó�� �� �ִϸ����� ���� �߰�

                // �ִϸ����Ϳ� Die ���� �Ķ���� ����
                animator.SetTrigger("isDie");

                // ���� ������Ʈ ��Ȱ��ȭ �Ǵ� ����
                gameObject.SetActive(false);
                // �Ǵ� Destroy(gameObject);
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
        // ���Ͱ� ���ظ� �Ծ��� �� ȣ��Ǵ� �Լ�
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ���Ͱ� ���� �� ȣ��Ǵ� �Լ�
        playerCollisions++;

        if (playerCollisions >= 2)
        {
            // ���⿡ ���� ó�� �� �ִϸ����� ���� �߰�

            // �ִϸ����Ϳ� Die ���� �Ķ���� ����
            animator.SetTrigger("isDie");

            // ���� ������Ʈ ��Ȱ��ȭ �Ǵ� ����
            gameObject.SetActive(false);
            // �Ǵ� Destroy(gameObject);

        }
        else
        {
            // If the monster hasn't collided with the player twice yet, reset its health
            currentHealth = maxHealth;
        }
    }

}