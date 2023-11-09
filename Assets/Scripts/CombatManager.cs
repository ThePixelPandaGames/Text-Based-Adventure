using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.Mathematics;
using System;

public class CombatManager : MonoBehaviour
{
    public bool combatInProgress = false;
    private bool playerTurn = true;
    private PlayerCharacter player;
    private Enemy enemy;
    //private GameController gameController;



    private void Start()
    {
        //gameController =  GetComponent<GameController>();  
    }

    private void DisplayCombatInfo(string message, GameController gameController)
    {
        gameController.LogAction(message);
    }

    public void StartCombat(Enemy enemy, bool isPlayerTurn, GameController gameController)
    {
        this.player = gameController.player;
        this.enemy = enemy;
        if (combatInProgress)
        {
            DisplayCombatInfo("Combat is already in progress.", gameController);
            return;
        }

        combatInProgress = true;
        playerTurn = isPlayerTurn;

        gameController.ShowPriotityObject();


        gameController.priorityImage.sprite = enemy.image;
        gameController.priorityInfo.text = enemy.DisplayStats();
       
        DisplayCombatInfo("Battle Start!", gameController);
        StartCoroutine(CombatLoop(gameController));
    }

    private IEnumerator CombatLoop(GameController gameController)
    {
        while (player._Health > 0 && enemy.Health > 0)
        {

            gameController.priorityInfo.text = enemy.DisplayStats();

            gameController.playerInfo.text = player.DisplayInfo();

            yield return new WaitForSeconds(3f);

            if (combatInProgress)
            {
                if (playerTurn)
                {
                    // Player's turn
                    DisplayCombatInfo("--- waiting for your attack ! ---", gameController);
                }
                else
                {
                    // Enemy's turn
                    EnemyAttack(gameController);
                }
            }

            

        }

        combatInProgress = false;

        gameController.HidePriotityObject();

        if (player._Health <= 0)
        {
            HandleGameOver(gameController);
        }
        else
        {
            DisplayCombatInfo("Enemy has been defeated!", gameController);
            player.GainExperience(enemy.expRewards, gameController);
            enemy.defeated = true;
        }




    }

    private void EnemyAttack(GameController gameController)
    {
        Skill enemySkill = enemy.GetRandomSkill(); // Choose a random enemy skill
        int damage = Mathf.Max((int)Math.Round(enemy.attack * enemySkill.attackMultiplier) - player.Defense, 1); // Damage calculation
        enemySkill.ActivateSkillEffect(player, enemy, gameController);
        player.TakeDamage(damage, gameController);
        gameController.LogAttack($"Enemy uses {enemySkill.name} for {damage} damage!");
        gameController.LogAttack(enemySkill.description);

        // Switch back to player's turn
        playerTurn = true;
    }

    public void AttackWithSkill(GameController gameController, string skillName)
    {
        Skill playerSkill = player.GetSkillByName(skillName);
        if (playerSkill != null)
        {
            Debug.Log(player.Attack);
            Debug.Log($"{playerSkill}");  
            Debug.Log(playerSkill.attackMultiplier);
            Debug.Log(player.Attack * playerSkill.attackMultiplier);
            int damage = Mathf.Max((int)Math.Round(player.Attack * playerSkill.attackMultiplier) - enemy.defense, 1); // Damage calculation
            if (player.UseMana(playerSkill.ManaCost, gameController))
            {
                enemy.TakeDamage(damage);
                playerSkill.ActivateSkillEffect(player, enemy, gameController);
                gameController.LogAttack($"Player uses {playerSkill.name} for {damage} damage!");
                gameController.LogAttack(playerSkill.description);
            } else
            {
                gameController.LogError("No enough Mana for this skill!");
            }

            playerTurn = false;
        } 
        else
        {
            gameController.LogError("Invalid skill name. Try again.");
        }
    }

    private void HandleGameOver(GameController gameController)
    {
        gameController.LogError("You have been defeated");
        gameController.LogError("Type 'exit' to return to the main menu or create a new player to start the story from the beginning! Hint: Create [race] [name]");
        gameController.player = null;
        gameController.alreadyCreatedPlayer = false;
        gameController.isGameOver = true;
    }
}
