using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Удаляет объект с заданной задержкой.
public class RemoveAfterDelay : MonoBehaviour
{
    // Задержка в секундах перед удалением.
    public float delay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Запустить сопрограмму 'Remove'.
        StartCoroutine("Remove");
    }

    IEnumerator Remove()
    {
        // Ждать 'delay' секунд и затем уничтожить объект
        // gameObject, присоединенный к объекту this.
        yield return new WaitForSeconds(delay);

        // var body = gameObject.transform.Find("Body");
        //if (body != null)
        //{
        //     print("Body fined!");
        //}
        Destroy(gameObject);

        // Нельзя использовать вызов Destroy(this) - он уничтожит сам
        // объект сценария RemoveAfterDelay.
    }
}
