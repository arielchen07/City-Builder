using System;

interface IProvider {
    void OnPlace();
    void OnRemove();
    (string, int) GetProvided();
    void UpdateAllocation(int consumption);
}
