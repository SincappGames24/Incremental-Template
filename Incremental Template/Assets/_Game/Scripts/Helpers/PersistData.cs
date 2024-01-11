
using UnityEngine;

public class PersistData : PersistManager<PersistData>
{
  public int CurrentLevel = 1;
  public int LevelLoopStartIndex = 2;
  public float Money = 1000;
  public float GiftFillAmount;
  public int CurrentPlayerRank = 1000;
  public int LastSceneIndex = 2;
  public float HighScoreSignPositionZ;

  #region Incrementals
  
  #region FireRateUpgrade

  public float FireRate = .65f;
  public int FireRateLevel = 1;
  public float FireRateUpgradeCost = 50;
  public int MaxFireRateLevel = 40;
  public int MaxFireRateCost = 1000;

  #endregion

  #region RangeUpgrade

  public float Range = 13;
  public int RangeLevel = 1;
  public float RangeUpgradeCost = 50;
  public int MaxRangeLevel = int.MaxValue;
  public int MaxRangeCost = 1000;

  #endregion
  
  #region IncomeUpgrade

  public float Income=0.5f;
  public int IncomeLevel = 1;
  public float IncomeUpgradeCost = 10;
  public int MaxIncomeLevel = 50;
  public int MaxIncomeCost = 1000;

  #endregion
  
  #endregion
  
  #region HighScore

  public float HighScore;
  public float HighScoreSignLastZPosition;

  #endregion

  #region SpecialEditor

  public Color CurrentSkyColor = new Color(0.65f, 0.83f, 0.97f);
  public Color CurrentPlatformColor;
  public int CurrentSkyColorIndex;
  public int CurrentPlatformColorIndex;
  public float CameraPosX = 0.0f;
  public float CameraPosY = -4.79f;
  public float CameraPosZ = 3f;
  public float CameraRotX = 34.52f;
  public float CameraRotY = 0f;
  public bool InGameUIOpen = true;
  public bool IncrementalUIOpen = true;
  public bool IsClamp = true;
  public float CameraFov = 60;
  public float PlayerSpeed=1f;

  #endregion
  
  #region BulletPowerUpgrade

  public float BulletPower = 1;

  #endregion

}
