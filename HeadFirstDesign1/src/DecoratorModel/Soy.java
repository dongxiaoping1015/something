package DecoratorModel;

public class Soy extends CondimentDecorator {
    Beverage beverage;

    public Soy(Beverage beverage)
    {
        this.beverage = beverage;
        size = beverage.size;
    }

    @Override
    public String getDescription()
    {
        return beverage.getDescription() + ", Soy";
    }

    @Override
    public double cost() {
        double money;
        switch (beverage.getSize())
        {
            case "tall": money = 0.10;break;
            case "grande": money = 0.15;break;
            case "venti": money = 0.20;break;
            default: money = 0;break;
        }
        return money + beverage.cost();
    }
}
