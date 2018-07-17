using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {

    protected int health;
    protected int maxHealth;
    protected int mana;
    protected int maxMana;
    protected float protection;
    protected int dmg;
    protected int manaregen;
    protected int healthregen;
    protected float speed;
    protected string abilityTargetTag;
    protected int manaCost;
    protected int itemCount;
    protected float moveTime = 0.5f;
    protected int maxItemCount = 3;

    public bool isDead;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public virtual void Ability(CharacterScript target)
    {

    }

    public void Attack(CharacterScript target)
    {
        StartCoroutine(AttackDash(target, moveTime));
    }


    public virtual IEnumerator AttackDash(CharacterScript target, float moveTime)
    {
        Vector3 oldPos = transform.position;
        StartCoroutine(MoveTo(target.transform.position, moveTime/2));
        yield return new WaitForSeconds(moveTime / 2);
        target.TakeDmg(dmg);
        StartCoroutine(MoveTo(oldPos, moveTime/2));
        yield return new WaitForSeconds(moveTime / 2);
        transform.position = oldPos;
        yield return null;
    }

    public IEnumerator MoveTo(Vector3 targetPos, float moveTime)
    {
        float dist = (targetPos - transform.position).magnitude;
        float speed = dist / moveTime;
        while (dist > 1.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            dist = (targetPos - transform.position).magnitude;
            yield return null;
        }
    }

    public virtual void TakeDmg(int dmgTaken)
    {
        health -= (int) (dmgTaken - (dmgTaken * protection));
        resetProtection();
        CheckForDeath();
    }

    public virtual void CheckForDeath()
    {
        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
        }
        else
        {
            isDead = false;
        }
    }

    public void Healed(int lifeHealed)
    {
        health += lifeHealed;
    }

    public void ProtectionIncrease (float protectionIncrease)
    {
        protection += protectionIncrease;
        if (protection > 1.0f)
            protection = 1.0f;
    }

    public void resetProtection()
    {
        protection = 0;
    }

    public void defend()
    {
        this.ProtectionIncrease(0.5f);
    }

    public virtual void Item()
    {

    }

    public void ReduceMana(int manaLost)
    {
        mana -= manaLost;
    }

    public string GetAbilityTargetTag()
    {
        return abilityTargetTag;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMana()
    {
        return mana;
    }

    public void EndOfRoundRoutine()
    {
        mana += manaregen;
        resetProtection();
    }

    public void EndOfBattleRoutine()
    {
        mana = maxMana;
        health += healthregen;
        SetPrefs();
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    public virtual void SetPrefs()
    {
            //PlayerPrefs.SetInt("heroHealth", health);
            //PlayerPrefs.SetInt("heroItemCount", itemCount);
    }

    public virtual IEnumerator AttackTest(Vector3 target)
    {
        float moveTime = 1f;
        Vector3 oldPos = transform.position;
        StartCoroutine(MoveTo(target, moveTime/2));
        yield return new WaitForSeconds(moveTime / 2);
        //target.TakeDmg(dmg);
        Debug.Log("Leide!");
        /*notThereYet = true;
        while (notThereYet)
        {
            transform.position = Vector3.MoveTowards(transform.position, oldPos, stepDistance * Time.deltaTime);
            float dist = (oldPos - transform.position).magnitude;
            if (dist < 1f)
                notThereYet = false;
            yield return null;
        }*/
        StartCoroutine(MoveTo(oldPos, moveTime/2));
        yield return new WaitForSeconds(moveTime / 2);
        transform.position = oldPos;
        yield return null;
    }
}
