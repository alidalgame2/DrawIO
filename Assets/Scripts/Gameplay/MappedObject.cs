using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MappedObject : MonoBehaviour 
{
	public Transform 		m_MappedTransform;

	// Cache
	protected Transform		m_Transform;
	private EntityType m_entityType;

	protected virtual void Awake()
	{
		// Cache
		m_Transform = transform;
	}

	protected void RegisterMap(EntityType _EntitiyType)
	{
		m_entityType = _EntitiyType;
		MapManager.RegisterEntity (m_entityType, this);
	}



	protected void UnregisterMap()
	{
		MapManager.UnregisterEntity (m_entityType, this);
	}
}
