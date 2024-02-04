using System;
using UnityEngine;
using Utilities;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(AnimationDataHolder), menuName = nameof(AnimationDataHolder), order = 1)]

    public class AnimationDataHolder: ScriptableObjectSingleton<AnimationDataHolder>
    {
        public LoadingScreenAnimData LoadingScreenAnimData;
        public PanelAnimData PanelAnimData;
        public WindowAnimData WindowAnimData;
        public ButtonAnimData ButtonAnimData;
    }

    [Serializable]
    public struct LoadingScreenAnimData
    {
        
    }
    
    [Serializable]
    public struct PanelAnimData
    {
        
    }
    
    [Serializable]
    public struct WindowAnimData
    {
        
    }
    
    [Serializable]
    public struct ButtonAnimData
    {
        
    }
}