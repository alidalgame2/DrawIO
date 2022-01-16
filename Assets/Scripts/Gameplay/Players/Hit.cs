using UnityEngine;
using System.Collections.Generic;

public class Hit : MonoBehaviour
{
    private Player player;
    private List<MappedObject> m_SearchBuffer = new List<MappedObject>();
   

    void Awake()
    {
        this.enabled = OptionsManager.Instance.CollisionsActive;
        player = GetComponent<Player>();
    }

    private void LateUpdate()
    {
        ComputePlayerCollisions();
    }

    private void ComputePlayerCollisions()
    {
        if (player.isDead)
            return;

        float size = player.GetSize() * 1.2f + 4f;
        MapManager.FindEntities(EntityType.Player, transform.position, size * size, ref m_SearchBuffer);

        for (int i = 0; i < m_SearchBuffer.Count; i++)
        {
            if (m_SearchBuffer[i] != this.player)
            {
                Player enemy = m_SearchBuffer[i] as Player;
                player.HitOther(enemy);
            }
        }
    }
}
