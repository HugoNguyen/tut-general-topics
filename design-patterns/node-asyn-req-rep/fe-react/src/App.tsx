import React, { useEffect, useState } from 'react';
import io, { Socket } from 'socket.io-client';
import axios from 'axios';
import './App.css';
import { useSocket } from './sockets/use-socket';

interface Event {
  id: string,
  connectionId: string,
  createdAt: number;
  completedAt: number;
}

let socket: Socket | undefined;
const backendBaseUrl = '/';
const socketPath = 'ws-socket-io';

const http = axios.create({
  baseURL: backendBaseUrl
});

function App() {

  const [events, setEvents] = useState<Event[]>([]);

  const { isConnected, create, disconnect, on } = useSocket({
    url: backendBaseUrl,
    path: socketPath
  });

  useEffect(() => {
    console.log('mounted');
    return () => {
      console.log('unmounted');
    };
  }, []);

  const makeConnect = () => {
    if (!socket || socket.disconnected) {
      socket = create();
    }

    if (socket) {

      on(socket)('long-processing-completed', (event: Event) => {
        if(!event) return;
        setEvents((prev) => {
          return prev.map(q => q.id !== event.id ? q : ({ ...event }) );
        });
      });

      on(socket)('ping', arg => {
        console.log('pong', arg);
      });
    }
  }

  const makeRequest = async () => {
    let headers = {};

    if(socket && socket.id) {
      headers = {
        'X-CONNECTION-ID': socket.id
      }
    }

    const rs = await http.post<{
      name: string;
      method: string;
      payload: Event;
    }>('/api/long-services', undefined, { headers });

    if (rs.data && rs.data.payload) {
      setEvents([
        ...events,
        rs.data.payload
      ]);
    }
  }


  return (
    <div className='container' style={{ padding: '10px' }}>
      <h1>Async Request - Reply PATTERN</h1>
      <hr />

      <h2>{isConnected ? 'Connected' : 'Disconnected'}</h2>
      {
        isConnected && socket?.id && <h2>SocketId: {socket.id}</h2>
      }

      {
        !isConnected && <button onClick={makeConnect} >Connect</button>
      }

      {
        isConnected && <button onClick={() => socket && disconnect(socket)} >Disconnect</button>
      }

      <br />

      {
        isConnected && <button onClick={makeRequest} >Call Long Processing</button>
      }

      <hr />
      <h3>Result</h3>

      {events.length === 0 && <h4>No tasks</h4>}
      <h4>{events.length}</h4>

      <div>
      {events.length > 0 && events.map(e =>
        <div key={e.id} style={{ margin: '8px', width: '100%', border: '1px solid green', display:'flex', flexDirection: 'column'}}>
          <h5>Id: {e.id}</h5>
          <p>Connection: {e.connectionId}</p>
          <p>Created: {e.createdAt && new Date(e.createdAt).toString()},</p>
          <p>Commpleted: {e.completedAt && new Date(e.completedAt).toString()}</p>
        </div>
      )}
      </div>
      

    </div>
  );
}

export default App;
