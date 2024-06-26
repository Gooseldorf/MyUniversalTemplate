﻿using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public abstract class ButtonViewBase: MonoBehaviour
    {
        [SerializeField] private protected Button Button;
        
        public IObservable<Unit> OnClickStream => onClickSubject;
        
        private readonly Subject<Unit> onClickSubject = new Subject<Unit>();

        private void Start()
        {
            if(Button!=null) 
                Button.OnClickAsObservable().Subscribe(_ => onClickSubject.OnNext(Unit.Default)).AddTo(this);
            else
                Debug.LogError($"{GetType()} no button component!");
        }

        public void Toggle(bool enable) => Button.interactable = enable;
    }
}