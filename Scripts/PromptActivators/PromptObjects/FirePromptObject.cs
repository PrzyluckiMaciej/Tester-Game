using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Aby wystrzeli� promie� wci�nij "
            + GameInput.Instance.GetBindingText(GameInput.Binding.Fire)
            + ". Wed�ug naszych oblicze� jest 97% szans, �e jest niegro�ny dla cz�owieka."
            ;
    }
}
