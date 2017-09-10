package FactoryModel;

public class NYStylePizzaStore extends PizzaStore {
    Pizza createPizza(String item) {
        Pizza pizza = null;
        PizzaIngredientFactory ingredientFactory =
                new NYPizzaIngredientFactory();
        if (item.equals("cheese")) {
            //return new NYStyleCheesePizza();
            pizza = new CheesePizza(ingredientFactory);
            pizza.setName("New York Style Cheese Pizza");
        } else if (item.equals("clam")) {
            pizza = new ClamPizza(ingredientFactory);
            pizza.setName("New York Style Clam Pizza");
        } //else return null;
        return pizza;
    }
}
