using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public InputManager inputManager { get; private set; }
    public GeneralFunctions generalFunctions { get; private set; }
    public PauseMenuHandler pauseMenuHandler { get; private set; }
    public DamageManager DamageManager { get; private set; }


    public SoundManager soundManager { get; private set; }

    [SerializeField] internal AssetsRefrence assets;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        generalFunctions = new GeneralFunctions();
        inputManager = GetComponentInChildren<InputManager>();
        pauseMenuHandler = GetComponentInChildren<PauseMenuHandler>();
        soundManager = GetComponentInChildren<SoundManager>();
        DamageManager = GetComponentInChildren<DamageManager>();


    }
    private void Start()
    {
        inputManager.inputs.General.Quit.canceled += QuitGame;
        inputManager.inputs.General.Reset.canceled += ResetScene;

    }
    private void ResetScene(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitGame(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }
    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
}

[System.Serializable]
public class AssetsRefrence
{
    public GameObject Player;
    public PlayerActor playerActor;
    public PlayerController PlayerController;
}
public class GeneralFunctions
{
    public bool IsInRange(Vector3 positionA, Vector3 positionB, float range)
    {
        bool _isInRange = (positionA - positionB).sqrMagnitude < range * range;
        return _isInRange;

    }

    public float CalcRange(Vector3 posA, Vector3 posB)
    {
        return (posA - posB).magnitude;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public StatusEffect GetStatusFromType(StatusEffectEnum effect)
    {
        switch (effect)
        {
            case StatusEffectEnum.burn:
                return new BurnSE();
            case StatusEffectEnum.freeze:
                break;
            default:
                break;
        }
        return null;
    }
}


