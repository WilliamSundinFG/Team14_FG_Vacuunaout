using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using Team14;
using System;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optMenu;
    [SerializeField] private Button settingsOpenButton;

    [Header("Bindings & Input")]
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private Button vacSwitchButton;
    [SerializeField] private InputActionReference vacSwitchAction;
    [SerializeField] private Button primaryActionButton;
    [SerializeField] private InputActionReference primaryAction;
    [SerializeField] private Button secondaryActionButton;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private Button jumpActionButton;
    [SerializeField] private InputActionReference secondaryAction;
    [SerializeField] private Button crouchActionButton;
    [SerializeField] private InputActionReference crouchAction;
    [SerializeField] private Button pauseActionButton;
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private Button interactActionButton;
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private Button sprintActionButton;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private Button tabletActionButton;
    [SerializeField] private InputActionReference tabletAction;
    [SerializeField] private Button mapActionButton;
    [SerializeField] private InputActionReference mapAction;
    [SerializeField] private Slider MouseSensitivitySlider;

    [Header("Graphics")]
    [SerializeField] private Dropdown resDropdown;
    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Sound")]
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider MusicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private Slider BackgroundVolumeSlider;

    [SerializeField] private GameObject camera;

    List<Resolution> resolutions;

    // Start is called before the first frame update
    void Start()
    {
        resDropdown.onValueChanged.AddListener(UpdateResolution);
        qualityDropdown.onValueChanged.AddListener(ChangeQuality);
        fullscreenToggle.onValueChanged.AddListener(ChangeFullscreenState);
        //optBackButton.onClick.AddListener(SwitchUI);
        //settingsOpenButton.onClick.AddListener(SwitchUI);

        vacSwitchButton.onClick.AddListener(() => PerformRebind(vacSwitchAction.action, vacSwitchButton.GetComponentInChildren<Text>()));
        primaryActionButton.onClick.AddListener(() => PerformRebind(primaryAction.action, primaryActionButton.GetComponentInChildren<Text>()));
        secondaryActionButton.onClick.AddListener(() => PerformRebind(secondaryAction.action, secondaryActionButton.GetComponentInChildren<Text>()));
        jumpActionButton.onClick.AddListener(() => PerformRebind(jumpAction.action, jumpActionButton.GetComponentInChildren<Text>()));
        crouchActionButton.onClick.AddListener(() => PerformRebind(crouchAction.action, crouchActionButton.GetComponentInChildren<Text>()));
        pauseActionButton.onClick.AddListener(() => PerformRebind(pauseAction.action, pauseActionButton.GetComponentInChildren<Text>()));
        interactActionButton.onClick.AddListener(() => PerformRebind(interactAction.action, interactActionButton.GetComponentInChildren<Text>()));
        sprintActionButton.onClick.AddListener(() => PerformRebind(sprintAction.action, sprintActionButton.GetComponentInChildren<Text>()));
        tabletActionButton.onClick.AddListener(() => PerformRebind(tabletAction.action, tabletActionButton.GetComponentInChildren<Text>()));
        mapActionButton.onClick.AddListener(() => PerformRebind(mapAction.action, mapActionButton.GetComponentInChildren<Text>()));

        MasterVolumeSlider.onValueChanged.AddListener(ChangeMasterVolume);
        MusicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        BackgroundVolumeSlider.onValueChanged.AddListener(ChangeBackgroundVolume);
        SFXVolumeSlider.onValueChanged.AddListener(ChangeSFXVolume);

        MouseSensitivitySlider.onValueChanged.AddListener(ChangeMouseSensitivity);

        InitUIValues();
    }

    private void OnEnable()
    {
        LoadBindingOverrides();
    }

    private void Awake()
    {
        Settings.InitSettings();
    }

    private void LoadBindingOverrides()
    {
        inputActions.LoadBindingOverridesFromJson(Settings.GetInputFromFile());
    }

    private void OnDisable()
    {
        Settings.SaveInputToFile(inputActions.SaveBindingOverridesAsJson());
    }

    private void InitUIValues()
    {
        MasterVolumeSlider.value = Settings.GetSettings().MasterVolume;
        MusicVolumeSlider.value = Settings.GetSettings().MusicVolume;
        BackgroundVolumeSlider.value = Settings.GetSettings().BackgroundVolume;
        SFXVolumeSlider.value = Settings.GetSettings().SFXVolume;
        InitBindingButtonStrings();
        InitGraphicsSettings();
    }

    private void PerformRebind(InputAction actionToRebind, Text buttonText)
    {
        buttonText.text = "Press a Key to rebind";
        var rebindOp = actionToRebind.PerformInteractiveRebinding(1).WithControlsExcluding("Gamepad")
            .OnMatchWaitForAnother(.1f)
            .WithCancelingThrough("<Keyboard>/Escape")
            .Start();

        rebindOp.OnCancel((e) =>
        {
            InitBindingButtonStrings();
            e.Dispose();
        });
        rebindOp.OnComplete((e) => 
        {
            InitBindingButtonStrings();
            e.Dispose();
        });
    }

    public void ResetInput()
    {
        Settings.ResetInputOverrides();
        LoadBindingOverrides();
        InitBindingButtonStrings();
    }

    private void InitBindingButtonStrings()
    {
        string groupString;
        groupString = "Keyboard&Mouse";

        Text vacSwitchText = vacSwitchButton.GetComponentInChildren<Text>();
        Text primaryActionText = primaryActionButton.GetComponentInChildren<Text>();
        Text secondaryActionText = secondaryActionButton.GetComponentInChildren<Text>();
        Text jumpActionText = jumpActionButton.GetComponentInChildren<Text>();
        Text crouchActionText = crouchActionButton.GetComponentInChildren<Text>();
        Text pauseActionText = pauseActionButton.GetComponentInChildren<Text>();
        Text interactActionText = interactActionButton.GetComponentInChildren<Text>();
        Text sprintActionText = sprintActionButton.GetComponentInChildren<Text>();
        Text tabletActionText = tabletActionButton.GetComponentInChildren<Text>();
        Text mapActionText = mapActionButton.GetComponentInChildren<Text>();

        vacSwitchText.text = vacSwitchAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        primaryActionText.text = primaryAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        secondaryActionText.text = secondaryAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        jumpActionText.text = jumpAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        crouchActionText.text = crouchAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        pauseActionText.text = pauseAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        interactActionText.text = interactAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        sprintActionText.text = sprintAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        tabletActionText.text = tabletAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);
        mapActionText.text = mapAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions, group: groupString);

        MouseSensitivitySlider.value = Settings.GetSettings().Sensitivity;
    }

    private void ChangeMouseSensitivity(float val)
    {
        Settings.SetMouseSensitivity(val);
    }

    private void ChangeSFXVolume(float val)
    {
        AudioManager.Instance.ChangeVolume(val, "SFX");
    }

    private void ChangeBackgroundVolume(float val)
    {
        AudioManager.Instance.ChangeVolume(val, "Background");
    }

    private void ChangeMusicVolume(float val)
    {
        AudioManager.Instance.ChangeVolume(val, "Music");
    }

    private void ChangeMasterVolume(float val)
    {
        AudioManager.Instance.ChangeVolume(val, "Master");
    }

    private void ChangeFullscreenState(bool val)
    {
        Settings.FullScreen = val;
    }

    private void ChangeQuality(int index)
    {
        Settings.SetQualityLevel(index);
    }

    private void UpdateResolution(int index)
    {
        Settings.SetResolution(resolutions[index].width, resolutions[index].height);
    }

    void InitGraphicsSettings()
    {
        //Settings.InitSettings();
        resolutions = Screen.resolutions.ToList();
        List<Dropdown.OptionData> resOptions = new List<Dropdown.OptionData>();
        resolutions.ForEach(resolution => resOptions.Add(new Dropdown.OptionData(resolution.ToString())));
        resDropdown.AddOptions(resOptions);
        resDropdown.value = resOptions.FindIndex(r => r.text.Contains($"{Settings.ResX} x {Settings.ResY}"));
        qualityDropdown.value = Settings.GetSettings().GraphicsQuality;
        fullscreenToggle.isOn = Settings.FullScreen;
    }


    public void MenuOrient(string type)
    {

        if (type == "SettingsSub")
        {
            camera.GetComponent<Animator>().SetTrigger("RotateDown");
            camera.GetComponent<Animator>().ResetTrigger("RotateUp");
            gameObject.transform.Find("Main Text Options").gameObject.SetActive(false);
            gameObject.transform.Find("Graphic").gameObject.SetActive(false);
            gameObject.transform.Find("SettingsSub").gameObject.SetActive(true);
            EnableBack("SettingsSub");
        }

        if (type == "Credits")
        {
            camera.GetComponent<Animator>().SetTrigger("ZoomIn");
            camera.GetComponent<Animator>().ResetTrigger("ZoomOut");
            gameObject.transform.Find("Main Text Options").gameObject.SetActive(false);
            gameObject.transform.Find("Graphic").gameObject.SetActive(false);
            gameObject.transform.Find("CreditsText").gameObject.SetActive(true);
            EnableBack("Credits");
        }

        if (type == "SettingsAudio")
        {
            gameObject.transform.Find("SettingsSub").gameObject.SetActive(false);
            gameObject.transform.Find("Settings_Audio").gameObject.SetActive(true);
            EnableBack("SettingsAudio");
            DisableBack("BackSub");
        }

        if (type == "SettingsGraphics")
        {
            gameObject.transform.Find("SettingsSub").gameObject.SetActive(false);
            gameObject.transform.Find("Settings_Graphics").gameObject.SetActive(true);
            EnableBack("SettingsGraphics");
            DisableBack("BackSub");
        }

        if (type == "SettingsControls")
        {
            gameObject.transform.Find("SettingsSub").gameObject.SetActive(false);
            gameObject.transform.Find("Settings_Controls").gameObject.SetActive(true);
            EnableBack("SettingsControls");
            DisableBack("BackSub");
        }
    }
    private void EnableBack(string type)
    {
        if (type == "SettingsSub")
        {
            gameObject.transform.Find("BackSettingsSub").gameObject.SetActive(true);
        }
        if (type == "Credits")
        {
            gameObject.transform.Find("BackCredits").gameObject.SetActive(true);
        }

        if (type == "SettingsAudio")
        {
            gameObject.transform.Find("BackSettingsAudio").gameObject.SetActive(true);
        }
        if (type == "SettingsGraphics")
        {
            gameObject.transform.Find("BackSettingsGraphics").gameObject.SetActive(true);
        }
        if (type == "SettingsControls")
        {
            gameObject.transform.Find("BackSettingsControls").gameObject.SetActive(true);
        }
    }
    private void DisableBack(string type)
    {
        if (type == "BackSub")
            {
                gameObject.transform.Find("BackSettingsSub").gameObject.SetActive(false);
            }
        if (type == "Credits")
            {
                gameObject.transform.Find("BackCredits").gameObject.SetActive(false);
            }
        if (type == "BackAudio")
        {
            gameObject.transform.Find("BackSettingsAudio").gameObject.SetActive(false);
        }
        if (type == "BackGraphics")
        {
            gameObject.transform.Find("BackSettingsGraphics").gameObject.SetActive(false);
        }
        if (type == "BackControls")
        {
            gameObject.transform.Find("BackSettingsControls").gameObject.SetActive(false);
        }
    }

    public void Back(string From)
    {
        if (From == "BackSub")
        {
            DisableBack("BackSub");
            camera.GetComponent<Animator>().ResetTrigger("RotateDown");
            camera.GetComponent<Animator>().SetTrigger("RotateUp");
            gameObject.transform.Find("Main Text Options").gameObject.SetActive(true);
            gameObject.transform.Find("Graphic").gameObject.SetActive(true);
            gameObject.transform.Find("SettingsSub").gameObject.SetActive(false);
        }
        if (From == "Credits")
        {
            DisableBack("Credits");
            camera.GetComponent<Animator>().ResetTrigger("ZoomIn");
            camera.GetComponent<Animator>().SetTrigger("ZoomOut");
            gameObject.transform.Find("Main Text Options").gameObject.SetActive(true);
            gameObject.transform.Find("Graphic").gameObject.SetActive(true);
            gameObject.transform.Find("CreditsText").gameObject.SetActive(false);
        }

        if (From == "BackAudio")
        {
            DisableBack("BackAudio");
            EnableBack("SettingsSub");
            gameObject.transform.Find("SettingsSub").gameObject.SetActive(true);
            gameObject.transform.Find("Settings_Audio").gameObject.SetActive(false);
        }
        if (From == "BackGraphics")
        {
            DisableBack("BackGraphics");
            EnableBack("SettingsSub");
            gameObject.transform.Find("SettingsSub").gameObject.SetActive(true);
            gameObject.transform.Find("Settings_Graphics").gameObject.SetActive(false);
        }
        if (From == "BackControls")
        {
            DisableBack("BackControls");
            EnableBack("SettingsSub");
            gameObject.transform.Find("SettingsSub").gameObject.SetActive(true);
            gameObject.transform.Find("Settings_Controls").gameObject.SetActive(false);
        }
    }
}