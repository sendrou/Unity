using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 1f; // Скорость движения облака

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Двигаем облако вправо со скоростью speed
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Если облако выходит за пределы экрана, уничтожаем его
        if (transform.position.x > Screen.width)
        {
            Destroy(gameObject);
        }
    }
}
