using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HealthDisplay : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private Health health = null;
    [SerializeField] private Image healthBarImage = null;

    private void OnEnable()
    {
        health.OnHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= HandleHealthChanged;
    }
    private void HandleHealthChanged(object sender, HealthChangedEventArgs e)
    {
        Debug.Log((float)e.CurrentHealth / e.MaxHealth);
        healthBarImage.fillAmount = (float)e.CurrentHealth / e.MaxHealth;
    }
}
