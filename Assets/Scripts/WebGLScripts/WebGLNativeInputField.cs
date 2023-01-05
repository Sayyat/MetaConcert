using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WebGLScripts
{
    public class WebGLNativeInputField : TMPro.TMP_InputField
    {
        public enum EDialogType
        {
            PromptPopup,
            OverlayHtml,
        }
        public string m_DialogTitle = "Введите имя";
        public string m_DialogOkBtn = "OK";
        public string m_DialogCancelBtn = "Отмена";
        public EDialogType m_DialogType = EDialogType.OverlayHtml;

#if UNITY_WEBGL && !UNITY_EDITOR 

    public override void OnSelect(BaseEventData eventData)
    {
        Debug.Log("<Color=Green>Check user device</Color>");
        if (!WebNativeDialog.IsMobileOnWebgl())
        {
            Debug.Log("<Color=Green>User device is Desktop</Color>");
            return;
        }

        var device = (WebNativeDialog.IsAndroidOnWebgl()) ? "Android" : "IOS";
        
        Debug.Log($"<Color=Green>User device is Mobile ({device})</Color>");
            
        
        m_DialogType = WebNativeDialog.IsAndroidOnWebgl() ? EDialogType.OverlayHtml : EDialogType.PromptPopup;
        
        switch( m_DialogType ){
            case EDialogType.PromptPopup:
                this.text = WebNativeDialog.OpenNativeStringDialog(m_DialogTitle, this.text);
                StartCoroutine(this.DelayInputDeactive());
                break;
            case EDialogType.OverlayHtml:
                WebNativeDialog.SetUpOverlayDialog(m_DialogTitle, this.text , m_DialogOkBtn , m_DialogCancelBtn );
                StartCoroutine(this.OverlayHtmlCoroutine());
                break;
        }
    }
    private IEnumerator DelayInputDeactive()
    {
        yield return new WaitForEndOfFrame();
        this.DeactivateInputField();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private IEnumerator OverlayHtmlCoroutine()
    {
        yield return new WaitForEndOfFrame();
        this.DeactivateInputField();
        EventSystem.current.SetSelectedGameObject(null);
        WebGLInput.captureAllKeyboardInput = false;
        while (WebNativeDialog.IsOverlayDialogActive())
        {
            yield return null;
        }
        WebGLInput.captureAllKeyboardInput = true;

        if (!WebNativeDialog.IsOverlayDialogCanceled())
        {
            this.text = WebNativeDialog.GetOverlayDialogValue();
        }
    }
    
#endif
    }
}
