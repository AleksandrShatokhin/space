using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundEmemyShip : MonoBehaviour
{
    Vector3 moveBounds;
    public float enemyWidth;
    public MeshRenderer mr;

    void Start()
    {
        StartCoroutine(SearchScreenBounds());
    }

    // вызывая сразу напряму в старте наблюдаю ошибку, что сылка не задана на объект
    // сделал небольшую задержку и на данный момент всё работает
    IEnumerator SearchScreenBounds()
    {
        yield return new WaitForSeconds(0.01f);

        moveBounds = GameController.GetInstance().ScreenBound();

        enemyWidth = mr.bounds.size.x / 2;

        moveBounds.x -= enemyWidth;
    }

        void LateUpdate()
    {
        Vector3 boundsTransform = transform.position;

        boundsTransform.x = Mathf.Clamp(transform.position.x, -moveBounds.x, moveBounds.x);
        transform.position = boundsTransform;
    }
}
