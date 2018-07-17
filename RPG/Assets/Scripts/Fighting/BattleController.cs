
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {

    [SerializeField]
    List<CharacterScript> combatants;
    [SerializeField]
    Text heroText;
    [SerializeField]
    Text knightText;
    [SerializeField]
    Text mageText;
    [SerializeField]
    Text medicText;
    [SerializeField]
    Text winText;
    [SerializeField]
    Text actionText;
    [SerializeField]
    Text turnText;

    int combatantIndex = -1;
    CharacterScript currentCombatant;


    private string command;
	// Use this for initialization
	void Start () {
        combatants = SortAfterSpeed(combatants);
        winText.text = "";
        turnText.text = "";
        actionText.text = "";
        NextCombatant();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void NextCombatant()
    {
        UpdateUi();
        if (!CheckForWin())
        {
            combatantIndex++;

            if (combatantIndex >= combatants.Count)
            {
                for (int i = 0; i < combatants.Count; i++)
                {
                    combatants[i].EndOfRoundRoutine();
                    combatantIndex = 0;
                }
            }
            currentCombatant = combatants[combatantIndex];
            if(currentCombatant.tag == "Hero")
            {
                SetTurnText();
            }

            if (currentCombatant.isDead)
            {
                combatants.Remove(currentCombatant);
                NextCombatant();
            }

            if (currentCombatant.tag == "Villain")
            {
                float waitingTime = 1f;
                StartCoroutine(VillainMove(waitingTime));
            }
        }
        else
        {
            CheckWhoWon();
        }
    }

    public void Attack()
    {
        
        string targetTag;
        if (currentCombatant.tag == "Hero")
            targetTag = "Villain";
        else
            targetTag = "Hero";
        CharacterScript target = findRandomTarget(targetTag);
        currentCombatant.Attack(target);
        actionText.text = "a " + currentCombatant.tag + " attacked!";
        NextCombatant();
    }

    public void Defend()
    {
        currentCombatant.defend();
        actionText.text = "a " + currentCombatant.tag + " defended!";
        NextCombatant();
    }

    public void Item()
    {
        currentCombatant.Item();
        actionText.text = "a " + currentCombatant.tag + " used his healpot!";
        NextCombatant();
    }

    public void Ability()
    {
        CharacterScript target = findRandomTarget(currentCombatant.GetAbilityTargetTag());
        currentCombatant.Ability(target);
        actionText.text = "a " + currentCombatant.tag + " used his ability!";
        NextCombatant();
    }

    List<CharacterScript> SortAfterSpeed(List<CharacterScript> combatants)
    {
        for(int i = 1; i < combatants.Count; i++ )
        {
            CharacterScript swapOut;
            if (i >= 1)
            {
                if (combatants[i].GetSpeed() > combatants[i - 1].GetSpeed())
                {
                    swapOut = combatants[i - 1];
                    combatants[i - 1] = combatants[i];
                    combatants[i] = swapOut;
                    i -= 2;
                }
            }
        }
        return combatants;
    }

    private void UpdateUi()
    {
        foreach(CharacterScript character in combatants)
        {
            if(character is SwordFighterScript)
            {
                heroText.text = "Hero: \n Health: " + character.GetHealth() + "\n" + "Mana: " + character.GetMana() + "\n" + "Pots: " + character.GetItemCount();
            }

            if (character is MageScript)
            {
                mageText.text = "Mage: \n Health: " + character.GetHealth() + "\n" + "Mana: " + character.GetMana() + "\n" + "Pots: " + character.GetItemCount();
            }

            if (character is ParamedicScript)
            {
                medicText.text = "Paramedic: \n Health: " + character.GetHealth() + "\n" + "Mana: " + character.GetMana() + "\n" + "Pots: " + character.GetItemCount();
            }

            if (character is KnightScript)
            {
                knightText.text = "Knight: \n Health: " + character.GetHealth() + "\n" + "Mana: " + character.GetMana() + "\n" + "Pots: " + character.GetItemCount();
            }
        }
    }

    private bool CheckForWin()
    {
        string firstTag = combatants[0].tag;
        for (int i = 0; i < combatants.Count; i++)
        {
            if (combatants[i].isDead)
            {
                combatants.Remove(combatants[i]);
                i--;
            }
            else {
                if (!combatants[i].gameObject.CompareTag(firstTag))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void CheckWhoWon()
    {
        
        if (combatants[0].gameObject.CompareTag("Hero"))
        {
            winText.text = "You win!";
            for(int i = 0; i < combatants.Count; i++)
            {
                combatants[i].EndOfBattleRoutine();
            }
            SceneManager.LoadScene("Dungeon");
        }
        else
        {
            SceneManager.LoadScene("MainMenue");
        }
    }

    private IEnumerator VillainMove(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        int attackPointer = Random.Range(0, 10);
        if (attackPointer <= 4)
        {
            Attack();
        }
        else if (attackPointer <= 8)
        {
            Ability();
        }
        else
        {
            Defend();
        }
        yield return null;
    }

    private CharacterScript findRandomTarget(string targetTag)
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < combatants.Count; i++)
        {
            indices.Add(i);
        }
        int[] randomIndices = new int[combatants.Count];

        for (int i = 0; i < combatants.Count; i++)
        {
            int accessIndex = Random.Range(0, indices.Count);
            randomIndices[i] = indices[accessIndex];
            indices.RemoveAt(accessIndex);
        }

        for (int i = 0; i < combatants.Count; i++)
        {
            CharacterScript target = combatants[randomIndices[i]];
            if (target.tag == targetTag && !target.isDead)
                return target;
        }
        CheckWhoWon();
        return null;
    }

    private void SetTurnText()
    {
       
            if (currentCombatant is SwordFighterScript)
            {
                turnText.text = "Hero's turn";
            }

            if (currentCombatant is MageScript)
            {
            turnText.text = "Mage's turn";
            }

            if (currentCombatant is ParamedicScript)
            {
            turnText.text = "Medic's turn";
            }

            if (currentCombatant is KnightScript)
            {
            turnText.text = "Knight's turn";
            }
        
    }
}
