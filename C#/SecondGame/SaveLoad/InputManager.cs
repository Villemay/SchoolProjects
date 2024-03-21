using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private KeyCode dash;
    private KeyCode granade;
    private KeyCode attack;
    private KeyCode interact;
    private KeyCode ultimate;
    private KeyCode dashSecondary;
    private KeyCode granadeSecondary;
    private KeyCode attackSecondary;
    private KeyCode interactSecondary;
    private KeyCode ultimateSecondary;

    public void SetKeys()
    {
        ControlData data = SaveLoadControls.loadControls();
        if (data != null)
        {
            dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetDashString());
            granade = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetGranadeString());
            attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetAttackString());
            interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetInteractString());
            ultimate = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetUltimateString());
            dashSecondary = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetDashSecondaryString());
            granadeSecondary = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetGranadeSecondaryString());
            attackSecondary = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetAttackSecondaryString());
            interactSecondary = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetInteractSecondaryString());
            ultimateSecondary = (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetUltimateSecondaryString());
        }
        else
        {
            dash = KeyCode.Space;
            granade = KeyCode.Q;
            attack = KeyCode.Mouse0;
            interact = KeyCode.F;
            ultimate = KeyCode.E;
            dashSecondary = KeyCode.JoystickButton0;
            granadeSecondary = KeyCode.JoystickButton1;
            attackSecondary = KeyCode.JoystickButton2;
            interactSecondary = KeyCode.JoystickButton3;
            ultimateSecondary = KeyCode.JoystickButton4;
        }
    }

    public KeyCode GetDash()
    {
        return dash;
    }
    public KeyCode GetGranade()
    {
        return granade;
    }
    public KeyCode GetAttack()
    {
        return attack;
    }
    public KeyCode GetInteract()
    {
        return interact;
    }
    public KeyCode GetUltimate()
    {
        return ultimate;
    }
    public KeyCode GetSecondaryDash()
    {
        return dashSecondary;
    }
    public KeyCode GetSecondaryGranade()
    {
        return granadeSecondary;
    }
    public KeyCode GetSecondaryAttack()
    {
        return attackSecondary;
    }
    public KeyCode GetSecondaryInteract()
    {
        return interactSecondary;
    }
    public KeyCode GetSecondaryUltimate()
    {
        return ultimateSecondary;
    }

}
