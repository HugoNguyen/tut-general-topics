interface Event {
    id?: string,
    connectionId?: string,
    createdAt?: number;
    completedAt?: number;
}

class Store {
    // Tell ts this variable can be undefine
    private _events: Event[] = [];

    get events() {
        return this._events;
    }

    addItem(item: Partial<Event>): Event {
        const newItem = {
            ...item,
            id: `id_${this._events.length + 1}`
        };
        this._events.push(newItem);
        return newItem;
    }

    updateItem(id: string, item: Event): Event {
        const existing = this._events.find(q => q.id === id);

        if(!existing) throw new Error('Item not existing');

        existing.completedAt = item.completedAt;

        return existing;
    }
}

export const store = new Store();