using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [Header("=== Manager to initialize ===")]
    private PlayerSetting _player;
    private PlayerManager _playerManager;
    private DeckManager _deckManager;
    private PokerLogicManager _pokerLogicManager;
    private TurnManager _turnManager;
    private BettingManager _bettingManager;
    private RoundManager _roundManager;

    // RUNTIME UI REFERENCES
    private Canvas _coachCanvas;
    private TextMeshProUGUI _coachText;
    private Button _nextButton;

    private int _currentPhase = 0;
    private List<string> _highlightObjects = new List<string>();

    private void Awake()
    {
        FindCoachUI(); // ← RUNTIME FIND
    }

    public async Task Initialize()
    {
        Debug.Log("[TutorialManager] Starting initialization...");

        await Task.Yield();
        FindAllManagers();
        await InitializeManagersInOrder();

        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log("[TutorialManager] INITIALIZATION COMPLETE");
    }

    private void FindAllManagers()
    {
        _player = FindObjectOfType<PlayerSetting>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _deckManager = FindObjectOfType<DeckManager>();
        _pokerLogicManager = FindObjectOfType<PokerLogicManager>();
        _turnManager = FindObjectOfType<TurnManager>();
        _bettingManager = FindObjectOfType<BettingManager>();
        _roundManager = FindObjectOfType<RoundManager>();
    }

    private async Task InitializeManagersInOrder()
    {
        if (_player != null) await _player.Initialize();
        if (_playerManager != null) await _playerManager.Initialize();
        if (_deckManager != null) await _deckManager.Initialize();
        if (_pokerLogicManager != null) await _pokerLogicManager.Initialize();
        if (_turnManager != null) await _turnManager.Initialize();
        if (_bettingManager != null) await _bettingManager.Initialize();
        if (_roundManager != null) await _roundManager.Initialize();
    }

    private void FindCoachUI()
    {
        // Find Coach Canvas by name (pooled safe)
        GameObject coachCanvasObj = GameObject.Find("CoachCanvas");
        if (coachCanvasObj != null)
        {
            _coachCanvas = coachCanvasObj.GetComponent<Canvas>();
            _coachText = coachCanvasObj.GetComponentInChildren<TextMeshProUGUI>();
            _nextButton = coachCanvasObj.GetComponentInChildren<Button>();

            if (_coachText == null) Debug.LogError("[TutorialManager] CoachText (TMP) not found in CoachCanvas!");
            if (_nextButton == null) Debug.LogError("[TutorialManager] NextButton not found in CoachCanvas!");
        }
        else
        {
            Debug.LogError("[TutorialManager] CoachCanvas not found in scene!");
        }
    }

    public void TutorialStart()
    {
        Debug.Log("[TutorialManager] TUTORIAL STARTED — Phase 1 begins");
        _currentPhase = 1;
        ShowPhase1();
        SetupNextButton();
    }

    private void ShowPhase1()
    {
        if (_coachText == null) return;

        _coachText.text = "Welcome to 27 Poker!\n\n" +
                         "A classic game with a powerful twist.\n" +
                         "Let's learn the ropes. I'll be your guide.";

        _coachCanvas.gameObject.SetActive(true);
        HighlightUI("HoleCards", "CommunityCards", "Pot", "ChipStack", "ActionButtons");
    }

    private void SetupNextButton()
    {
        if (_nextButton == null) return;

        _nextButton.onClick.RemoveAllListeners();
        _nextButton.onClick.AddListener(NextPhase);
    }

    private void NextPhase()
    {
        _currentPhase++;
        switch (_currentPhase)
        {
            case 2:
                ShowPhase2();
                break;
            case 3:
                ShowPhase3();
                break;
            // Add more phases...
            default:
                EndTutorial();
                break;
        }
    }

    private void ShowPhase2()
    {
        _coachText.text = "You've been dealt 2♦ and 7♦!\n\n" +
                         "Normally the weakest hand... but here it has a secret power.\n\n" +
                         "AI is checking. Tap Check.";
        HighlightUI("CheckButton");
    }

    private void ShowPhase3()
    {
        _coachText.text = "Rainbow Flop!\n\n" +
                         "All different suits = 'Rainbow'.\n" +
                         "Your 2-7 now glows! Tap to transform.";
        HighlightUI("PlayerHand");
    }

    private void HighlightUI(params string[] elementNames)
    {
        // Clear previous highlights
        foreach (string name in _highlightObjects)
        {
            var obj = GameObject.Find(name);
            if (obj != null)
            {
                var image = obj.GetComponent<Image>();
                if (image != null) image.color = Color.white;
            }
        }
        _highlightObjects.Clear();

        // New highlights
        foreach (string name in elementNames)
        {
            GameObject obj = GameObject.Find(name);
            if (obj != null)
            {
                _highlightObjects.Add(name);
                var image = obj.GetComponent<Image>();
                if (image != null)
                {
                    image.color = new Color(1f, 1f, 0f, 0.7f); // Yellow glow
                }
            }
        }
    }

    private void EndTutorial()
    {
        _coachCanvas.gameObject.SetActive(false);
        _nextButton.onClick.RemoveAllListeners();
        ClearHighlights();
        Debug.Log("[TutorialManager] TUTORIAL COMPLETE");
    }

    private void ClearHighlights()
    {
        HighlightUI(); // Empty array = clear all
    }

    private void OnDestroy()
    {
        if (_nextButton != null)
            _nextButton.onClick.RemoveAllListeners();
    }
}