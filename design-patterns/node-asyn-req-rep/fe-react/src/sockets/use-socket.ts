import { useState, useEffect } from 'react';
import io, { Socket } from 'socket.io-client';

interface Option {
    url: string;
    path: string;
}

export const useSocket = (option: Partial<Option>) => {
    const [socketOpt, _] = useState<Partial<Option>>(option);
    const [isConnected, setIsConnected] = useState(false);
    const [events, setEvents] = useState<string[]>([]);

    const create = () => {
        if(!socketOpt) return;
        if(!socketOpt.url) return;

        const socket = io(socketOpt.url, {
            autoConnect: true,
            path: `/${socketOpt.path}/`
        });

        on(socket)('connect', () => {
            setIsConnected(true);
            console.log(`${socket.id} connected`);
        });
    
        on(socket)('disconnect', () => {
            setIsConnected(false);
        });

        return socket;
    }

    const on = (socket: Socket) => (ev: string, listener = (...arg: any[]) => {}) => {
        if(!socket) return;
        if(!ev) return;
        if(events.includes(ev)) return;
        socket.on(ev, listener);
        setEvents([...events, ev]);
    }

    const off = (socket: Socket) => (ev: string) => {
        if(!socket) return;
        if(!ev) return;
        if(!events.includes(ev)) return;
        socket.off(ev);
        setEvents(events.filter(q => q !== ev));
    }

    const disconnect = (socket: Socket) => {
        if(!socket) return;
    
        socket.disconnect();
    
        events.forEach(ev => off(socket)(ev));
    }

    return {
        isConnected,
        create,
        disconnect,
        on,
        off
    }
}