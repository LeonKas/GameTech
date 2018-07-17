using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageScript : CharacterScript {

    [SerializeField]
    GameObject fireBall;

    int abilityDmg = 50;
    int itemStrength = 15;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("mageDeath") == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            abilityTargetTag = "Villain";
            maxHealth = 190;
            health = PlayerPrefs.GetInt("mageHealth", maxHealth);
            protection = 0f;
            dmg = 15;
            speed = 0.5f;
            maxMana = 150;
            mana = maxMana;
            manaregen = 10;
            manaCost = 20;
            healthregen = 30;
            itemCount = PlayerPrefs.GetInt("mageItemCount", maxItemCount);
        }
    }

    public override void Ability(CharacterScript target)
    {
        if (mana >= manaCost)
        {
            //target.TakeDmg(abilityDmg);
            ReduceMana(manaCost);
            FireBallScript fireAttack = Instantiate(fireBall).GetComponent<FireBallScript>();
            fireAttack.gameObject.transform.position = transform.position;
            fireAttack.FireAttack(target, abilityDmg);
        }
    }

    public override void Item()
    {
        if (itemCount > 0)
        {
            this.Healed(itemStrength);
            itemCount -= 1;
        }
    }

    public override void SetPrefs()
    {
        PlayerPrefs.SetInt("mageHealth", health);
        PlayerPrefs.SetInt("mageItemCount", itemCount);
    }

    public override void CheckForDeath()
    {
        base.CheckForDeath();
        if (isDead)
        {
            PlayerPrefs.SetInt("mageDeath", 1);
        }

    }


}
