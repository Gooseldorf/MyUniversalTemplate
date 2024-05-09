using System;
using TMPro;
using UI.Base;
using UniRx;

namespace UI.Menu
{
    public class MenuButtonView : ButtonViewBase
    {
        public TextMeshProUGUI Text;
        public IObservable<Unit> OnClickAsObservable => onClickSubject;
    
        private readonly Subject<Unit> onClickSubject = new Subject<Unit>();

        private void Start()
        {
            Button.OnClickAsObservable().Subscribe(_ => onClickSubject.OnNext(Unit.Default)).AddTo(this);
        }
    
        public void SetText(string text) => Text.text = text;

        public void Init()
        {
        
        }
    }
}
