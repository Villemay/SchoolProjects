using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssingKeybindsToUI : MonoBehaviour
{
    private GameObject iM;
    [SerializeField] private GameObject attack;
    [SerializeField] private GameObject attackSecondary;
    [SerializeField] private GameObject dash;
    [SerializeField] private GameObject dashSecondary;
    [SerializeField] private GameObject granade;
    [SerializeField] private GameObject granadeSecondary;
    [SerializeField] private GameObject ultimate;
    [SerializeField] private GameObject ultimateSecondary;
    [SerializeField] private GameObject interact;
    [SerializeField] private GameObject interactSecondary;



    // Start is called before the first frame update
    void Start()
    {
        iM = GameObject.Find("InputManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Setkeys()
    {
        attack.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetAttack().ToString();
        attackSecondary.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetSecondaryAttack().ToString();
        dash.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetDash().ToString();
        dashSecondary.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetSecondaryDash().ToString();
        granade.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetGranade().ToString();
        granadeSecondary.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetSecondaryGranade().ToString();
        ultimate.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetUltimate().ToString();
        ultimateSecondary.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetSecondaryUltimate().ToString();
        interact.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetInteract().ToString();
        interactSecondary.GetComponent<Text>().text = iM.GetComponent<InputManager>().GetSecondaryInteract().ToString();
    }
}
