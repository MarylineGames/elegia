using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPbar hpBar;

    public void SetData(Pokemon pokemon)
    {
        nameText.text = pomeon.Base.Name;
        levelText.text = "Lvl" + pokemon.Level;
        hpBar.SetHP((float)pokemon.HP / pokemon.MaxHp);
    }

}
