using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public GameObject leftGun;
    public GameObject rightGun;

    //Регулировка угла наклона 
    public float tilt = 1.5f;
    bool leftUsed = false;

    public GameObject projectile;
    Rigidbody rb;

    float speed = 18.0f;
    float forwardInput = 0;
    float horizontalInput = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void LateUpdate()
    {
        Shoot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Получить данные по вводу от игркоа
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        ////Переместить игрока 
        //transform.Translate(Vector3.forward * speed * Time.deltaTime * forwardInput);
        //transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        ////Повернуть игрока при движении влево и вправо
        //rb.rotation = Quaternion.Euler(
        //    0.0f,
        //    0.0f,
        //    -horizontalInput * 20.0f
        //);


        //Реализовал новую механику перемещения используя RigidBody игрока
        //При использовании напрямую Transofrm получаю странное поведение при поворотах игрока
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Задать вектор движения и умножить на скорость игрока. 
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;


        //Задать вращение при движении по оси X. Угол наклона регулируется через параметр tilt
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }


    private void Shoot()
    {
        if (Input.GetKeyDown("space"))
        {
            _ = Instantiate(projectile, GetGun().transform.position, Quaternion.identity);
        }
    }

    private GameObject GetGun()
    {
        if (leftUsed)
        {
            leftUsed = false;
            return rightGun;
        } else
        {
            leftUsed = true;
            return leftGun;
        }
    }

}
