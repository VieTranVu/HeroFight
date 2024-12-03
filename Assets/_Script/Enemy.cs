using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public float moveSpeed = 2f;             // Tốc độ di chuyển của kẻ thù
    private bool isGround;
    private Rigidbody2D rb;
    private Vector2 platformVelocity;

    public Transform pointA; // Điểm bắt đầu (ví dụ, phía trái)
    public Transform pointB; // Điểm kết thúc (ví dụ, phía phải)
    private bool isMoving = true;         // Hướng di chuyển của kẻ thù
    private Animator animator;

    public float attackRange = 1f; // Khoảng cách tấn công
    public int attackDamage = 20; // Lượng sát thương
    public float attackRate = 1f; // Tần suất tấn công (giới hạn)
    private float nextAttackTime = 0f; // Thời gian tấn công tiếp theo
    
    public Transform attackPoint;
    public LayerMask playerLayer; // Lớp đối tượng của player để va chạm
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        // Di chuyển kẻ thù qua lại
        Move();
        Attack();

    }

    private void Move()
    {
        // Di chuyển kẻ thù qua lại giữa hai điểm
        Vector2 scale = transform.localScale;
        if (isMoving)
        {
            // Di chuyển kẻ thù từ pointA đến pointB
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);
            animator.SetBool("isMoving", true);
            // Nếu kẻ thù đã đến pointB, đổi hướng                                                                                                                        
            if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
            {
                isMoving = false;
                scale.x = -1;
            }
        }
        else
        {
            // Di chuyển kẻ thù từ pointB đến pointA
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
            // Nếu kẻ thù đã đến pointA, đổi hướng                   
            if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
            {
                isMoving = true;
                scale.x = 1;
            }
        }
        transform.localScale = scale;
        if (isGround)
        {
            rb.velocity = new Vector2(platformVelocity.x, rb.velocity.y);
        }
    }

    private void Attack()
    {
        // Kiểm tra thời điểm kẻ thù có thể tấn công
        if (Time.time >= nextAttackTime)
        {
            // Kiểm tra xem player có trong phạm vi tấn công hay không
            Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

            if (player != null)
            {
                // Gọi phương thức tấn công
                player.GetComponent<Player>().TakeDamage(attackDamage); // Gọi hàm TakeDamage trên Player
                nextAttackTime = Time.time + 1f / attackRate; // Cập nhật thời gian tấn công tiếp theo
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    // Nếu kẻ thù va chạm với nhân vật (Player), có thể gây sát thương
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Gọi hàm gây sát thương hoặc thực hiện hành động khi va chạm với nhân vật
            collision.gameObject.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDeath", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false; // Vô hiệu hóa script khi kẻ địch chết
        Destroy(gameObject, 1f);
    }
}