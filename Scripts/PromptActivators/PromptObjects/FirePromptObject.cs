using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Aby wystrzeliæ promieñ wciœnij "
            + GameInput.Instance.GetBindingText(GameInput.Binding.Fire)
            + ". Wed³ug naszych obliczeñ jest 97% szans, ¿e jest niegroŸny dla cz³owieka."
            ;
    }
}
