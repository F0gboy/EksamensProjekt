using EksamensProjekt.DesignPatterns.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.DesignPatterns.ObserverPatterns
{
    public interface IObserver
    {
        void Update(Enemy enemy);
    }

    public interface IObservable
    {
        //Methods for adding, removing and notifying observers
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }
}
