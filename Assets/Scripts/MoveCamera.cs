using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 따라갈 속도
    public float minX, maxX, minY, maxY; // Boundary limits for the camera
    private Vector3 targetPosition; // 대상의 현재 위치

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 대상이 있는지 체크
        if (target.gameObject != null)
        {
            // this는 카메라를 의미 (z값은 카메라값을 그대로 유지)
            targetPosition.Set(
                Mathf.Clamp(target.transform.position.x, minX, maxX),
                Mathf.Clamp(target.transform.position.y, minY, maxY),
                this.transform.position.z
            );

            // vectorA -> B까지 T의 속도로 이동
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}