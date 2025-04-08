using Assets.Project.Scripts;
using UnityEngine;

public class ChipManager : Singleton<ChipManager>
{
    public Chip selected = null;
    [SerializeField] private GameObject[] Chips;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        selected.OnClick();
    }

    public GameObject InstantiateChip(int index)
    {
        return Instantiate(Instance.Chips[index]);
    }

    public float GetSelectedValue()
    {
        if (selected != null)
            return selected.GetValue;

        return 0;
    }

    public void EnableChips(bool enable)
    {
        canvasGroup.interactable = enable;
    }
}