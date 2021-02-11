using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Function to Quit Game
#if UNITY_EDITOR // If inside the editor, just show the debug message "Quit Game!".
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
    }
#else // If not, close the application.
    public void QuitGame()
    {
        Application.Quit();
        //System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
#endif

}