using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyInputGenerator : MonoBehaviour
{
    public InputActionAsset playerInput;
    public GameObject inputKeyPrefab;
    public GameObject content;
    private float lineWith = 710f;
    public int numberPerLine = 2;
    private int lineWithCount = 0;
    public int yTranslation;

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        int yCount = 0;
        foreach(InputAction inputAction in playerInput.actionMaps[0].actions)
        {
            //Debug.Log(inputAction.bindings.Count);
            if (inputAction.bindings[0].isComposite)
            {
                int i = 1;
                while (inputAction.bindings[i].isPartOfComposite)
                {
                    GameObject inputKey = Instantiate(inputKeyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    //position ui
                    inputKey.transform.SetParent(content.transform);
                    RectTransform rt = inputKey.GetComponent<RectTransform>();
                    rt.localPosition = new Vector3((lineWith / 2) * lineWithCount, (yTranslation * (yCount / numberPerLine)));

                    inputKey.GetComponent<KeyInput>().indexBinding = i;
                    inputKey.GetComponent<KeyInput>().initializedInputKey(playerInput.actionMaps[0].actions[0]);
                    lineWithCount++;
                    if (lineWithCount == numberPerLine)
                    {
                        lineWithCount = 0;
                    }
                    i++;
                    yCount++;
                }

            } else 
            {
                GameObject inputKey = Instantiate(inputKeyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                //position ui
                inputKey.transform.SetParent(content.transform);
                RectTransform rt = inputKey.GetComponent<RectTransform>();
                rt.localPosition = new Vector3((lineWith / 2) * lineWithCount, (yTranslation * (yCount / numberPerLine)));

                // set action
                inputKey.GetComponent<KeyInput>().initializedInputKey(inputAction);
                lineWithCount++;
                if (lineWithCount == numberPerLine)
                {
                    lineWithCount = 0;
                }
                yCount++;
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
