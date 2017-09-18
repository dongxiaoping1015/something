package DecoratorModel;

public class HouseBlend extends Beverage
{
    public HouseBlend()
    {
        description = "House Blend TemplatePattern.Coffee";
    }

    @Override
    public double cost() {
        return .89;
    }
}
