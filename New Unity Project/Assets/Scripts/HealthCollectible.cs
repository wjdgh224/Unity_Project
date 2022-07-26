using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>(); //루비 객체에 있는 스크립트
        
        if(controller != null) {
            if(controller.health < controller.maxHealth) {
                controller.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
