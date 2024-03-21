using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlSettings : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    [SerializeField] private Text dash, granade, attack, interact, ultimate,
        dashSecondary, granadeSecondary, attackSecondary, interactSecondary, ultimateSecondary;
    [SerializeField] private GameObject timer;

    private GameObject currentKey;
    
    private string dashString;
    private string granadeString;
    private string attackString;
    private string interactString;
    private string ultimateString;
    private string dashSecondaryString;
    private string granadeSecondaryString;
    private string attackSecondaryString;
    private string interactSecondaryString;
    private string ultimateSecondaryString;
    

    private bool buttonActive = false;

    // Start is called before the first frame update
    void Start()
    {
         loadControls();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentKey != null)
        {
            if (timer.GetComponent<Timer>().isReady() == true)
            {
                if (Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton0;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton0.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton1;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton1.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton2))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton2;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton2.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton3))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton3;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton3.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton4))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton4;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton4.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton5))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton5;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton5.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton6))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton6;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton6.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton7))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton7;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton7.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton8))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton8;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton8.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton9))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton9;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton9.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
                else if (Input.GetKeyDown(KeyCode.JoystickButton10))
                {
                    keys[currentKey.name] = KeyCode.JoystickButton10;
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.JoystickButton10.ToString();
                    currentKey = null;
                    buttonActive = false;
                }
            }
        }
    }

    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
                buttonActive = false;
            }
            else if (e.isMouse)
            {
                switch (e.button)
                {
                    case 0:
                        keys[currentKey.name] = KeyCode.Mouse0;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse0";
                        break;

                    case 1:
                        keys[currentKey.name] = KeyCode.Mouse1;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse1";
                        break;
                    case 2:
                        keys[currentKey.name] = KeyCode.Mouse2;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse2";
                        break;
                    case 3:
                        keys[currentKey.name] = KeyCode.Mouse3;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse3";
                        break;
                }
                currentKey = null;
                buttonActive = false;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (buttonActive == false)
        {
            currentKey = clicked;
            buttonActive = true;
            timer.GetComponent<Timer>().TimerStart();
        }
    }

    public void setKeys()
    {
        dashString = keys["dash"].ToString();
        granadeString = keys["granade"].ToString();
        attackString = keys["attack"].ToString();
        interactString = keys["interact"].ToString();
        ultimateString = keys["ultimate"].ToString();
        dashSecondaryString = keys["dashSecondary"].ToString();
        granadeSecondaryString = keys["granadeSecondary"].ToString();
        attackSecondaryString = keys["attackSecondary"].ToString();
        interactSecondaryString = keys["interactSecondary"].ToString();
        ultimateSecondaryString = keys["ultimateSecondary"].ToString();
    } 
    public void setTexts()
    {
        dash.text = keys["dash"].ToString();
        granade.text = keys["granade"].ToString();
        attack.text = keys["attack"].ToString();
        interact.text = keys["interact"].ToString();
        ultimate.text = keys["ultimate"].ToString();
        dashSecondary.text = keys["dashSecondary"].ToString();
        granadeSecondary.text = keys["granadeSecondary"].ToString();
        attackSecondary.text = keys["attackSecondary"].ToString();
        interactSecondary.text = keys["interactSecondary"].ToString();
        ultimateSecondary.text = keys["ultimateSecondary"].ToString();
    }
    public void saveControls()
    {
        setKeys();
        SaveLoadControls.saveControls(this);
    }
    public void loadControls()
    {
        ControlData data = SaveLoadControls.loadControls();
        if (data != null)
        {
            keys.Add("dash", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetDashString()));
            keys.Add("granade", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetGranadeString()));
            keys.Add("attack", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetAttackString()));
            keys.Add("interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetInteractString()));
            keys.Add("ultimate", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetUltimateString()));
            keys.Add("dashSecondary", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetDashSecondaryString()));
            keys.Add("granadeSecondary", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetGranadeSecondaryString()));
            keys.Add("attackSecondary", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetAttackSecondaryString()));
            keys.Add("interactSecondary", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetInteractSecondaryString()));
            keys.Add("ultimateSecondary", (KeyCode)System.Enum.Parse(typeof(KeyCode), data.GetUltimateSecondaryString()));
            setTexts();
        }
        else
        {
            keys.Add("dash", KeyCode.Space);
            keys.Add("granade", KeyCode.Q);
            keys.Add("attack", KeyCode.Mouse0);
            keys.Add("interact", KeyCode.F);
            keys.Add("ultimate", KeyCode.E);
            keys.Add("dashSecondary", KeyCode.JoystickButton0);
            keys.Add("granadeSecondary", KeyCode.JoystickButton1);
            keys.Add("attackSecondary", KeyCode.JoystickButton2);
            keys.Add("interactSecondary", KeyCode.JoystickButton3);
            keys.Add("ultimateSecondary", KeyCode.JoystickButton4);

            setTexts();
        }

    }


    public string GetDashString()
    {
        return dashString;
    }
    public string GetGranadeString()
    {
        return granadeString;
    }
    public string GetAttackString()
    {
        return attackString;
    }
    public string GetInteractString()
    {
        return interactString;
    }
    public string GetUltimateString()
    {
        return ultimateString;
    }
    public string GetDashSecondaryString()
    {
        return dashSecondaryString;
    }
    public string GetGranadeSecondaryString()
    {
        return granadeSecondaryString;
    }
    public string GetAttackSecondaryString()
    {
        return attackSecondaryString;
    }
    public string GetInteractionSecondaryString()
    {
        return interactSecondaryString;
    }
    public string GetUltimateSecondaryString()
    {
        return ultimateSecondaryString;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
