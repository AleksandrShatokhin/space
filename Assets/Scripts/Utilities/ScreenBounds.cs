using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    //Границы перемещения игрока на экране
    private Vector3 moveBounds;
    private float playerWidth;
    private float playerHeight;
    [SerializeField] private MeshRenderer mr;

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

        playerWidth = mr.bounds.size.x / 2;
        playerHeight = mr.bounds.size.z / 2;

        moveBounds.x -= playerWidth;
        moveBounds.z -= playerHeight;
    }

    // когда появляется(исчезает) босс, необходимо задать новые границы движения
    public void SearchNewScreenBounds()
    {
        StartCoroutine(SearchScreenBounds());
    }

    void LateUpdate()
    {
        //Сохранить текущее положение корабля
        Vector3 boundsTransform = transform.position;

        //Ограничиваем возможное перемещение по экрану через функцию Clamp и устанавливаем новое положение
        boundsTransform.x = Mathf.Clamp(transform.position.x, -moveBounds.x, moveBounds.x);
        boundsTransform.z = Mathf.Clamp(transform.position.z, -moveBounds.z, moveBounds.z);
        transform.position = boundsTransform;
    }
}
