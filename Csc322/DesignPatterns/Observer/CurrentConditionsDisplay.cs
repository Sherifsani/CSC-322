namespace Simulation.DesignPatterns.Observer;

public class CurrentConditionsDisplay : IDisplayElement, IObserver
{
    private float humidity;
    private float temp;
    private ISubject weatherData;

    public CurrentConditionsDisplay(ISubject weatherData)
    {
        this.weatherData = weatherData;
        weatherData.RegisterObserver(this);
    }

    public void Display()
    {
        Console.WriteLine("Current Condition: Temperature: {0} Degrees C, Pressure: {1} mmHg", temp, humidity);
    }

    public void Update(float temp, float humidity, float pressure)
    {
        this.humidity = humidity;
        this.temp = temp;
        Display(); 
    }
}