using System;
using TMPro;
using UniRx;
using UnityEngine;
using UI;

public class MenuButtonView : ButtonViewBase
{
    public TextMeshProUGUI Text;
    
    private readonly Subject<Unit> onClickSubject = new Subject<Unit>();
    public new IObservable<Unit> OnClickAsObservable => onClickSubject;

    private void Start()
    {
        // Here we subscribe our button's onClick event to our subject.
        Button.OnClickAsObservable().Subscribe(_ => onClickSubject.OnNext(Unit.Default)).AddTo(this);
        OnClickAsObservable.Subscribe(_ => Debug.Log("Button was clicked"));
    }
    
    public void SetText()
    {
        
    }

    public void Init()
    {
        
    }
}
