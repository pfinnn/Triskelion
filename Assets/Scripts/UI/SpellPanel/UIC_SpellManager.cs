using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIC_SpellManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer_01_TextField;
    [SerializeField]
    private TextMeshProUGUI timer_02_TextField;
    [SerializeField]
    private TextMeshProUGUI timer_03_TextField;

    private SpellManager spellManager;

    [SerializeField]
    AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
       spellManager = this.GetComponentInParent<SpellManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSpellButtonClicked(int spellNumber)
    {
        spellManager.ChangeCurrentSpell(spellNumber);
        audioManager.PlayUISound(AudioManager.UISoundTypes.ButtonClicked);
    }

}
