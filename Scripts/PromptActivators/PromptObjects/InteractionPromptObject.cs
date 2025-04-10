using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Mo¿esz podnosiæ obiekty wciskaj¹c "
            + GameInput.Instance.GetBindingText(GameInput.Binding.Interactions)
            + " i upuszczaæ je za pomoc¹ "
            + GameInput.Instance.GetBindingText(GameInput.Binding.Drop)
            + ". Pamiêtaj, ¿e odpowiadasz za wszelkie szkody materialne poniesione przez sprzêt laboratoryjny.";
    }
}
