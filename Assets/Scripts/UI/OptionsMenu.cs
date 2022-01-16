using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle m_CollisionsActiveToggle;
    public Toggle m_BrushSelectionActiveToggle;
    public Toggle m_DailyRewardsActiveToggle;
    public void Init()
    {
        m_CollisionsActiveToggle.isOn = OptionsManager.Instance.CollisionsActive;
        m_BrushSelectionActiveToggle.isOn = OptionsManager.Instance.BrushSelectionActive;
        m_DailyRewardsActiveToggle.isOn = OptionsManager.Instance.DailyRewardsActive;
    }

    public void HandleCollisionActiveToggleChange()
    {
        OptionsManager.Instance.CollisionsActive = m_CollisionsActiveToggle.isOn;
    }

    public void HandleBrushSelectionActiveToggleChange()
    {
        OptionsManager.Instance.BrushSelectionActive = m_BrushSelectionActiveToggle.isOn;
    }

    public void HandleDailyRewardsActiveToggleChange()
    {
        OptionsManager.Instance.BrushSelectionActive = m_DailyRewardsActiveToggle.isOn;
    }

    public void HandleDoneSettingsButton()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
