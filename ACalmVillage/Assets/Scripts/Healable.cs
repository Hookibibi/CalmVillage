using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healable : MonoBehaviour
{
    [SerializeField] private Health health = null;

    public void HealDamage(int damage)
    {
        health.CmdHealDamage(damage);
    }
}
