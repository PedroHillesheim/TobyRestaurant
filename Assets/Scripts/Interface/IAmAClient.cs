using UnityEngine;

public interface IAmAClient
{
    void NewClient(Sprite clientSprite, float clientWaitingTime);
    void Order(Slot slot);
}
