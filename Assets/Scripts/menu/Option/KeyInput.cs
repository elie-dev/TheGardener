using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInput : MonoBehaviour
{
    public InputAction inputActionReference = null;
    public TMP_Text nameInput;
    public TMP_Text bindingDisplayNameText = null;
    public GameObject startRebindObject = null;
    public GameObject waitingForInputObject = null;

    public int indexBinding = 0;
    public string title = "";


    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void initializedInputKey(InputAction inputAction)
    {
        inputActionReference = inputAction;

        //Composite bindings (multiple bind in one action)
        if (indexBinding > 0)
        {
            nameInput.text = inputAction.bindings[indexBinding].name;
            bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                inputActionReference.bindings[indexBinding].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        } else
        {
            nameInput.text = inputActionReference.name;
            int bindingIndex = inputActionReference.GetBindingIndexForControl(inputActionReference.controls[0]);

            bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                inputActionReference.bindings[indexBinding].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
    }

    public void Save()
    {
        /*string rebinds = playerController.PlayerInput.actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString(RebindsKey, rebinds);*/
    }

    public void StartRebinding()
    {
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        inputActionReference.Disable();
        rebindingOperation = inputActionReference.PerformInteractiveRebinding(indexBinding)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete()
    {

        bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            inputActionReference.bindings[indexBinding].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);

        inputActionReference.Enable();
    }
}
