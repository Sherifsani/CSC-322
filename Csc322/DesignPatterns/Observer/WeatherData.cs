namespace Simulation.DesignPatterns.Observer;

public class WeatherData : ISubject
{
    private float temp;
    private float humidity;
    private float pressure;
    private List<IObserver> observers;

    public WeatherData()
    {
        observers = new List<IObserver>();
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        int index = observers.IndexOf(observer);
        if (index >= 0)
        {
            observers.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(temp, humidity, pressure);
        }
    }

    public void MeasurementsChanged()
    {
        NotifyObservers();
    }

    public void SetMeasurements(float temp, float humidity, float pressure)
    {
        this.temp = temp;
        this.humidity = humidity;
        this.pressure = pressure;
        MeasurementsChanged();
    }
    
       
}