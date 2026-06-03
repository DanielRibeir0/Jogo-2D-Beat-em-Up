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

        RectTransform parentRect = GetComponent<RectTransform>();
        maxWidth = parentRect.sizeDelta.x;

        targetWidth = maxWidth;

        SetWidth(lifeBar, maxWidth);
        SetWidth(redBar, maxWidth);
    }

    public void OnHealthChanged(int current, int max)
    {
        if (max <= 0) return;

        float percent = Mathf.Clamp01((float)current / max);
        targetWidth = maxWidth * percent;

        SetWidth(lifeBar, targetWidth);

        if (redBar.sizeDelta.x > targetWidth)
            timer = redBarDelay;
        else
            SetWidth(redBar, targetWidth);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        float width = Mathf.MoveTowards(
            redBar.sizeDelta.x,
            targetWidth,
            redBarSpeed * Time.deltaTime
        );

        SetWidth(redBar, width);
    }

    private void SetWidth(RectTransform rect, float width)
    {
        Vector2 size = rect.sizeDelta;
        size.x = width;
        rect.sizeDelta = size;
    }
}