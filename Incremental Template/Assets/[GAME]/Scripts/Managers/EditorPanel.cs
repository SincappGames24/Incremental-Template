using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditorPanel : MonoBehaviour
{
    private int SelectedScene = 1;
    [SerializeField] private GameObject _panel;
    public Dropdown LevelSelectDropdown;
    public Dropdown BrushSelectDropdown;
    public Text _moneyTextField;
    public Button _incomeIncreaseButton, _incomeDecreaseButton;
    private Camera _camera;
    public Dropdown SkyboxColorInput, PlatformColorInput;
    public Text PlayerSpeedText;
    public Text PlayerPowerText;
    public Text PlayerFireRateText;
    public Text PlayerRangeText;
    public Slider PlayerSpeedSlider;
    private PlayerMovementController _playerMovementController;
    public Toggle IncrementalCloseToggle;
    public Toggle InGameUICloseToggle;
    private InGameUIManager _inGameUIManager;
    private CameraController _cameraController;
    public Slider CameraPositionXSlider;
    public Slider CameraPositionYSlider;
    public Slider CameraPositionZSlider;
    public Slider CameraFovSlider;
    public Slider CameraRotXSlider;
    public Slider CameraRotYSlider;
    public Slider FireRateSlider;
    public Slider RangeSlider;
    public Slider PowerSlider;
    private CUIColorPicker _cuiColorPicker;
    [SerializeField] private GameObject ColorWheel;
    [SerializeField] private Material _platformMat;
    [SerializeField] private Button _skyboxColorWheel, _platformColorWheel;
    private int skyOrPlatformWheelIndex;
    private Color _platformDefaultColors = new Color(0.79f, 0.79f, 0.79f);


    private string[] _skyColors = new[]
    {
        "A7D3F7", //default
        "FF00FF", //magenta
        "0000FF", //blue
        "00B140", //green
        "000000", //black
        "ffffff", //white
    };

    private void Awake()
    {
        Time.timeScale = 1;
        _playerMovementController = FindObjectOfType<PlayerMovementController>();
        _cuiColorPicker = FindObjectOfType<CUIColorPicker>();
        _inGameUIManager = FindObjectOfType<InGameUIManager>();
        _cameraController = FindObjectOfType<CameraController>();
        _camera = Camera.main;
        LoadPersistData();
    }

    private void OnEnable()
    {
        IncrementalCloseToggle.onValueChanged.AddListener(IncrementalCloseToggles);
        LevelSelectDropdown.onValueChanged.AddListener(ChangeActiveLevel);
        SkyboxColorInput.onValueChanged.AddListener(ChangeSkyboxColor);
        PlatformColorInput.onValueChanged.AddListener(ChangePlatformColor);
        _incomeIncreaseButton.onClick.AddListener(IncreaseMoney);
        _incomeDecreaseButton.onClick.AddListener(DecreaseMoney);
        PlayerSpeedSlider.onValueChanged.AddListener(ChangeMovementSpeed);
        InGameUICloseToggle.onValueChanged.AddListener(InGameUICloseToggles);
        CameraPositionXSlider.onValueChanged.AddListener(CameraPosX);
        CameraPositionYSlider.onValueChanged.AddListener(CameraPosY);
        CameraPositionZSlider.onValueChanged.AddListener(CameraPosZ);
        FireRateSlider.onValueChanged.AddListener(ChangeFireRate);
        RangeSlider.onValueChanged.AddListener(ChangeRange);
        PowerSlider.onValueChanged.AddListener(ChangePower);
        _skyboxColorWheel.onClick.AddListener(OpenSkyWheel);
        _platformColorWheel.onClick.AddListener(OpenPlatformWheel);
        CameraFovSlider.onValueChanged.AddListener(CamFov);
        CameraRotXSlider.onValueChanged.AddListener(CameraRotX);
        CameraRotYSlider.onValueChanged.AddListener(CameraRotY);
        _cuiColorPicker._onValueChange += ChangeWheelColor;
    }

    private void OnDisable()
    {
        PersistData.Instance.Save();
        CameraPositionXSlider.onValueChanged.RemoveAllListeners();
        CameraPositionYSlider.onValueChanged.RemoveAllListeners();
        CameraPositionZSlider.onValueChanged.RemoveAllListeners();
        FireRateSlider.onValueChanged.RemoveAllListeners();
        RangeSlider.onValueChanged.RemoveAllListeners();
        PowerSlider.onValueChanged.RemoveAllListeners();
        CameraRotYSlider.onValueChanged.RemoveAllListeners();
        CameraRotXSlider.onValueChanged.RemoveAllListeners();
        CameraFovSlider.onValueChanged.AddListener(CamFov);
        IncrementalCloseToggle.onValueChanged.RemoveListener(IncrementalCloseToggles);
        LevelSelectDropdown.onValueChanged.RemoveListener(ChangeActiveLevel);
        SkyboxColorInput.onValueChanged.RemoveListener(ChangeSkyboxColor);
        PlatformColorInput.onValueChanged.RemoveAllListeners();
        _incomeIncreaseButton.onClick.RemoveAllListeners();
        _incomeDecreaseButton.onClick.RemoveAllListeners();
        _platformColorWheel.onClick.RemoveAllListeners();
        _skyboxColorWheel.onClick.RemoveAllListeners();
        PlayerSpeedSlider.onValueChanged.RemoveListener(ChangeMovementSpeed);
        InGameUICloseToggle.onValueChanged.RemoveListener(InGameUICloseToggles);
        _cuiColorPicker._onValueChange -= ChangeWheelColor;
        _platformMat.color = _platformDefaultColors;
    }

    private void Start()
    {
        ColorWheel.SetActive(false);
        LoadPersistData();
    }

    private void LoadPersistData()
    {
        var persistData = PersistData.Instance;

        SkyboxColorInput.value = persistData.CurrentSkyColorIndex;
        PlatformColorInput.value = persistData.CurrentPlatformColorIndex;

        _camera.backgroundColor = persistData.CurrentSkyColor;

        if (persistData.CurrentPlatformColor != new Color(0, 0, 0, 0))
        {
            Debug.Log(persistData.CurrentPlatformColor);
            _platformMat.color = persistData.CurrentPlatformColor;
        }

        //purchase persist check
        PlayerSpeedSlider.onValueChanged?.Invoke(persistData.PlayerSpeed);
        InGameUIStatusChange();
        IncrementaUIStatusChange();
        PlayerSpeedText.text = persistData.PlayerSpeed.ToString();
        PlayerFireRateText.text = persistData.FireRate.ToString();
        PlayerRangeText.text = persistData.Range.ToString();
        PlayerPowerText.text = persistData.BulletPower.ToString();
        CameraPositionXSlider.onValueChanged?.Invoke(persistData.CameraPosX);
        CameraPositionYSlider.onValueChanged?.Invoke(persistData.CameraPosY);
        CameraPositionZSlider.onValueChanged?.Invoke(persistData.CameraPosZ);
        CameraRotXSlider.onValueChanged?.Invoke(persistData.CameraRotX);
        CameraRotYSlider.onValueChanged?.Invoke(persistData.CameraRotY);
        CameraFovSlider.onValueChanged?.Invoke(persistData.CameraFov);
        FireRateSlider.onValueChanged?.Invoke(persistData.FireRate);
        RangeSlider.onValueChanged?.Invoke(persistData.Range);
        PowerSlider.onValueChanged?.Invoke(persistData.BulletPower);
    }

    public void EnableEditorPanel()
    {
        _panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void DisableEditorPanel()
    {
        _panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void IncreaseMoney()
    {
        int.TryParse(_moneyTextField.text, out var money);
        PersistData.Instance.Money += money;
        EventManager.OnIncomeChange?.Invoke();
    }


    private void DecreaseMoney()
    {
        int.TryParse(_moneyTextField.text, out var money);
        PersistData.Instance.Money -= money;
        EventManager.OnIncomeChange?.Invoke();
    }
    //  public void LoadScene() => LevelManager.Instance.LoadNewLevel(SelectedScene);

    private void ChangeActiveLevel(int activeIndex) => SelectedScene = activeIndex;

    private void ChangeSkyboxColor(int index)
    {
        PersistData.Instance.CurrentSkyColorIndex = index;

        if (index == 6)
        {
            return;
        }

        ColorUtility.TryParseHtmlString("#" + _skyColors[index], out var newCol);
        PersistData.Instance.CurrentSkyColor = newCol;
        _camera.backgroundColor = newCol;
    }

    private void ChangePlatformColor(int index)
    {
        PersistData.Instance.CurrentPlatformColorIndex = index;

        if (index == 0)
        {
            _platformMat.color = _platformDefaultColors;
            PersistData.Instance.CurrentPlatformColor = new Color(0, 0, 0, 0);
            return;
        }

        if (index == 6)
        {
            return;
        }

        ColorUtility.TryParseHtmlString("#" + _skyColors[index], out var newCol);
        PersistData.Instance.CurrentPlatformColor = newCol;
        _platformMat.color = newCol;
    }

    private void ChangeMovementSpeed(float speed)
    {
        PlayerSpeedText.text = $"{speed:f1}";
        PlayerSpeedSlider.value = speed;
        PersistData.Instance.PlayerSpeed = speed;
        _playerMovementController.SpeedMultiplier = speed;
    }


    private void IncrementalCloseToggles(bool status)
    {
        PersistData.Instance.IncrementalUIOpen = !status;
        IncrementaUIStatusChange();
    }

    private void IncrementaUIStatusChange()
    {
        if (PersistData.Instance.IncrementalUIOpen)
        {
            _inGameUIManager.incrementalsObj.SetActive(true);
            IncrementalCloseToggle.isOn = false;
        }
        else
        {
            _inGameUIManager.incrementalsObj.SetActive(false);
            IncrementalCloseToggle.isOn = true;
        }
    }

    private void InGameUICloseToggles(bool status)
    {
        PersistData.Instance.InGameUIOpen = !status;
        InGameUIStatusChange();
    }

    private void InGameUIStatusChange()
    {
        if (PersistData.Instance.InGameUIOpen)
        {
            _inGameUIManager.GameUIStatus(true);
            InGameUICloseToggle.isOn = false;
        }
        else
        {
            _inGameUIManager.GameUIStatus(false);
            InGameUICloseToggle.isOn = true;
        }
    }

    private void CameraPosX(float amount)
    {
        PersistData.Instance.CameraPosX = amount;
        CameraPositionXSlider.value = amount;
        _cameraController.CameraXPos(amount);
    }

    private void CameraPosY(float amount)
    {
        PersistData.Instance.CameraPosY = amount;
        CameraPositionYSlider.value = amount;
        _cameraController.CameraYPos(amount);
    }

    private void CameraPosZ(float amount)
    {
        PersistData.Instance.CameraPosZ = amount;
        CameraPositionZSlider.value = amount;
        _cameraController.CameraZPos(amount);
    }


    private void CamFov(float amount)
    {
        PersistData.Instance.CameraFov = amount;
        CameraFovSlider.value = amount;
        _cameraController.Fov(amount);
    }

    private void CameraRotX(float amount)
    {
        PersistData.Instance.CameraRotX = amount;
        CameraRotXSlider.value = amount;
        _cameraController.CameraRotateX(amount);
    }

    private void CameraRotY(float amount)
    {
        PersistData.Instance.CameraRotY = amount;
        CameraRotYSlider.value = amount;
        _cameraController.CameraRotateY(amount);
    }

    public void ResetCameraSettings()
    {
        PersistData persistData = PersistData.Instance;
        persistData.CameraPosX = 0f;
        persistData.CameraPosY = -4.79f;
        persistData.CameraPosZ = 3f;
        persistData.CameraRotX = 34.52f;
        persistData.CameraRotY = 0f;
        persistData.CameraFov = 60;


        CameraFovSlider.value = persistData.CameraFov;
        CameraPositionXSlider.value = persistData.CameraPosX;
        CameraPositionYSlider.value = persistData.CameraPosY;
        CameraPositionZSlider.value = persistData.CameraPosZ;
        CameraRotXSlider.value = persistData.CameraRotX;
        CameraRotYSlider.value = persistData.CameraRotY;


        CameraPositionXSlider.onValueChanged?.Invoke(0f);
        CameraPositionYSlider.onValueChanged?.Invoke(-4.79f);
        CameraPositionZSlider.onValueChanged?.Invoke(3f);
        CameraRotXSlider.onValueChanged?.Invoke(34.52f);
        CameraRotYSlider.onValueChanged?.Invoke(0f);
        CameraFovSlider.onValueChanged?.Invoke(60);
    }

    public void ResetDefaultValues()
    {
        var persistData = PersistData.Instance;
        persistData.CurrentLevel = 1;
        persistData.Money = 10;
        EventManager.OnIncomeChange?.Invoke();
        SelectedScene = persistData.CurrentLevel;
        ChangeSkyboxColor(0);
        ChangePlatformColor(0);
        _playerMovementController.SpeedMultiplier = 1f;
        persistData.PlayerSpeed = 1f;
        persistData.FireRate = .5f;
        persistData.BulletPower = 1;
        persistData.Range = 20;
        persistData.IncrementalUIOpen = true;
        persistData.InGameUIOpen = true;
        ChangeMovementSpeed(persistData.PlayerSpeed);
        ChangeFireRate(persistData.FireRate);
        ChangeRange(persistData.Range);
        ChangePower(persistData.BulletPower);
        InGameUIStatusChange();
        IncrementaUIStatusChange();
        PlayerPrefsX.SetIntArray("LevelList", new[] { 1 });
    }

    public void OpenSkyWheel()
    {
        skyOrPlatformWheelIndex = 1;
        ColorWheel.SetActive(true);
    }

    public void OpenPlatformWheel()
    {
        skyOrPlatformWheelIndex = 2;
        ColorWheel.SetActive(true);
    }

    private void ChangeFireRate(float amount)
    {
        PlayerFireRateText.text = $"{amount:f1}";
        PersistData.Instance.FireRate = amount;
        FireRateSlider.value = amount;
        //  _playerAttackController.SetFireRate(amount);
    }

    private void ChangePower(float amount)
    {
        PlayerPowerText.text = $"{amount:f1}";
        PersistData.Instance.BulletPower = amount;
        PowerSlider.value = amount;
    }

    private void ChangeRange(float amount)
    {
        PlayerRangeText.text = $"{amount:f1}";
        PersistData.Instance.Range = amount;
        RangeSlider.value = amount;
        //   _playerAttackController.SetRange(amount);
    }


    private void ChangeWheelColor(Color wheelColor)
    {
        if (skyOrPlatformWheelIndex == 1)
        {
            PersistData.Instance.CurrentSkyColor = wheelColor;
            _camera.backgroundColor = wheelColor;

            PersistData.Instance.CurrentSkyColorIndex = 6;
            SkyboxColorInput.value = 6;
        }

        if (skyOrPlatformWheelIndex == 2)
        {
            PersistData.Instance.CurrentPlatformColor = wheelColor;

            PersistData.Instance.CurrentPlatformColorIndex = 6;
            PlatformColorInput.value = 6;
            _platformMat.color = wheelColor;
        }
    }

    public void DisableColorWheel()
    {
        skyOrPlatformWheelIndex = 0;
        ColorWheel.SetActive(false);
    }
}