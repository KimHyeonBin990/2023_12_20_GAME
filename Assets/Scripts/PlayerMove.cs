using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int currentHp;
    public float maxSpeed;
    public float JumpPower;
    public int maxHp;

    public float attackRange = 1.5f;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        currentHp = maxHp;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;

        // Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

        // Attack
        if (Input.GetKeyDown(KeyCode.Z) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("isAttacking");
            Attack();
        }
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Monster"));

        foreach (Collider2D collider in hitColliders)
        {
            MonsterController monster = collider.GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.TakeDamage(5); // 5 is the player's attack damage
            }
        }
    }

    private bool isDie = false;

    void Die()
    {
        if (isDie)
            return;

        isDie = true;

        anim.SetTrigger("isDie");

        // You can add custom logic here if needed

        // Example: Reload the scene after a delay
        Invoke("ReloadScene", 1.0f);
    }

    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Standby screen");
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    anim.SetBool("isJumping", false);
            }
        }

        float characterWidth = spriteRenderer.bounds.extents.x;
        float characterHeight = spriteRenderer.bounds.extents.y;

        float screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x + characterWidth;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - characterWidth;

        float screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - characterHeight;

        float screenBottom = Camera.main.ScreenToWorldPoint(Vector3.zero).y + characterHeight;

        float clampedX = Mathf.Clamp(transform.position.x, screenLeft, screenRight);
        float clampedY = Mathf.Clamp(transform.position.y, screenBottom, screenTop);

        transform.position = new Vector2(clampedX, clampedY);
    }
    public void TakeDamage(int damage)
    {
        // Player takes damage logic goes here
        // For example, reduce player health or play a hurt animation
    }
}
