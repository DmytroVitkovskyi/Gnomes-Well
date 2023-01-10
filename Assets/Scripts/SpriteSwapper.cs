using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Меняет один спрайт на другой. Например, при переключении сокровища
// из состояния 'сокровище есть' в состояние 'сокровища нет'.
public class SpriteSwapper : MonoBehaviour
{
    // Спрайт, который требуется отобразить.
    public Sprite spriteToUse;
    // Визуализатор спрайта, который должен использоваться
    // для отображения нового спрайта.
    public SpriteRenderer spriteRenderer;
    // Исходный спрайт. Используется в вызове ResetSprite.
    private Sprite originalSprite;

    // Меняет спрайт.
    public void SwapSprite()
    {
        // Если требуемый спрайт отличается от текущего...
        if (spriteToUse != spriteRenderer.sprite)
        {
            // Сохранить предыдущий в originalSprite
            originalSprite = spriteRenderer.sprite;
            // Передать новый спрайт визуализатору.
            spriteRenderer.sprite = spriteToUse;
        }
    }
    // Возвращает прежний спрайт.
    public void ResetSprite()
    {
        // Если прежний спрайт был сохранен...
        if (originalSprite != null)
        {
            // ...передать его визуализатору.
            spriteRenderer.sprite = originalSprite;
        }
    }
}
