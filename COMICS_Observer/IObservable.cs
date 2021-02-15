namespace COMICS_Observer
{
    public interface IObservable
    {
        void RegisterObserver(IObserver observer);
        void UnregisterObserver(IObserver observer);
        void Notify(EventType eventName, object data);
    }
}