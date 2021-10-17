using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public GameObject leftGun;
    public GameObject rightGun;
    bool leftUsed = false;

    public GameObject projectile;

    float speed = 18.0f;
    float forwardInput = 0;
    float horizontalInput = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
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

        //Переместить игрока 
        transform.Translate(Vector3.forward * speed * Time.deltaTime * forwardInput);
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);
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
