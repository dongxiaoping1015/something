public class WeatherData implements IObserverable
{
    private ArrayList observers;
    private float temperature;
    private float humidity;
    private float pressure;
    public WeatherData()
    {
        observers = new ArrayList();
    }
    public void registerObserver(IObserver observer)
    {
        observers.add(o);
    }
    public void removeObserver(IObserver observer)
    {
        int i = observers.indexOf(o);
        if (i >= 0)
        {
            observers.remove(i);
        }
    }
    public void notifyObservers()
    {
        for (int i = 0; i < observers.size(); i++)
        {
            IObserver observer = (IObserver)observers.get(i);
            observer.update(temperature, humidity, pressure);
        }
    }
    public double getTemperature()
    {
        
    }
    public double getHumidity()
    {
        
    }
    public double getPressure()
    {
        
    }
    //实力变量声明
    public void measurementsChanged()
    {        
        notifyObservers();
    }
    public void setMeasurements(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
    }
    //其他WeatherData方法
    
    
    //其他W
    //其他W
    //其他W
    //其他W
    //其他W
    
}