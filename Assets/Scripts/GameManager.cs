using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    public CraftingManager CraftingManager { get; private set; }
    public RelicManager RelicManager { get; private set; }
    public ZooManager Zoo { get; private set; }
    public VfxManager vfxManager { get; private set; }


    public bool isTutorial;
    private bool gameStarted;
    public SoundManager soundManager { get; private set; }
    public DialogueExecuter dialogueExecuter { get; private set; }
    public bool GameStarted { get => gameStarted; }

    public UnityEvent OnRunStart;
    public UnityEvent OnRunEnd;


    [SerializeField] internal AssetsRefrence assets;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        Application.targetFrameRate = 300;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //monot
        generalFunctions = new GeneralFunctions();
        //mono
        inputManager = GetComponentInChildren<InputManager>();
        pauseMenuHandler = GetComponentInChildren<PauseMenuHandler>();
        soundManager = GetComponentInChildren<SoundManager>();
        DamageManager = GetComponentInChildren<DamageManager>();
        PopupManager = GetComponentInChildren<DamagePopupManager>();
        LevelManager = GetComponentInChildren<LevelManager>();
        CraftingManager = GetComponentInChildren<CraftingManager>();
        vfxManager = GetComponentInChildren<VfxManager>();
        dialogueExecuter = GetComponentInChildren<DialogueExecuter>();
        RelicManager = GetComponentInChildren<RelicManager>();
        Zoo = GetComponentInChildren<ZooManager>();
    }
    private void Start()
    {
        inputManager.inputs.General.Quit.canceled += QuitGame;
        inputManager.inputs.General.Reset.canceled += ResetScene;
        if (!isTutorial)
        {
            assets.playerActor.OnDeath.AddListener(generalFunctions.onPlayerDiedActions);
            assets.GlimmeringWoodsAudioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.GlimmeringWoodsAmbiance);
            assets.GlimmeringWoodsAudioSource.volume = SoundManager.GetVolumeOfClip(SoundManager.Sound.GlimmeringWoodsAmbiance);
            assets.BaseAudioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.BaseSound);
            assets.BaseAudioSource.volume = SoundManager.GetVolumeOfClip(SoundManager.Sound.BaseSound);
            assets.BaseAudioSource.Play();
            OnRunStart.AddListener(CallGlimmeringWoodsSound);
        }
        OnRunStart.AddListener(StartGameBool);
        OnRunEnd.AddListener(EndGameBool);
    }

    private void StartGameBool()
    {
        gameStarted = true;
    }
    private void EndGameBool()
    {
        gameStarted = false;
    }
    private void CallGlimmeringWoodsSound()
    {
        //turn off base 
        assets.BaseAudioSource.Stop();
        //turn on glimmering sound
        assets.GlimmeringWoodsAudioSource.Play();
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
    public Sprite WindChimes;
    public Sprite KnifeOfTheHunter;
    public Sprite TurtlePendant;
    public Sprite LegendaryCloneRelic;
    public Sprite BranchGlimmeringBundleSprite;
    public Sprite HealingFromWheelSprite;
    public Sprite TuffLegsSprite;



    public Image RelicBarImage;
    public PrizePanelHandler prizePanel;

    public RelicDrop relicDropPrefab;

    [Header("CAMERA"), Space(10)]
    public CamPositionSetter camPositionSetter;
    public Cinemachine.CinemachineVirtualCamera mainVCam;
    public CinemachineShake CamShaker;

    [Header("HEALTH BAR"), Space(10)]
    public ObjectPool CubePool;

    [Header("CANVAS"), Space(10)]
    public BlackFade blackFade;
    public GameObject mobileControls;
    public MobileControlsHandler mobileButtonHandler;
    public GameObject endInteractablePanel;
    public GameObject CraftingPanel;
    public BaseTutorialHandler baseTutorialHandler;
    public ChoiceWorldManager choiceWorldHandler;
    public TextMeshProUGUI ThanksForPlaying;
    public RelicChoicePanel RelicChoicePanel;
    [Header("Bounder"), Space(10)]
    public BounderScout BounderScout;

    [Header("CraftingUi"), Space(10)]
    public CraftingMapNode RelicCraftingMapNode;
    public CraftingMapNode WeaponCraftingMapNode;
    public Sprite GlimmeringSprite;
    public Sprite BranchSprite;
    public Sprite RuneSprite;
    public Sprite TuffCointSprite;
    public Sprite LeatherSprite;
    public ItemUiSlot ItemUiSlot;

    [Header("Crafting"), Space(10)]
    public ItemPickup ItemPickUpPrefab;

    [Header("Base"), Space(10)]
    public GameObject baseFatherObject;
    public GameObject baseSpawnPlayerPositionObject;
    public AudioSource BaseAudioSource;


    [Header("Mushroom"), Space(10)]
    public ShroomCapObjectPool CapPool;

    [Header("Tuff")]
    public TuffInteractionHandler tuffRef;
    public WheelOfFortuneManager wheelOfFortuneManager;
    public WheelOfFortunePrizes wheelOfFortunePrizes;
    [Header("GlimmeringWoods")]
    public AudioSource GlimmeringWoodsAudioSource;
    [Header("ZOO")]
    public AnimalPickup AnimalPickupPrefab;
    [Header("Boss")]
    public Animator BossAnimator;
    [Header("Object Pools")]
    public ExplosionObjectPool ExplsionOP;

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
    public void onPlayerDiedActions()//change this to not reset the scene.
    {
        LeanTween.cancelAll();
        GameManager.Instance.assets.mobileControls.SetActive(false);

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
            case StatusEffectEnum.KnifeOfTheHunter:
                return new KnifeOfTheHunter();
            case StatusEffectEnum.WindChimes:
                return new RubberDuck();
            case StatusEffectEnum.TurtlePendant:
                return new TurtlePendant();
            case StatusEffectEnum.Legendary:
                return new LegendaryStatus();
            default:
                break;
        }
        return null;
    }

    public CraftingMapNode GetCraftingMapNodeFromEnum(CraftingMapType givenMapType)
    {
        switch (givenMapType)
        {
            case CraftingMapType.Relic:
                return GameManager.Instance.assets.RelicCraftingMapNode;
            case CraftingMapType.Weapon:
                return GameManager.Instance.assets.WeaponCraftingMapNode;
            default:
                return null;
        }
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
            case ItemType.TuffCoin:
                return GameManager.Instance.assets.TuffCointSprite;

            default:
                return null;
        }
    }
    public Sprite GetSpriteFromSpecialPrizeType(SpecialPrizes prize)
    {
        switch (prize)
        {
            case SpecialPrizes.BranchAndGlimmering:
                return GameManager.Instance.assets.BranchGlimmeringBundleSprite;
            case SpecialPrizes.Healing:
                return GameManager.Instance.assets.HealingFromWheelSprite;
            case SpecialPrizes.TuffLegs:
                return GameManager.Instance.assets.TuffLegsSprite;
            default:
                return null;
        }
    }
}

