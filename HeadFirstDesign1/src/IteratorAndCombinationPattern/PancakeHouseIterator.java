package IteratorAndCombinationPattern;

import java.util.ArrayList;

public class PancakeHouseIterator implements Iterator {
    ArrayList items;
    int position = 0;

    public PancakeHouseIterator(ArrayList items) {
        this.items = items;
    }

    @Override
    public Object next() {
        MenuItem item = (MenuItem) items.get(position);
        position = position + 1;
        return item;
    }

    @Override
    public boolean hasNext() {
        if (position >= items.size() || items.get(position) == null) {
            return false;
        } else {
            return true;
        }
    }
}
