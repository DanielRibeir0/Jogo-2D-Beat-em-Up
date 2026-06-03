using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("Eventos")]
    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent onDeath;

    private int lastHealth;
    private bool isDead;

    private void Start()
    {
        if (maxHealth <= 0)
            maxHealth = 100;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
            currentHealth = maxHealth;

        lastHealth = currentHealth;

        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (currentHealth != lastHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            onHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0 && !isDead)
            {
                isDead = true;
                onDeath?.Invoke();
            }

            lastHealth = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead || amount <= 0)
            return;

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        onHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            onDeath?.Invoke();
        }

        lastHealth = currentHealth;
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (currentHealth > 0)
            isDead = false;

        onHealthChanged?.Invoke(currentHealth, maxHealth);

        lastHealth = currentHealth;
    }
}