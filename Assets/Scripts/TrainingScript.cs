using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingScript : MonoBehaviour {

    private string sceneName = "GameScene";

    public void SetTrainingMode()
    {
        GameController.instance.SetCurrentState(GameState.Training);
        UIManager.instance.MenuController(GameState.Training);
        SceneManager.LoadScene(sceneName);
    }
}
