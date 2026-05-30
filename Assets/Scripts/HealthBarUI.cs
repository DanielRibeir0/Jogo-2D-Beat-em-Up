using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [Header("Referências")]
    public RectTransform lifeBar;
    public RectTransform redBar;

    [Header("Animação")]
    public float redBarDelay = 0.5f;
    public float redBarSpeed = 300f;

    private float maxWidth;
    private float targetWidth;
    private float timer;

    private void Start()
    {
        if (lifeBar == null || redBar == null)
        {
            Debug.LogError("Atribua Life Bar e Red Bar no Inspector.");
            enabled = false;
            return;
        }

        maxWidth = lifeBar.sizeDelta.x;
        targetWidth = maxWidth;

        Vector2 size = lifeBar.sizeDelta;
        size.x = maxWidth;
        lifeBar.sizeDelta = size;

        size = redBar.sizeDelta;
        size.x = maxWidth;
        redBar.sizeDelta = size;
    }

    public void OnHealthChanged(int current, int max)
    {
        float percent = Mathf.Clamp01((float)current / max);

        targetWidth = maxWidth * percent;

        // Barra verde muda instantaneamente
        Vector2 lifeSize = lifeBar.sizeDelta;
        lifeSize.x = targetWidth;
        lifeBar.sizeDelta = lifeSize;

        // Se perdeu vida, a vermelha espera antes de seguir
        if (redBar.sizeDelta.x > targetWidth)
        {
            timer = redBarDelay;
        }
        else
        {
            // Cura: as duas acompanham imediatamente
            Vector2 redSize = redBar.sizeDelta;
            redSize.x = targetWidth;
            redBar.sizeDelta = redSize;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        Vector2 redSize = redBar.sizeDelta;

        redSize.x = Mathf.MoveTowards(
            redSize.x,
            targetWidth,
            redBarSpeed * Time.deltaTime
        );

        redBar.sizeDelta = redSize;
    }
}