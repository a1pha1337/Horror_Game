using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class Cheats : MonoBehaviour {       //Командная строка игры

    public GameObject inputCheat;
    public InputField inputfieldname;
    public GameObject countOfSheets;
	
	// Update is called once per frame
	void Update () {
        CheatInputField();
	}

    private void CheatInputField()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.C))
        {
            StreamWriter log = new StreamWriter("log.dat", true);

            if (inputCheat.active == false)
            {
                inputCheat.SetActive(true);
                Cursor.visible = true;
                Time.timeScale = 0;

                log.WriteLine("[GAME_CONSOLE]("+ DateTime.Now + "): Game console has been opened!");
            }
            else
            {
                inputCheat.SetActive(false);
                Cursor.visible = false;
                Time.timeScale = 1;

                log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Game console has been closed!");
            }

            log.Close();
        }
    }

    public void EndEdit()
    {
        StreamWriter log = new StreamWriter("log.dat", true);

        inputfieldname.Select();
        
        string command = inputfieldname.text;

        string[] definition = command.Split((char)' ');

        switch (definition[0])
        {
            case "settime":         //Команда
                float newTime;

                if (!float.TryParse(definition[1], out newTime))
                {
                    Debug.Log("Symbol must be float format!");
                    log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Symbol must be float format!" + newTime);

                    break;
                }

                StaticValues.time = newTime;
                Debug.Log("Set time to " + newTime);
                log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Set time to " + newTime);

                break;

            case "setdifficulty":
                int newDifficulty;

                if (!int.TryParse(definition[1], out newDifficulty))
                {
                    Debug.Log("Symbol must be integer format!");
                    log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Symbol must be integer format!");

                    break;
                }

                if ((newDifficulty < 0) || (newDifficulty > 3))
                {
                    Debug.Log("Nonexistent difficulty!");
                    log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Nonexistent difficulty!");

                    break;
                }

                StaticValues.difficulty = newDifficulty;
                Debug.Log("Difficulty set to " + newDifficulty);
                log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Difficulty set to " + newDifficulty);

                switch (StaticValues.difficulty)
                {
                    case 0:
                        countOfSheets.GetComponent<Text>().text = StaticValues.countOfSheets + "/2";
                        break;

                    case 1:
                        countOfSheets.GetComponent<Text>().text = StaticValues.countOfSheets + "/4";
                        break;

                    case 2:
                        countOfSheets.GetComponent<Text>().text = StaticValues.countOfSheets + "/6";
                        break;

                    case 3:
                        countOfSheets.GetComponent<Text>().text = StaticValues.countOfSheets + "/8";
                        break;
                }

                break;

            case "restartgame":
                SceneManager.LoadScene("Game");
                StaticValues.countOfSheets = 0;
                StaticValues.time = 600;
                Time.timeScale = 1;
                Cursor.visible = false;

                Debug.Log("Game restarted!");
                log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Game restarted");

                break;

            case "setsheets":
                int newCountOfSheets;

                if (!int.TryParse(definition[1], out newCountOfSheets))
                {
                    Debug.Log("Symbol must be integer format!");
                    log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Symbol must be integer format!");

                    break;
                }

                StaticValues.countOfSheets = newCountOfSheets;

                switch (StaticValues.difficulty)
                {
                    case 0:
                        countOfSheets.GetComponent<Text>().text = StaticValues.countOfSheets + "/2";
                        break;

                    case 1:
                        countOfSheets.GetComponent<Text>().text = StaticValues.countOfSheets + "/4";
                        break;

                    case 2:
                        countOfSheets.GetComponent<Text>().text = StaticValues.countOfSheets + "/6";
                        break;

                    case 3:
                        countOfSheets.GetComponent<Text>().text = StaticValues.countOfSheets + "/8";
                        break;
                }

                Debug.Log("Sheets are setted!");
                log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + "): Sheets are setted!");

                break;

            default:
                Debug.Log("Unknown command!");
                log.WriteLine("[GAME_CONSOLE](" + DateTime.Now + ": Unknown command!");

                break;
        }

        inputfieldname.text = "";
        log.Close();
    }
}
