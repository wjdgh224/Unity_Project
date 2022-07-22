using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10; //초당 10프레임, Time.deltaTime적용시 프레임에만 영향을 끼침
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x = position.x + 3.0f * horizontal * Time.deltaTime; //1프레임 당 시간을 곱해 평준화
        position.y = position.y + 3.0f * vertical * Time.deltaTime; //초당 0.1유닛 -> 3.0
        transform.position = position;
    }
}
