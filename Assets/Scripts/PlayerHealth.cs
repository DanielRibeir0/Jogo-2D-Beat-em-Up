using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Eventos")]
    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent onDeath;

    private int lastHealth;

    private void Start()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0)
            currentHealth = maxHealth;

        lastHealth = currentHealth;

        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void Update()
    {
        // Detecta alterações feitas manualmente no Inspector
        if (currentHealth != lastHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            onHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
                onDeath?.Invoke();

            lastHealth = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - amount);

        onHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
            onDeath?.Invoke();

        lastHealth = currentHealth;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        onHealthChanged?.Invoke(currentHealth, maxHealth);

        lastHealth = currentHealth;
    }
}