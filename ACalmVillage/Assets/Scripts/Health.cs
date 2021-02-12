using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar(hook = nameof(HandleHealthUpdated))]
    private int currentHealth = 0;

    public static event EventHandler<DeathEventArgs> OnDeath;
    public event EventHandler<HealthChangedEventArgs> OnHealthChanged;

    public bool isDead => currentHealth == 0;

    public override void OnStartServer()
    {
        currentHealth = maxHealth;
    }

    [ServerCallback]
    private void OnDestroy()
    {
        OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });

    }

    [Server]
    public void Add(int value)
    {
        value = Mathf.Max(value, 0);
        currentHealth = Mathf.Min(currentHealth + value, maxHealth);
    }

    [Server]
    public void Remove(int value)
    {
        value = Mathf.Max(value, 0);
        currentHealth = Mathf.Max(currentHealth - value, 0);
        if (currentHealth == 0)
        {
            OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
            RpcHandleDeath();
        }
    }

    private void HandleHealthUpdated(int oldValue, int newValue)
    {
        OnHealthChanged?.Invoke(this, new HealthChangedEventArgs
        {
            CurrentHealth = currentHealth,
            MaxHealth = maxHealth
        });
    }

    [ClientRpc]
    private void RpcHandleDeath()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<PlayerMovementController>().enabled = false;
        gameObject.GetComponent<PlayerAttackController>().enabled = false;
        gameObject.GetComponent<CharacterController>().enabled = false;
    }

    [Command(ignoreAuthority = true)]
    public void CmdDealDamage(int value) => Remove(value);

    [Command(ignoreAuthority = true)]
    public void CmdHealDamage(int value) => Add(value);

    [ClientCallback]
    private void Update()
    {
        if (!hasAuthority) { return; }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Take damage");
            CmdDealDamage(10);
        }
    }
}
