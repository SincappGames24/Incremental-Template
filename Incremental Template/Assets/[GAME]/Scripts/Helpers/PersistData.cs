
using UnityEngine;

public class PersistData : PersistManager<PersistData>
{
  public int CurrentLevel = 1;
  public int LevelLoopStartIndex = 4;
  public float Money = 1000;
  public float GiftFillAmount;
  public int CurrentPlayerRank = 1000;

  #region Incrementals
  
  #region StaminaUpgrade

  public float Stamina = 16;
  public int StaminaLevel = 1;
  public int StaminaUpgradeCost = 10;
  public int MaxStaminaLevel = 40;
  public int MaxStaminaCost = 1000;

  #endregion
    
  #region SpeedUpgrade

  public float Speed = 10;
  public int SpeedLevel = 1;
  public int SpeedUpgradeCost = 10;
  public int MaxSpeedLevel = 20;
  public int MaxSpeedCost = 1000;

  #endregion
    
    
  #region IncomeUpgrade

  public float Income=0.5f;
  public int IncomeLevel = 1;
  public int IncomeUpgradeCost = 10;
  public int MaxIncomeLevel = 50;
  public int MaxIncomeCost = 1000;

  #endregion
  
  #endregion
  
  #region HighScore

  public float HighScore;
  public float HighScoreSignLastZPosition;

  #endregion
}
