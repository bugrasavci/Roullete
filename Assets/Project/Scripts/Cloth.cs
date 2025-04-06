using Assets.Project.Scripts;
using UnityEngine;

public class Cloth : Singleton<Cloth>
{
    [SerializeField] private GameObject chipStackPrefab;

    public ChipStack InstanceStack()
    {
        GameObject ob = Instantiate(chipStackPrefab);

        return ob.GetComponent<ChipStack>();
    }
}
