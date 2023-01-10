using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Использует диспетчер ввода для приложения боковой силы
// к объекту. Используется для отклонения гномика в сторону.
public class Swinging : MonoBehaviour
{
    // Насколько большим должно быть отклонение?
    // Больше число = больше отклонение
    public float swingSensitivity = 100.0f;
    
    // Вместо Update используется FixedUpdate, чтобы
    // упростить работу с физическим движком
    private void FixedUpdate()
    {
        // Если твердое тело отсутствует (уже), удалить
        // этот компонент
        if (GetComponent<Rigidbody2D>() == null)
        {
            Destroy(this);
            return;
        }

        // Получить величину наклона из InputManager
        float swing = InputManager.instance.sidewaysMotion;

        // Вычислить прилагаемую силу
        Vector2 force = new Vector2(swing * swingSensitivity, 0);

        // Приложить силу
        GetComponent<Rigidbody2D>().AddForce(force);
    }
}
