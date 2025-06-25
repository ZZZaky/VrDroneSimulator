using SceneController;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader.LoadGameMapScene(1);
    }

}
