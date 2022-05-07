using UnityEngine;

public class PlayerMoneyController : MonoBehaviour
{
    public void IncreaseMoney(int quantity)
    {
        var persistData = PersistData.Instance;
        persistData.Money += quantity;
    }

    public void DecreaseMoney(int quantity)
    {
        var persistData = PersistData.Instance;
        persistData.Money -= quantity;
    }
}