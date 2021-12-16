using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newUnit : MonoBehaviour
{
    public string UnitName = default;
    public int UnityLevel;
    public int Damage;
    public int MaxHp;
    public int CurrentHp;
    public int HealAmount;

    public bool TakeDamage(int damage)
    {
        CurrentHp -= damage;

        if (CurrentHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HealUnit(int amount)
    {
        CurrentHp += amount;
        if (CurrentHp >= MaxHp)
        {
            CurrentHp = MaxHp;
        }
    }
}
