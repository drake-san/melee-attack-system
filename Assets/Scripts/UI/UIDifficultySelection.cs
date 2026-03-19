using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDifficultySelection : MonoBehaviour
{
    public void StartEasy()
    {
        GameManager.instance.StartEasy();
    }

    public void StartMedium()
    {
        GameManager.instance.StartMedium();
    }

    public void StartHard()
    {
        GameManager.instance.StartHard();
    }

    public void StartImpossible()
    {
        GameManager.instance.StartImpossible();
    }
}