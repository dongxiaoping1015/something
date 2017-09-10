package DecoratorModel;

public abstract class Beverage {
    String description = "Unknown Beverage";
    String size = "Unknown Size";
    public String getDescription() {
        return description;
    }
    public String getSize()
    {
        return size;
    }
    public void setSize(String size)
    {
        this.size = size;
    }
    public abstract double cost();
}
