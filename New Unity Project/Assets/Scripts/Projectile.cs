using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f) {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force) {
        rigidbody2D.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other) {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if(e != null) {
            e.Fix();
        }
        Debug.Log(other.gameObject);
        Destroy(gameObject);
    }
}
