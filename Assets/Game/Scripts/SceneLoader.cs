using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCourseCreationScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLabScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadCourseJoinScene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadCourseStudyScene()
    {
        SceneManager.LoadScene(5);
    }
}
