using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private RectTransform building;
    [SerializeField] private float durationLoadScene = 0.5f;
    [SerializeField] private Vector2 target;

    private void Start()
    {
        building = GameObject.Find("Building").GetComponent<RectTransform>();
    }

    public void LoadLevelSelectScene()
    {
        StartCoroutine(UIMovement.MoveBuildingToLeftAndLoadScene(building, target, durationLoadScene));
    }

    public void LoadMainMenuSceneWithTransition()
    {
        StartCoroutine(UIMovement.MoveBuildingToRightAndLoadScene(building, target, durationLoadScene));
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeVertexColorText()
    {

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
