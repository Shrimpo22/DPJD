using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IDoor{
    bool IsLocked { get; set; }
    bool IsClosed { get; set; }
    float RotationSpeed { get; set; }

    void Start();
    void OpenDoor(GameObject player);
    void TextActivate();
    void TextClear();
    void OnTriggerExit(Collider other);
    void Update();
}
