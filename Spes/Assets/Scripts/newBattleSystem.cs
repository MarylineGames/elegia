using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BattleState
{
    START,
    PLAYER_TURN,
    ENEMY_TURN,
    WON,
    LOST
}
public class newBattleSystem : MonoBehaviour
{
    public GameObject playerPrefab = default;
    public GameObject enemyPrefab = default;

    public Transform playerBattleStation = default;
    public Transform enemyBattleStation = default;

    public BattleState state = default;

    public TMP_Text text_battle = default;

    [Header("Battle HUDs")]
    public newBattleHUD playerBattleHud = default;
    public newBattleHUD enemyBattleHud = default;

    private newUnit playerUnit = default;
    private newUnit enemyUnit = default;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponent<newUnit>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<newUnit>();

        text_battle.text = "O inimigo " + enemyUnit.UnitName + " esta pronto para a batalha";

        playerBattleHud.SetHUD(playerUnit);
        enemyBattleHud.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYER_TURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        text_battle.text = "Hora de batalhar!";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYER_TURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYER_TURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    private IEnumerator PlayerHeal()
    {
        playerUnit.HealUnit(playerUnit.HealAmount);
        playerBattleHud.SetHP(playerUnit.CurrentHp);

        text_battle.text = "Ariel esta se sentindo mais forte...";

        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMY_TURN;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator PlayerAttack()
    {
        // Adicionar dano ao inimigo
        bool isDead = enemyUnit.TakeDamage(playerUnit.Damage);
        enemyBattleHud.SetHP(enemyUnit.CurrentHp);
        text_battle.text = "O ataque foi certeiro!";

        yield return new WaitForSeconds(2f);

        if (isDead == true)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMY_TURN;
            StartCoroutine(EnemyTurn());
        }
    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            text_battle.text = "A temida lenda foi derrota";
        }
        else if (state == BattleState.LOST)
        {
            text_battle.text = "Ariel foi derrotado por " + enemyUnit.UnitName;
        }
    }

    private IEnumerator EnemyTurn()
    {
        text_battle.text = enemyUnit.UnitName + " esta atacando!";
        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.Damage);
        playerBattleHud.SetHP(playerUnit.CurrentHp);

        yield return new WaitForSeconds(1f);
        text_battle.text = " Ariel foi atingido";

        yield return new WaitForSeconds(2f);

        if (isDead == true)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYER_TURN;
            PlayerTurn();
        }
    }
}
