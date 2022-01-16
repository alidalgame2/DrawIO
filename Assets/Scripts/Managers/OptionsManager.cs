using UnityEngine;

public class OptionsManager : SingletonMB<OptionsManager>
{
    public OptionsMenu m_OptionsMenu;

    public GameObject m_OldMainMenu;
    public GameObject m_NewMainMenu;

    private bool m_collisionsActive;
    private bool m_brushSelectionActive;
    private bool m_dailyRewardsActive;

    public bool CollisionsActive { get => m_collisionsActive; set { PlayerPrefs.SetInt("CollisionsActive", value ? 1 : 0); m_collisionsActive = value; } }
    public bool BrushSelectionActive { get => m_brushSelectionActive; set { PlayerPrefs.SetInt("BrushSelectionActive", value ? 1 : 0); m_brushSelectionActive = value; } }
    public bool DailyRewardsActive { get => m_dailyRewardsActive; set { PlayerPrefs.SetInt("DailyRewardsActive", value ? 1 : 0); m_dailyRewardsActive = value; } }

    public void Init()
    {
        m_collisionsActive = PlayerPrefs.GetInt("CollisionsActive", 1) == 1;
        m_brushSelectionActive = PlayerPrefs.GetInt("BrushSelectionActive", 1) == 1;
        m_dailyRewardsActive = PlayerPrefs.GetInt("DailyRewardsActive", 1) == 1;

        if(m_brushSelectionActive)
        {
            m_NewMainMenu.SetActive(true);
        }
        else
        {
            m_OldMainMenu.SetActive(true);
        }
        m_OptionsMenu.Init();
    }
}
