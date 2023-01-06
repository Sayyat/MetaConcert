using System.Runtime.InteropServices;
using UnityEngine;

namespace WebGLScripts
{
    public class WebNativeDialog
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern string NativeDialogPrompt(string title, string defaultValue);
    [DllImport("__Internal")]
    private static extern string SetupOverlayDialogHtml(string title , string defaultValue,string okBtnText,string cancelBtnText);

    [DllImport("__Internal")]
    private static extern bool IsOverlayDialogHtmlActive();
    [DllImport("__Internal")]
    private static extern bool IsOverlayDialogHtmlCanceled();
    [DllImport("__Internal")]
    private static extern string GetOverlayHtmlInputFieldValue();
    [DllImport("__Internal")]
    private static extern void HideUnityScreenIfHtmlOverlayCant();
    [DllImport("__Internal")]
    private static extern bool IsRunningOnEdgeBrowser();
            
    [DllImport("__Internal")]
    private static extern bool IsMobileBrowser();
    [DllImport("__Internal")]
    private static extern bool IsAndroidBrowser();
    [DllImport("__Internal")]
    private static extern bool IsIosBrowser();
#endif

        public static string OpenNativeStringDialog(string title, string defaultValue)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return NativeDialogPrompt(title, defaultValue);
#else
            return defaultValue;
#endif
        }

        public static void SetUpOverlayDialog(string title, string defaultValue, string okBtnText, string cancelBtnText)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Screen.fullScreen)
        {
            if( IsRunningOnEdgeBrowser() ){
                Screen.fullScreen = false;
            }else{
                HideUnityScreenIfHtmlOverlayCant();
            }
        }
        SetupOverlayDialogHtml(title, defaultValue,okBtnText,cancelBtnText);
#else
#endif
        }


        public static bool IsOverlayDialogActive()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsOverlayDialogHtmlActive();
#else
            return false;
#endif
        }

        public static bool IsOverlayDialogCanceled()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsOverlayDialogHtmlCanceled();
#else
            return false;
#endif
        }

        public static string GetOverlayDialogValue()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return GetOverlayHtmlInputFieldValue();
#else
            return "";
#endif
        }

        public static bool IsMobileOnWebgl()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsMobileBrowser();
#else
            return false;
#endif
        }
        
        public static bool IsAndroidOnWebgl()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsAndroidBrowser();
#else
                return false;
#endif
        }
        
        public static bool IsIosOnWebgl()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsIosBrowser();
#else
                return false;
#endif
        }
    }
}