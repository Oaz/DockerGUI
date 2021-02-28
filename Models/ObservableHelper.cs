using System;
using System.Collections.Generic;

namespace DockerGUI.Models
{
  public class ObservableHelper<T>
  {
    public ObservableHelper()
    {
      _hashset = new HashSet<IObserver<T>>();
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
      _hashset.Add(observer);
      return new Unsubscriber(this, observer);
    }

    public void ForEach(Action<IObserver<T>> action)
    {
      foreach (var observer in _hashset)
        action(observer);
    }

    private void Unsubscribe(IObserver<T> observer)
    {
      _hashset.Remove(observer);
    }

    private readonly HashSet<IObserver<T>> _hashset;
    
    private class Unsubscriber : IDisposable
    {
      private readonly IObserver<T> _observer;
      private readonly ObservableHelper<T> _observers;

      public Unsubscriber(ObservableHelper<T> observers, IObserver<T> observer)
      {
        _observers = observers;
        _observer = observer;
      }

      public void Dispose()
      {
        _observers.Unsubscribe(_observer);
      }
    }
  }
}