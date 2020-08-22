using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{

    SpellManager spm;

    public enum State_Player
    {
        Default,
        CastingSpell,
    }

    public State_Player currentState = State_Player.Default;

    private void Awake()
    {
        spm = GetComponentInParent<SpellManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentState = State_Player.Default;
        }

        switch (currentState)
        {
            case State_Player.Default:
                break;

            case State_Player.CastingSpell:
                if (Input.GetMouseButtonDown(0))
                {
                    if (EventSystem.current.IsPointerOverGameObject()) return;
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Terrain")))
                    {
                        spm.CastSpellOnPosition(hit.point);
                    }
                }
                break;
        }

    }

    public void ChangeState(State_Player state)
    {
        switch (state)
        {
            case State_Player.Default:
                currentState = State_Player.Default;
                break;
            case State_Player.CastingSpell:
                currentState = State_Player.CastingSpell;
                break;
        }
    }
}
