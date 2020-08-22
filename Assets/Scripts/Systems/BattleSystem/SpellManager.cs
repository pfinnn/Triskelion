using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    PlayerManager pm;

    [SerializeField]
    GameObject particle_attack_01;

    [SerializeField]
    GameObject particle_attack_02;

    [SerializeField]
    GameObject particle_buff_01;

    public enum Spell
    {
        Attack_01,
        Attack_02,
        Buff_01,
    }

    public Spell currentSpell = Spell.Buff_01;

    // Start is called before the first frame update
    void Awake()
    {
        pm = GetComponentInParent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (pm.currentState == PlayerManager.State_Player.CastingSpell )
        //{

        //}
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
        pm.ChangeState(PlayerManager.State_Player.CastingSpell);
    }

    public void CastSpellOnPosition(Vector3 targetPosition)
    {
        switch (currentSpell)
        {
            case Spell.Attack_01:
                Instantiate(particle_attack_01, targetPosition, Quaternion.identity);
                break;
            case Spell.Attack_02:
                Instantiate(particle_attack_02, targetPosition, Quaternion.identity);
                break;
            case Spell.Buff_01:
                Instantiate(particle_buff_01, targetPosition, Quaternion.identity);
                break;
        }



        // if spell is available (timer)


    }
}
