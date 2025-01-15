using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float health = 3;
    public float maxHealth = 3;
    private float currentHealth;
    public int score = 0;
    public int coin = 0;
    public string playerName;
    
    //private bool isPoisoned = false;  // Kiểm tra tình trạng bị độc


    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinText;

    private Rigidbody2D rb;
    private bool isGround;
    private bool isPlaying = true;
    private bool isRight = true;

    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public float jumpForce = 400f; // Lực nhảy có thể điều chỉnh
    public float moveSpeed = 2f; // Tốc độ di chuyển có thể điều chỉnh

    private bool isJumping = false;

    private Vector2 platformVelocity;
    private FixedJoint2D fixedJoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Tải dữ liệu người chơi từ PlayerPrefs
        LoadPlayerData();

        currentHealth = health;
        coinText.SetText(coin.ToString());
        healthText.SetText(currentHealth.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scale = transform.localScale;
        animator.SetBool("isRunning", false);

        // Di chuyển sang phải
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunning", true);
            isRight = true;
            scale.x = -1;
            transform.Translate(Vector3.right * 2f * Time.deltaTime);
        }

        // Di chuyển sang trái
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isRunning", true);
            isRight = false;
            scale.x = 1;
            transform.Translate(Vector3.left * 2f * Time.deltaTime);
        }
        transform.localScale = scale;

        if (isGround)
        {
            rb.velocity = new Vector2(platformVelocity.x, rb.velocity.y);
        }

        // Nhảy
        if (Input.GetKey(KeyCode.UpArrow) && isGround)
        {
            Jump();
        }

        // Tấn công
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);  // Đảm bảo rằng nhân vật không di chuyển lên khi đang nhảy từ phía trước hoặc phía sau
        rb.AddForce(new Vector2(0, jumpForce));  // Thêm lực lên để nhảy
        isGround = false;  // Sau khi nhảy, đánh dấu là không còn đứng trên mặt đất
        animator.SetTrigger("Jump");
    }

    void Attack()
    {
        // Attack
        animator.SetTrigger("Attack");

        // Range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGround = false;
        }
    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthText.SetText(currentHealth.ToString());
        //animator.SetTrigger("Hurt");
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;  // Màu đỏ khi bị tấn công
        }

        // Quay lại màu sắc ban đầu sau một khoảng thời gian
        StartCoroutine(ResetColor());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator ResetColor()
    {
        // Đợi 0.2 giây trước khi quay lại màu sắc ban đầu
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;  // Quay lại màu sắc ban đầu (trắng)
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            coin++;
            coinText.SetText(coin.ToString());

            // Mỗi lần lấy coin tăng điểm
            score += 100;
            // Không còn lưu điểm vào PlayerPrefs
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("meat"))
        {
            currentHealth = Mathf.Min(currentHealth + maxHealth * 0.5f, maxHealth);  // Hồi 50% máu
            healthText.SetText(currentHealth.ToString());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("HealthItem"))
        {
            maxHealth += 20;  // Tăng máu tối đa
            health = Mathf.Min(health, maxHealth);  // Giữ máu không vượt quá maxHealth
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("DamageItem"))
        {
            attackDamage += 5;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("trap"))
        {
            TakeDamage(1);
        }
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void Die()
    {
        Debug.Log("Player died!");  // Kiểm tra trong console
        //animator.SetBool("IsDeath", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false; // Vô hiệu hóa script khi nhân vật chết

        DeathFall deathFallScript = FindObjectOfType<DeathFall>();
        if (deathFallScript != null)
        {
            deathFallScript.PlayerDied();  // Gọi hàm PlayerDied trong DeathFall
        }
    }
    // Lưu trữ dữ liệu người chơi vào PlayerPrefs
    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("PlayerScore", score); // Lưu điểm của người chơi
        PlayerPrefs.SetInt("PlayerCoins", coin);  // Lưu coin của người chơi
        PlayerPrefs.SetFloat("PlayerHealth", currentHealth);  // Lưu sức khỏe của người chơi
        PlayerPrefs.SetString("PlayerName", playerName);  // Lưu tên người chơi
        PlayerPrefs.Save();
    }

    // Tải dữ liệu người chơi từ PlayerPrefs
    public void LoadPlayerData()
    {
        score = PlayerPrefs.GetInt("PlayerScore", 0); // Lấy điểm của người chơi
        coin = PlayerPrefs.GetInt("PlayerCoins", 0);  // Lấy số coin của người chơi
        currentHealth = PlayerPrefs.GetFloat("PlayerHealth", health);  // Lấy sức khỏe của người chơi
        playerName = PlayerPrefs.GetString("PlayerName", "Player");  // Lấy tên người chơi
    }

    // Reset toàn bộ dữ liệu game
    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteAll();  // Xóa toàn bộ PlayerPrefs
        score = 0;
        coin = 0;
        currentHealth = health;

        // Cập nhật UI sau khi reset
        coinText.SetText(coin.ToString());
        healthText.SetText(currentHealth.ToString());
    }
}