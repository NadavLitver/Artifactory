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
    public CraftinManager CraftingManager { get; private set; }

    public VfxManager vfxManager { get; private set; }


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
        CraftingManager = GetComponentInChildren<CraftinManager>();
        vfxManager = GetComponentInChildren<VfxManager>();



    }
    private void Start()
    {
        inputManager.inputs.General.Quit.canceled += QuitGame;
        inputManager.inputs.General.Reset.canceled += ResetScene;
        assets.playerActor.OnDeath.AddListener(generalFunctions.onPlayerDiedActions);
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

    [Header("LEVEL MANAGER")]
    public GameObject EmergencyExit;

    [Header("RELICS"), Space(10)]
    public Sprite LightningEmblem;
    public Sprite HealingGoblet;

    [Header("CAMERA"), Space(10)]
    public CamPositionSetter camPositionSetter;

    [Header("HEALTH BAR"), Space(10)]
    public ObjectPool CubePool;

    [Header("CANVAS"), Space(10)]
    public BlackFade blackFade;
    public GameObject mobileControls;
    public MobileControlsHandler mobileButtonHandler;
    public GameObject endInteractablePanel;
    public GameObject CraftingPanel;

    [Header("Bounder"), Space(10)]
    public Transform BounderScout;

    [Header("CraftingUi"), Space(10)]
    public CraftingMapNode craftingMapNode;
    public Sprite GlimmeringSprite;
    public Sprite BranchSprite;
    public Sprite RuneSprite;
    public Sprite LeatherSprite;
    public ItemUiSlot ItemUiSlot;
    

    [Header("Base"), Space(10)]
    public GameObject baseFatherObject;
    public GameObject baseSpawnPlayerPositionObject;

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
    public Vector2 CalcRangeV2(Vector3 posA, Vector3 posB)
    {
        return (posA - posB);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SpawnObjectAt(GameObject obj, Vector3 pos)
    {
        obj.transform.position = pos;
    }
    public void onPlayerDiedActions()
    {
        LeanTween.cancelAll();

        LeanTween.delayedCall(1, ResetScene);
    }

    public StatusEffect GetStatusFromType(StatusEffectEnum effect)
    {
        switch (effect)
        {
            case StatusEffectEnum.burn:
                return new BurnSE();
            case StatusEffectEnum.freeze:
                break;
            case StatusEffectEnum.LightningEmblem:
                return new LightningEmblem();
            case StatusEffectEnum.HealingGoblet:
                return new HealingGoblet();
            case StatusEffectEnum.Invulnerability:
                return new Invulnerability();
            default:
                break;
        }
        return null;
    }


    public Sprite GetSpriteFromItemType(ItemType givenItem)
    {
        switch (givenItem)
        {
            case ItemType.Glimmering:
                return GameManager.Instance.assets.GlimmeringSprite;
            case ItemType.Branch:
                return GameManager.Instance.assets.BranchSprite;
            case ItemType.Rune:
                return GameManager.Instance.assets.RuneSprite;
            default:
                return null;
        }
    }
}

