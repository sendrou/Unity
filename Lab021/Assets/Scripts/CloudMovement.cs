using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 1f; // �������� �������� ������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // ������� ������ ������ �� ��������� speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // ���� ������ ������� �� ������� ������, ���������� ���
        if (transform.position.x > Screen.width)
        {
            Destroy(gameObject);
        }
    }
}
