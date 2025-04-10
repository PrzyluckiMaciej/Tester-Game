using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Aby rzuci� obiektem wci�nij "
            + GameInput.Instance.GetBindingText(GameInput.Binding.Throw)
            +". Pami�taj, aby robi� to delikatnie. Nie odpowiadamy za �adne kontuzje.";
    }
}
