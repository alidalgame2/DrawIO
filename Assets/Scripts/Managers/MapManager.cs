using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapManager : SingletonMB<MapManager>
{
    private const int c_MapComputationFactor = 10000;

    public static float s_TileSize = 10.0f;

    private Dictionary<EntityType, List<MappedObject>> m_Entities;
    private bool m_IsInitialized = false;
    private float m_SqrTileFactor;

    public bool initialized { get { return m_IsInitialized; } }

    #region External calls

    public static void RegisterEntity(EntityType _EntityType, MappedObject _Entity)
    {
        if (!Instance.initialized)
            Instance.Init();

       Instance.AddEntity(_EntityType, _Entity);
    }

    public static void UnregisterEntity(EntityType _EntitiyType, MappedObject _Entity)
    {
        // End of the game protection
        if (Instance == null)
            return;

        Instance.m_Entities[_EntitiyType].Remove(_Entity);
    }

    public static void FindEntities(EntityType _EntitiyType, Vector3 _Position, float _SqrRadius, ref List<MappedObject> _Results, int _Layer = -1)
    {
        Instance.Internal_FindEntities(_EntitiyType, _Position, _SqrRadius, ref _Results, _Layer);
    }

  

    public static Player FindNearestPlayer(Player _Player)
    {
        return Instance.Internal_FindNearestPlayer(_Player);
    }

    public static PowerUp FindNearestPowerUp(Player _Player)
    {
        return Instance.Internal_FindNearestPowerUp(_Player);
    }

    #endregion

    private void Init()
    {
        m_IsInitialized = true;
        m_Entities = new Dictionary<EntityType, List<MappedObject>>();
        foreach (EntityType entityValue in Enum.GetValues(typeof(EntityType)))
        {
            m_Entities.Add(entityValue, new List<MappedObject>());
        }

        m_SqrTileFactor = s_TileSize / Mathf.Sqrt(Mathf.PI);
        m_SqrTileFactor *= m_SqrTileFactor;
    }

    private void AddEntity(EntityType _EntitiyType, MappedObject _Entity)
    {
        m_Entities[_EntitiyType].Add(_Entity);
    }

    private void Internal_FindEntities(EntityType _EntityType, Vector3 _Position, float _SqrRadius, ref List<MappedObject> _Results, int _Layer)
    {
        _Results.Clear();
        List<MappedObject> _Entities = m_Entities[_EntityType];
        foreach (MappedObject entity in _Entities)
        {
            if (entity == null || (_Layer != -1 && entity.gameObject.layer != _Layer))
                continue;

            if ((entity.transform.position - _Position).sqrMagnitude < _SqrRadius)
                _Results.Add(entity);
        }
    }

    //private void Internal_FindEntities(Vector3 _Position, float _SqrRadius, ref List<GameObject> _Results, int _Layer)
    //{
    //	Debug.Log("Find Entities");
    //	if (_Results == null)
    //		_Results = new List<GameObject> ();
    //	else
    //		_Results.Clear ();

    //	if (m_Datas == null)
    //		return;

    //	int passCount = GetPassCount(_SqrRadius);

    //	int XBase = Mathf.RoundToInt(_Position.x / s_TileSize);
    //	int ZBase = Mathf.RoundToInt(_Position.z / s_TileSize);

    //	for (int XIndex = -passCount; XIndex <= passCount; ++XIndex)
    //	{
    //		for (int ZIndex = -passCount; ZIndex <= passCount; ++ZIndex)
    //		{
    //			int XCurrent = XBase + XIndex;
    //			int ZCurrent = ZBase + ZIndex;

    //               float x = _Position.x - (s_TileSize * XCurrent);
    //               float z = _Position.z - (s_TileSize * ZCurrent);
    //               float sqrMagnitude = x * x + z * z;
    //               if (sqrMagnitude > _SqrRadius + m_SqrTileFactor)
    //				continue;

    //			int key = XCurrent * c_MapComputationFactor + ZCurrent;

    //			if (m_Datas[key] == null)
    //				continue;

    //			List<int> entityIndexes = (List<int>) m_Datas [key];

    //			for (int index = 0; index < entityIndexes.Count; index++)
    //			{
    //				int entityIndex = entityIndexes [index];
    //				GameObject entity = m_Entities [entityIndex];

    //				if (entity == null || (_Layer != -1 && entity.layer != _Layer))
    //					continue;

    //                   Vector3 entityDiff = entity.transform.position - _Position;

    //				if (entityDiff.sqrMagnitude < _SqrRadius)
    //					_Results.Add (entity);
    //			}
    //		}
    //	}
    //}

    private Player Internal_FindNearestPlayer(Player _Player)
    {
        return null;
    }

    private PowerUp Internal_FindNearestPowerUp(Player _Player)
    {
        return null;
    }

#if UNITY_EDITOR

    public string GetNames()
    {
        string names = "";
        foreach (EntityType entityKey in m_Entities.Keys)
        {
            List<MappedObject> entityList = m_Entities[entityKey];
            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i] == null)
                    names += "<NULL>\n";
                else
                    names += entityList[i].gameObject.name + "\n";
            }
        }
        return names;
    }

#endif
}

public enum EntityType
{
    Player,
    PowerUp
}
