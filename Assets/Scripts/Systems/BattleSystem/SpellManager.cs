using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField]
    ResourceManager resourceManager;

    [SerializeField]
    GameObject particle_attack_01;

    [SerializeField]
    GameObject particle_attack_02;

    [SerializeField]
    GameObject particle_buff_01;

    int spellCost_Attack_01 = 75;
    int spellCost_Attack_02 = 150;
    int spellCost_Buff_01 = 50;

    public enum Spell
    {
        Attack_01,
        Attack_02,
        Buff_01,
    }

    int spellCost = 50;

    public Spell currentSpell = Spell.Buff_01;

    // Start is called before the first frame update
    void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeCurrentSpell(int spellNumber)
    {
        switch (spellNumber)
        {
            case 1:
                currentSpell = Spell.Attack_01;
                break;
            case 2:
                currentSpell = Spell.Attack_02;
                break;
            case 3:
                currentSpell = Spell.Buff_01;
                break;
        }
        playerManager.ChangeState(PlayerManager.State_Player.CastingSpell);
    }

    public void CastSpellOnPosition(Vector3 targetPosition)
    {
        int feidhAmount = resourceManager.GetFeidhAmount();
        switch (currentSpell)
        {
            case Spell.Attack_01:
                //Kills all soliders in range instantly
                if (feidhAmount > spellCost_Attack_01)
                {
                    GameObject particle = Instantiate(particle_attack_01, targetPosition, Quaternion.identity);
                    particle.GetComponent<KillAllEnemies>().DoSpell();
                    resourceManager.SetFeidhAmount(feidhAmount - spellCost_Attack_01);
                    Destroy(particle, 5);
                }
                break;

            case Spell.Attack_02:
                //Freezes Enemies around
                if (feidhAmount > spellCost_Attack_02)
                {
                    GameObject particle = Instantiate(particle_attack_02, targetPosition, Quaternion.identity);
                    particle.GetComponent<FreezeEnemies>().DoSpell();
                    resourceManager.SetFeidhAmount(feidhAmount - spellCost_Attack_02);
                    Destroy(particle, 5);
                }
                break;

            case Spell.Buff_01:

                if (feidhAmount > spellCost_Buff_01)
                {
                    GameObject particle = Instantiate(particle_buff_01, targetPosition, Quaternion.identity);
                    resourceManager.SetFeidhAmount(feidhAmount-spellCost_Buff_01);
                    Destroy(particle, 5);
                }
                break;
        }
        playerManager.ChangeState(PlayerManager.State_Player.Default);


        // if spell is available (timer)


    }
}
