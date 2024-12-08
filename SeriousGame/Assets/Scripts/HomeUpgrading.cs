using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SFXScript))]
public class HomeUpgrading : MonoBehaviour
{
    //public
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

    public void UpgradeBtnPressed() //To-do: Call this function when upgrade btn is pressed
    {
        if (_upgradeLevel >= UpgradeCosts.Length) return; //No more upgrades

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
