using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonViewBase: MonoBehaviour
    {
        [SerializeField] private protected Button Button;
        
        public IObservable<Unit> OnClickObservable => onClickSubject;
        
        private readonly Subject<Unit> onClickSubject = new Subject<Unit>();

        private void Start()
        {
            Button.OnClickAsObservable().Subscribe(_ => onClickSubject.OnNext(Unit.Default)).AddTo(this);
        }
    }
}