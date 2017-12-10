package IteratorAndCombinationPattern;

public class MenuTestDrive {
    public static void main(String args[]) {
        MenuComponent pancakeHouseMenu =
                new Menu("PANCAKE HOUSE MENU", "BREAKFAST");
        MenuComponent dinerMenu =
                new Menu("DINER MENU", "Lunch");
        MenuComponent cafeMenu =
                new Menu("CAFE MENU", "Dinner");
        MenuComponent dessertMenu =
                new Menu("DESSERT MENU", "Dessert of course!");

        MenuComponent allMenus =  new Menu("ALL MENUS", "All menu combined");

        allMenus.add(pancakeHouseMenu);
        allMenus.add(dinerMenu);
        allMenus.add(cafeMenu);

        dinerMenu.add(new MenuItem(
                "Pasta",
                "Spaghetti with Marinara Sauce, and a slice of sourdough bread",
                true,
                3.89
        ));
        dinerMenu.add(dessertMenu);

        dessertMenu.add(new MenuItem(
                "Apple Pie",
                "Apple pie with a flakey crust, topped with vanilla ice cream",
                true,
                1.59
        ));

        Waitress waitress = new Waitress(allMenus);
        waitress.printVegetarianMenu();
        //waitress.printMenu();
//        PancakeHouseMenu pancakeHouseMenu = new PancakeHouseMenu();
//        DinerMenu dinerMenu = new DinerMenu();
//        CafeMenu cafeMenu = new CafeMenu();
//
//        Waitress waitress = new Waitress(pancakeHouseMenu, dinerMenu, cafeMenu);
//
//        waitress.printMenu();
    }
}
