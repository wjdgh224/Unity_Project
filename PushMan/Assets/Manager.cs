using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int Count;
    public float _time;
    bool End;
    
    void OnTriggerEnter(Collider Get) {
        if(Get.GetComponent<Collider>().tag =="Box") {
            Count += 1;
        }
        
        if(Count >= 16) {
            End = true;
        }
    }

    void Update() {
        if(End == false) {
            _time += Time.deltaTime;
        }
    }
}
