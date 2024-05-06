using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonViewBase: MonoBehaviour
    {
        [SerializeField] private protected Button Button;
        
        public IObservable<Unit> OnClickObservable => onClickSubject;
        
        private readonly Subject<Unit> onClickSubject = new Subject<Unit>();

        private void Start()
        {
            if(Button!=null) 
                Button.OnClickAsObservable().Subscribe(_ => onClickSubject.OnNext(Unit.Default)).AddTo(this);
            else
                Debug.LogError($"{GetType()} no button component!");
        }
    }
}