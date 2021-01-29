﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class KeyManager : MonoBehaviour
{
    public InputActionAsset control;

    public static KeyManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        LoadControlOverrides();
    }

    /// <summary>
    /// stores the active control overrides to player prefs
    /// </summary>
    public void StoreControlOverrides()
    {
        //saving
        BindingWrapperClass bindingList = new BindingWrapperClass();
        foreach (var map in control.actionMaps)
        {
            foreach (var binding in map.bindings)
            {
                if (!string.IsNullOrEmpty(binding.overridePath))
                {
                    bindingList.bindingList.Add(new BindingSerializable(binding.id.ToString(), binding.overridePath));
                }
            }
        }

        PlayerPrefs.SetString("ControlOverrides", JsonUtility.ToJson(bindingList));
        Debug.Log(PlayerPrefs.GetString("ControlOverrides"));
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads control overrides from playerprefs
    /// </summary>
    public void LoadControlOverrides()
    {
        if (PlayerPrefs.HasKey("ControlOverrides"))
        {
            BindingWrapperClass bindingList = JsonUtility.FromJson(PlayerPrefs.GetString("ControlOverrides"), typeof(BindingWrapperClass)) as BindingWrapperClass;

            //create a dictionary to easier check for existing overrides
            Dictionary<System.Guid, string> overrides = new Dictionary<System.Guid, string>();
            foreach (var item in bindingList.bindingList)
            {
                overrides.Add(new System.Guid(item.id), item.path);
            }

            //walk through action maps check dictionary for overrides
            foreach (var map in control.actionMaps)
            {
                var bindings = map.bindings;
                //Debug.Log("Loading overrides for:" + map.name);
                for (var i = 0; i < bindings.Count; ++i)
                {
                    if (overrides.TryGetValue(bindings[i].id, out string overridePath))
                    {
                        //if there is an override apply it
                        //Debug.Log(overridePath);
                        map.ApplyBindingOverride(i, new InputBinding { overridePath = overridePath });
                    }
                }
            }
        }
    }

    public void ResetKeybindings()
    {
        foreach (var map in control.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        PlayerPrefs.DeleteKey("ControlOverrides");
    }

    /// <summary>
    /// Private wrapper class for json serialization of the overrides
    /// </summary>
    [System.Serializable]
    class BindingWrapperClass
    {
        public List<BindingSerializable> bindingList = new List<BindingSerializable>();
    }

    /// <summary>
    /// internal struct to store an id overridepath pair for a list
    /// </summary>
    [System.Serializable]
    public struct BindingSerializable
    {
        public string id;
        public string path;

        public BindingSerializable(string bindingId, string bindingPath)
        {
            id = bindingId;
            path = bindingPath;
        }
    }

    public void OnDash()
    {
        Debug.Log("dash");
    }
}
