using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Синхронизирует позицию камеры с позицией Y
// целевого объекта, соблюдая некоторые ограничения.
public class CameraFollow : MonoBehaviour
{
    // Целевой объект, с позицией Y которого будет
    // синхронизироваться положение камеры.
    public Transform target;

    // Наивысшая точка, где может находиться камера.
    public float topLimit = 10.0f;

    // Низшая точка, где может находиться камера.
    public float bottomLimit = -10.0f;

    // Скорость следования за целевым объектом.
    public float followSpeed = 0.5f;

    // Определяет положение камеры после установки
    // позиций всех объектов
    private void LateUpdate()
    {
        // Если целевой объект определен...
        if (target != null)
        {
            // Получить его позицию
            Vector3 newPosition = this.transform.position;

            // Определить, где камера должна находиться
            newPosition.y = Mathf.Lerp(newPosition.y, target.position.y, followSpeed);

            // Предотвратить выход позиции за граничные точки
            newPosition.y = Mathf.Min(newPosition.y, topLimit);
            newPosition.y = Mathf.Max(newPosition.y, bottomLimit);

            // Обновить местоположение
            transform.position = newPosition;
        }
    }

    // Если камера выбрана в редакторе, рисует линию от верхней
    // граничной точки до нижней.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 topPoint = new Vector3(this.transform.position.x,
        topLimit, this.transform.position.z);
        Vector3 bottomPoint = new Vector3(this.transform.position.x,
        bottomLimit, this.transform.position.z);
        Gizmos.DrawLine(topPoint, bottomPoint);
    }
}
