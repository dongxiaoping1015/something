package IteratorAndCombinationPattern;

import java.util.Calendar;
import java.util.Iterator;

public class AlternatingDinerMenuIterator implements Iterator {
    MenuItem[] items;
    int position;

    public AlternatingDinerMenuIterator(MenuItem[] items) {
        this.items = items;
        Calendar rightNow = Calendar.getInstance();
        position = rightNow.get(Calendar.DAY_OF_WEEK) % 2;
    }

    @Override
    public Object next() {
        MenuItem menuItem = items[position];
        position = position + 2;
        return menuItem;
    }

    @Override
    public boolean hasNext() {
        if (position >= items.length || items[position] == null) {
            return false;
        } else {
            return true;
        }
    }

    @Override
    public void remove() {
        throw new UnsupportedOperationException(
                "Alternating Diner Menu Iterator does not support remove()"
        );
    }
}
