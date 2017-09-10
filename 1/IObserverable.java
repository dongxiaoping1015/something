public interface IObserverable
{
    public void registerObserver(IObserver observer);
    public void removeObserver(IObserver observer);
    public void notifyObservers();
}