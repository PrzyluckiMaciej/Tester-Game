using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControlsPromptObject : BasePromptObject
{
    public override string GetPromptText() {
        return "Witaj obiekcie testowy #4229. Zanim rozpoczniesz testy z narzêdziem laserowym, musimy zweryfikowaæ twoj¹ koordynacjê ruchow¹. U¿yj "
                + GameInput.Instance.GetBindingText(GameInput.Binding.Forward) + ", "
                + GameInput.Instance.GetBindingText(GameInput.Binding.Back) + ", "
                + GameInput.Instance.GetBindingText(GameInput.Binding.Left) + ", "
                + GameInput.Instance.GetBindingText(GameInput.Binding.Right) + ", "
                + "oraz " + GameInput.Instance.GetBindingText(GameInput.Binding.Jump) + ", aby skakaæ.";
    }
}
