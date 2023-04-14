using System;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private static CheatManager _instance;
    GameObject player;
    PlayerMovement playerMovement;
    FPSMovement fpsMovement;

    bool showCheatConsole = false;
    float timer = 0f;
    string input = "";

    public static bool isNoDamage = false;
    public static bool is1HitKill = false;
    public static int motherlodeAmount = 0;
    public static bool is2xSpeed = false;
    public static bool isFullHPPet = false;


    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        fpsMovement = player.GetComponent<FPSMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            timer = 0f;
            ToggleCheatConsole();
        }
    }

    void ToggleCheatConsole()
    {
        showCheatConsole = !showCheatConsole;
        if (playerMovement != null)
        {
            playerMovement.enabled = !playerMovement.enabled;
        }
        if (fpsMovement != null)
        {
            fpsMovement.enabled = !fpsMovement.enabled;
        }
    }

    void OnGUI()
    {
        if (!showCheatConsole)
        {
            return;
        }
        timer += Time.deltaTime;

        float y = 0f;

        GUI.Box(new Rect(0, 0, Screen.width, 50), "Cheat Console");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.SetNextControlName("InputField");
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input, 25);
        GUI.FocusControl("InputField");

        //if user pres enter
        if (Event.current.keyCode == KeyCode.Return)
        {
            string[] parsedInput = input.ToLower().Replace(" ", "").Split('=');
            try
            {
                switch (parsedInput[0])
                {
                    case "isnodamage":
                        isNoDamage = bool.Parse(parsedInput[1]);
                        break;
                    case "is1hitkill":
                        is1HitKill = bool.Parse(parsedInput[1]);
                        break;
                    case "motherlode":
                        if (parsedInput.Length < 2)
                        {
                            motherlodeAmount = Int16.MaxValue;
                        }
                        else
                        {
                            motherlodeAmount = int.Parse(parsedInput[1]);
                        }
                        GoldManager.addGold(motherlodeAmount);
                        break;
                    case "is2xspeed":
                        is2xSpeed = bool.Parse(parsedInput[1]);
                        break;
                    case "isfullhppet":
                        isFullHPPet = bool.Parse(parsedInput[1]);
                        break;
                    case "killpet":
                        try
                        {
                            GameObject pet = PetManager.currentPet;
                            PetHealth petHealth = pet.GetComponent<PetHealth>();
                            petHealth.killPet();
                        }
                        catch (System.Exception)
                        {
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception)
            {
            }
            //Clear input
            input = "";
        }

        if (Event.current.keyCode == KeyCode.BackQuote && timer > 0.5f)
        {
            ToggleCheatConsole();
        }
    }
}
