using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Управляет главным меню.
public class MainMenu : MonoBehaviour
{
    // Имя сцены, содержащей саму игру.
    public string sceneToLoad;
    // Компонент пользовательского интерфейса,
    // содержащий текст "Loading...".
    public RectTransform loadingOverlay;
    // Выполняет загрузку сцены в фоновом режиме. Используется
    // для управления, когда требуется переключить сцену.
    AsyncOperation sceneLoadingOperation;
    // При запуске начинает загрузку игры.
    void Start()
    {
        // Скрыть заставку 'loading'
        loadingOverlay.gameObject.SetActive(false);
        // Начать загрузку сцены в фоновом режиме...
        sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        // ...но не переключаться в новую сцену,
        // пока мы не будем готовы.
        sceneLoadingOperation.allowSceneActivation = false;
    }

    // Вызывается в ответ на касание кнопки New Game.
    public void LoadScene()
    {
        // Сделать заставку 'Loading' видимой
        loadingOverlay.gameObject.SetActive(true);
        // Сообщить операции загрузки сцены, что требуется
        // переключить сцены по окончании загрузки.
        sceneLoadingOperation.allowSceneActivation = true;
    }
}
