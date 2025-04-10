using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePromptObject : MonoBehaviour
{
    public virtual string GetPromptText() {
        return "Base prompt text.";
    }
}
