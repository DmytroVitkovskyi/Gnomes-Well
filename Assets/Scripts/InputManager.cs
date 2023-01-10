using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ѕреобразует данные, полученные от акселерометра,
// в информацию о боковом смещении.
public class InputManager : Singleton<InputManager>
{
    // ¬еличина смещени€. -1.0 = максимально влево,
    // +1.0 = максимально вправо
    private float _sidewaysMotion = 0.0f;

    // Ёто свойство доступно только дл€ чтени€, поэтому
    // другие сценарии не смогут изменить его.
    public float sidewaysMotion { get => _sidewaysMotion; }

    // ¬еличина отклонени€ сохран€етс€ в каждом кадре
    void Update()
    {
        Vector3 accel = Input.acceleration;

        _sidewaysMotion = accel.x;
    }
}
