using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public GameObject projectilePrefab;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public float speed = 3.0f;

    public int maxHealth = 5;
    
    public int health { get { return currentHealth; }} //읽기 전용
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10; //초당 10프레임, Time.deltaTime적용시 프레임에만 영향을 끼침
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new  Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2D.position; //충동시 떨림 없음
        // position.x = position.x + speed * horizontal * Time.deltaTime; //1프레임 당 시간을 곱해 평준화
        // position.y = position.y + speed * vertical * Time.deltaTime; //초당 0.1유닛 -> 3.0
        position = position + move * speed * Time.deltaTime;
        
        rigidbody2D.MovePosition(position);

        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0) {
                isInvincible = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.C)) {
            Launch();
        }
    }

    public void ChangeHealth(int amount) {
        if(amount < 0) {
            animator.SetTrigger("Hit");
            if(isInvincible)
                return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void Launch() {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}
