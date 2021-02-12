using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Damageable : NetworkBehaviour
{
    [SerializeField] private Health health = null;

    public void TakeDamage(int damage)
    {
        health.CmdDealDamage(damage);
    }
}
