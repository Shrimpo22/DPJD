using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IOpenable{
    public bool IsClosed { get; set; }
    void Open(GameObject player, Canvas canvas);
    void ForceOpen(GameObject player, Canvas canvas);
    void Update();
}
