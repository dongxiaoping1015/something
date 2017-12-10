package IteratorAndCombinationPattern;

import java.util.Iterator;

public class Waitress {
    MenuComponent allMenus;

    public Waitress(MenuComponent allMenus) {
        this.allMenus = allMenus;
    }

    public void printMenu() {
        allMenus.print();
    }

    public void printVegetarianMenu() {
        Iterator iterator = allMenus.createIterator();
        System.out.println("\nVEGETARIAN MENU\n----");
        while (iterator.hasNext()) {
            MenuComponent menuComponent =
                    (MenuComponent) iterator.next();
            try {
                if (menuComponent.isVegetarian()) {
                    menuComponent.print();
                }
            } catch (UnsupportedOperationException e) {}
        }
    }
//    Menu pancakeHouseMenu;
//    Menu dinerMenu;
//    Menu cafeMenu;
//
//    public Waitress(Menu pancakeHouseMenu, Menu dinerMenu, Menu cafeMenu) {
//        this.pancakeHouseMenu = pancakeHouseMenu;
//        this.dinerMenu = dinerMenu;
//        this.cafeMenu = cafeMenu;
//    }
//
//    public void printMenu() {
//        Iterator pancakeIterator = pancakeHouseMenu.createIterator();
//        Iterator dinerIterator = dinerMenu.createIterator();
//        Iterator cafeIterator = cafeMenu.createIterator();
//        System.out.print("MENU\n----\nBREAKFAST");
//        printMenu(pancakeIterator);
//        System.out.print("\nLUNCH");
//        printMenu(dinerIterator);
//        System.out.println("\nDINNER");
//        printMenu(cafeIterator);
//    }
//
//    public void printMenu(Iterator iterator) {
//        while (iterator.hasNext()) {
//            MenuItem menuItem = (MenuItem)iterator.next();
//            System.out.print(menuItem.getName() + ", ");
//            System.out.print(menuItem.getPrice() + " -- ");
//            System.out.println(menuItem.getDescription());
//        }
//    }
}
