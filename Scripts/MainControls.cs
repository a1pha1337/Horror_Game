using UnityEngine;
using UnityEngine.SceneManagement;

public class MainControls : MonoBehaviour {
    public void PlayPressed()
    {
        SceneManager.LoadScene("Game");
        Cursor.visible = false;

        if (StaticValues.sens_x == -1 || StaticValues.sens_y == -1)
        {
            StaticValues.sens_x = 5.5f;
            StaticValues.sens_y = 5.5f;
        }
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
}
