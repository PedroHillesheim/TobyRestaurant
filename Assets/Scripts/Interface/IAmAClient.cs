using UnityEngine;

public interface IAmAClient
{
    void NewClient(Sprite clientSprite, float clientWaitingTime, Slot slot, int damege);
    void Order(Slot slot);
}
