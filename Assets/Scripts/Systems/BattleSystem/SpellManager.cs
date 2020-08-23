﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    PlayerManager playerManager;

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
        resourceManager = GetComponentInParent<ResourceManager>();
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
        int feidhAmount = resourceManager.getFeidhAmount();
        switch (currentSpell)
        {
            case Spell.Attack_01:
                if (feidhAmount > spellCost_Attack_01)
                {
                    Instantiate(particle_attack_01, targetPosition, Quaternion.identity);
                    resourceManager.setFeidhAmount(feidhAmount - spellCost_Attack_01);
                }
                break;

            case Spell.Attack_02:
                if (feidhAmount > spellCost_Attack_02)
                {
                    Instantiate(particle_attack_02, targetPosition, Quaternion.identity);
                    resourceManager.setFeidhAmount(feidhAmount - spellCost_Attack_02);
                }
                break;

            case Spell.Buff_01:

                if (feidhAmount > spellCost_Buff_01)
                {
                    Instantiate(particle_buff_01, targetPosition, Quaternion.identity);
                    resourceManager.setFeidhAmount(feidhAmount-spellCost_Buff_01);
                }
                break;
        }


        // if spell is available (timer)


    }
}
