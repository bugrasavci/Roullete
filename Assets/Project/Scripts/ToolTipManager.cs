using Assets.Project.Scripts;
using TMPro;
using UnityEngine;

public class ToolTipManager : Singleton<ToolTipManager>
{
    [SerializeField] private GameObject toolTip;
    [SerializeField] private TMP_Text toolTipText;
    private ChipStack Target;

    public void SelectTarget(ChipStack stack)
    {
        Target = stack;

        if (Target)
        {
            toolTip.SetActive(true);
            toolTip.transform.position = Camera.main.WorldToScreenPoint(Target.transform.position);
            toolTipText.text = Target.GetValue().ToString();
        }
    }
    public void Deselect()
    {
        Target = null;
        toolTip.SetActive(false);
    }
}
