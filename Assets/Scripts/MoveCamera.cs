using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject target; // ī�޶� ���� ���
    public float moveSpeed; // ī�޶� ���� �ӵ�
    public float minX, maxX, minY, maxY; // Boundary limits for the camera
    private Vector3 targetPosition; // ����� ���� ��ġ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ����� �ִ��� üũ
        if (target.gameObject != null)
        {
            // this�� ī�޶� �ǹ� (z���� ī�޶��� �״�� ����)
            targetPosition.Set(
                Mathf.Clamp(target.transform.position.x, minX, maxX),
                Mathf.Clamp(target.transform.position.y, minY, maxY),
                this.transform.position.z
            );

            // vectorA -> B���� T�� �ӵ��� �̵�
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}