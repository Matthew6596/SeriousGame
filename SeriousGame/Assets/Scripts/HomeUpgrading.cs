using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(SFXScript))]
public class HomeUpgrading : MonoBehaviour
{
    //public
    public TextMeshProUGUI upgradeBtnText;

    public UnityEvent[] OnUpgradeEvent;
    public int[] UpgradeCosts;

    //private
    SFXScript sfx;
    int _upgradeLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<SFXScript>();
    }

    private void Update()
    {
        if(_upgradeLevel < UpgradeCosts.Length)
        {
            upgradeBtnText.text = $"Upgrade?\nCost: {UpgradeCosts[_upgradeLevel]}\nPoints: {MinigameManager.upgradePoints}";
        }
        else
        {
            upgradeBtnText.text = $"Fully\nUpgraded!\nPoints: {MinigameManager.upgradePoints}";
        }
        
    }

    public void UpgradeBtnPressed() //To-do: Call this function when upgrade btn is pressed
    {
        if (_upgradeLevel >= UpgradeCosts.Length)
        {
            sfx.PlayNegative();
            return; //No more upgrades
        }

        if (MinigameManager.upgradePoints >= UpgradeCosts[_upgradeLevel]) //Can upgrade!
        {
            MinigameManager.upgradePoints -= UpgradeCosts[_upgradeLevel];
            OnUpgradeEvent[_upgradeLevel].Invoke();
            _upgradeLevel++;
            sfx.PlayUpgrade();
        }
        else //Not enough points to upgrade
        {
            sfx.PlayNegative();
        }
    }
    
}
