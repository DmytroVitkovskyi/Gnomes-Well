using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Вызывает UnityEvent, когда объект с тегом "Player" касается
// данного объекта.
[RequireComponent(typeof(Collider2D))]
public class SignalOnTouch : MonoBehaviour
{
    // UnityEvent для выполнения в ответ на касание.
    // Вызываемый метод подключается в редакторе.
    public UnityEvent onTouch;

    // Если имеется значение true, при касании проигрывается звук из AudioSource.
    public bool playAudioOnTouch = true;
    // Когда обнаруживается вход в область действия триггера,
    // вызывается SendSignal.
    void OnTriggerEnter2D(Collider2D collider)
    {
        SendSignal(collider.gameObject);
    }

    // Когда обнаруживается касание с данным объектом,
    // вызывается SendSignal.
    void OnCollisionEnter2D(Collision2D collision)
    {
        SendSignal(collision.gameObject);
    }

    // Проверяет наличие тега "Player" у данного объекта и
    // вызывает UnityEvent, если такой тег имеется.
    void SendSignal(GameObject objectThatHit)
    {
        // Объект отмечен тегом "Player"?
        if (objectThatHit.CompareTag("Player"))
        {
            // Если требуется воспроизвести звук, попытаться сделать это
            if (playAudioOnTouch)
            {
                var audio = GetComponent<AudioSource>();
                // Если имеется аудиокомпонент
                // и родитель этого компонента активен,
                // воспроизвести звук
                if (audio && audio.gameObject.activeInHierarchy)
                    audio.Play();
            }
            // Вызвать событие
            onTouch.Invoke();
        }
    }
}
