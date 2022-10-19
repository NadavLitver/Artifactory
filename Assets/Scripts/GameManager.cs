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
    public DamagePopupManager PopupManager { get; private set; }
    public LevelManager LevelManager { get; private set; }


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

        //non mono
        generalFunctions = new GeneralFunctions();
        //mono
        inputManager = GetComponentInChildren<InputManager>();
        pauseMenuHandler = GetComponentInChildren<PauseMenuHandler>();
        soundManager = GetComponentInChildren<SoundManager>();
        DamageManager = GetComponentInChildren<DamageManager>();
        PopupManager = GetComponentInChildren<DamagePopupManager>();
        LevelManager = GetComponentInChildren<LevelManager>();


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
    [Header("PLAYER"), Space(10)]
    public GameObject Player;
    public PlayerActor playerActor;
    public PlayerController PlayerController;

    [Header("RELICS"), Space(10)]
    public Sprite RubberDuck;
    public Sprite LightningEmblem;
    public Sprite HealingGoblet;

    [Header("CAMERA"), Space(10)]
    public ScreenShakeHandler CameraShake;

    [Header("HEALTH BAR")]
    public ObjectPool CubePool;
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

    public void SpawnObjectAt(GameObject obj, Vector3 pos)
    {
        obj.transform.position = pos;
    }

    public StatusEffect GetStatusFromType(StatusEffectEnum effect)
    {
        switch (effect)
        {
            case StatusEffectEnum.burn:
                return new BurnSE();
            case StatusEffectEnum.freeze:
                break;
            case StatusEffectEnum.RubberDuck:
                return new RubberDuck(); 
            case StatusEffectEnum.LightningEmblem:
                return new LightningEmblem();
            case StatusEffectEnum.HealingGoblet:
                return new HealingGoblet();
            default:
                break;
        }


        return null;
    }
}

