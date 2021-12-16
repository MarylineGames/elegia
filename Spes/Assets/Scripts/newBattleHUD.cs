using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class newBattleHUD : MonoBehaviour
{
    public TMP_Text nameText = default;
    public TMP_Text levelText = default;
    public Slider hpSlider = default;

    public void SetHUD(newUnit unit)
    {
        nameText.text = unit.UnitName;
        levelText.text = "LEVEL " + unit.UnityLevel.ToString();
        hpSlider.maxValue = unit.MaxHp;
        hpSlider.value = unit.CurrentHp;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
