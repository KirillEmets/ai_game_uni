
using UnityEngine;

class ArrowDropItemBehaviour: IDropItemBehaviour
{
    public void OnPickUp(PlayerController playerController)
    {
        playerController.ArrowCount += Random.Range(1, 4);
    }
}

class HealthDropItemBehaviour: IDropItemBehaviour
{
    public void OnPickUp(PlayerController playerController)
    {
        playerController.TakeHeal(Random.Range(50, 50));
    }
}

public interface IDropItemBehaviour
{
    void OnPickUp(PlayerController playerController);
}