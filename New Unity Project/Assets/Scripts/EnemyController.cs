using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool broken = true;

    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    Animator animator;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!broken) {
            return;
        }
        timer -= Time.deltaTime;

        if(timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
        Vector2 position = rigidbody2D.position;

        if(vertical) {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other) {
        RubyController player = other.gameObject.GetComponent<RubyController>(); //Trigger에서는 other.GetComponent...

        if(player != null) {
            player.ChangeHealth(-1);
        }
    }

    public void Fix() {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
