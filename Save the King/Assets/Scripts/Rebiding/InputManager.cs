using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using TMPro;

public class InputManager : MonoBehaviour
{
    public static PlayerControls inputActions;

    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction, int> rebindStarted;

    private void Awake(){
        if(inputActions == null)
            inputActions = new PlayerControls();
    }

    public static void StartRebind(string actionName, int bindingIndex, TextMeshProUGUI statusText, bool excludeMouse){
        InputAction action = inputActions.asset.FindAction(actionName);
        if(action == null || action.bindings.Count <= bindingIndex){
            Debug.Log("Couldn't find action or binding");
            return;
        }

        if(action.bindings[bindingIndex].isComposite){
            var firstPartIndex = bindingIndex + 1;
            if(firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite)
                DoRebind(action, bindingIndex, statusText, true, excludeMouse);
        }else{
            DoRebind(action, bindingIndex, statusText, false, excludeMouse);
        }
    }

    private static void DoRebind(InputAction actionToRebind, int bindingIndex, TextMeshProUGUI statusText, bool allCompositeParts, bool excludeMouse){
        if(actionToRebind == null || bindingIndex < 0)
            return;

        statusText.text = $"Press a Button";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();
            Debug.Log(allCompositeParts);
            if(CheckDuplicateBindings(actionToRebind, bindingIndex, allCompositeParts)){
                DoRebind(actionToRebind, bindingIndex, statusText, allCompositeParts, excludeMouse);
                return;
            }

            if(allCompositeParts){
                var nextBindingIndex = bindingIndex + 1;
                if(nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                    DoRebind(actionToRebind, nextBindingIndex, statusText, allCompositeParts, excludeMouse);
            }

            rebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
        });

        rebind.WithCancelingThrough("<Keyboard>/escape");

        if(excludeMouse)
            rebind.WithControlsExcluding("Mouse");

        rebindStarted?.Invoke(actionToRebind, bindingIndex);
        rebind.Start();
    }

    private static bool CheckDuplicateBindings(InputAction action, int bindingIndex, bool allCompositeParts = false){
        InputBinding newBinding = action.bindings[bindingIndex];
        foreach(InputBinding binding in action.actionMap.bindings){
            if(binding.action == newBinding.action){
                continue;
            }

            if(binding.effectivePath == newBinding.effectivePath){
                Debug.Log("Duplicate binding found");
                return true;
            }
        }

        if(allCompositeParts){
            for(int i = 1; i < bindingIndex; i++){
                if(action.bindings[i].effectivePath == newBinding.overridePath){
                    Debug.Log("Duplicate binding found");
                    return true;
                }
            }
        }else if(newBinding.isPartOfComposite){
            int compositeRootIndex = bindingIndex - 1;
            while (compositeRootIndex >= 0 && !action.bindings[compositeRootIndex].isComposite){
                compositeRootIndex--;
            }
            for (int i = compositeRootIndex + 1; i < action.bindings.Count && action.bindings[i].isPartOfComposite; i++){
                if (i == bindingIndex)
                    continue;
                if (action.bindings[i].effectivePath == newBinding.effectivePath)
                    return true;
            }
        }

        return false;
    }

    public static string GetBindingName(string actionName, int bindingIndex){
        if(inputActions == null)
            inputActions = new PlayerControls();

        InputAction action = inputActions.asset.FindAction(actionName);
        return action.GetBindingDisplayString(bindingIndex);
    }

    public static void ResetBinding(string actionName, int bindingIndex){
        InputAction action = inputActions.asset.FindAction(actionName);

        if(action == null || action.bindings.Count <= bindingIndex){
            Debug.Log("Could not find action or rebind");
            return;
        }

        if(action.bindings[bindingIndex].isComposite){
            for(int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
                action.RemoveBindingOverride(i);
        }else{
            action.RemoveBindingOverride(bindingIndex);
        }

    }
}
