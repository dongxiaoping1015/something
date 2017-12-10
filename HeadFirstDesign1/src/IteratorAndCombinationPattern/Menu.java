package IteratorAndCombinationPattern;

import java.util.ArrayList;
import java.util.Iterator;

public class Menu extends MenuComponent {
    ArrayList menuComponents = new ArrayList();
    String name;
    String desciption;

    public Menu(String name, String desciption) {
        this.name = name;
        this.desciption = desciption;
    }

    @Override
    public void add(MenuComponent menuComponent) {
        menuComponents.add(menuComponent);
    }
    @Override
    public void remove(MenuComponent menuComponent) {
        menuComponents.remove(menuComponent);
    }
    @Override
    public MenuComponent getChild(int i) {
        return (MenuComponent)menuComponents.get(i);
    }

    @Override
    public String getName() {
        return name;
    }
    @Override
    public String getDescription() {
        return desciption;
    }
    @Override
    public void print() {
        System.out.print("\n" + getName());
        System.out.print(", " + getDescription());
        System.out.println("-----------------");

        Iterator iterator = menuComponents.iterator();
        while (iterator.hasNext()) {
            MenuComponent menuComponent =
                    (MenuComponent) iterator.next();
            menuComponent.print();
        }
    }

    public Iterator createIterator() {
        return new CompositeIterator(menuComponents.iterator());
    }
}
