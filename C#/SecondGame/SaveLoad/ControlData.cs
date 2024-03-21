using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlData
{
    private string dash;
    private string granade;
    private string attack;
    private string interact;
    private string ultimate;
    private string dashSecondary;
    private string granadeSecondary;
    private string attackSecondary;
    private string interactSecondary;
    private string ultimateSecondary;


    public ControlData(ControlSettings controls)
    {
        dash = controls.GetDashString();
        granade = controls.GetGranadeString();
        attack = controls.GetAttackString();
        interact = controls.GetInteractString();
        ultimate = controls.GetUltimateString();
        dashSecondary = controls.GetDashSecondaryString();
        granadeSecondary = controls.GetGranadeSecondaryString();
        attackSecondary = controls.GetAttackSecondaryString();
        interactSecondary = controls.GetInteractionSecondaryString();
        ultimateSecondary = controls.GetUltimateSecondaryString();
    }

    public string GetDashString()
    {
        return dash;
    }
    public string GetGranadeString()
    {
        return granade;
    }
    public string GetAttackString()
    {
        return attack;
    }
    public string GetInteractString()
    {
        return interact;
    }
    public string GetUltimateString()
    {
        return ultimate;
    }
    public string GetDashSecondaryString()
    {
        return dashSecondary;
    }
    public string GetGranadeSecondaryString()
    {
        return granadeSecondary;
    }
    public string GetAttackSecondaryString()
    {
        return attackSecondary;
    }
    public string GetInteractSecondaryString()
    {
        return interactSecondary;
    }
    public string GetUltimateSecondaryString()
    {
        return ultimateSecondary;
    }
}
