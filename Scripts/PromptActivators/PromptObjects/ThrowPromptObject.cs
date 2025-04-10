using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Aby rzuciæ obiektem wciœnij "
            + GameInput.Instance.GetBindingText(GameInput.Binding.Throw)
            +". Pamiêtaj, aby robiæ to delikatnie. Nie odpowiadamy za ¿adne kontuzje.";
    }
}
