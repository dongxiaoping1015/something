package FactoryModel;

import java.util.ArrayList;

public abstract class Pizza {
    String name;
//    Dough dough;
//    Sauce sauce;
//    Veggies vessies[];
//    Cheese cheese;
//    Pepperoni pepperoni;
//    Clams clam;

    abstract void prepare();
//    ArrayList toppings = new ArrayList();
//    void prepare() {
//        System.out.println("Preparing " + name);
//        System.out.println("Tossing dough...");
//        System.out.println("Adding sauce...");
//        System.out.println("Adding toppings: ");
//        for (int i = 0; i < toppings.size(); i++) {
//            System.out.println("    " + toppings.get(i));
//        }
//    }
    void bake() {
        System.out.println("Bake for 25 minutes at 350");
    }
    void cut() {
        System.out.println("Cutting the pizza into diagonal slices");
    }
    void box() {
        System.out.println("Place pizza in official PizzaStore box");
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String toString() {
        //这里是打印披萨的代码
        return super.toString();
    }
}
