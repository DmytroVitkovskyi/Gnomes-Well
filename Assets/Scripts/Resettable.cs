using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Содержит поле UnityEvent, которое используется для
// установки объекта this в исходное состояние.
public class Resettable : MonoBehaviour
{
    // В редакторе подключите это событие к методам, которые должны
    // вызываться в момент сброса игры.
    public UnityEvent onReset;
    // Вызывается диспетчером игры GameManager в момент сброса игры.
    public void Reset()
    {
        // Породить событие, которое вызовет все
        // подключенные методы.
        onReset.Invoke();
    }
}
