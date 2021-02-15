namespace COMICS_Observer
{
    public interface IObserver
    {
        void OnNotify(EventType eventName, 
            object data);
    }
}