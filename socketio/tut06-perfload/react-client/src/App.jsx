import { useEffect, useState } from 'react';
import './App.css';
import socket from './utilities/socketConnection';

function App() {
  const [performanceData, setPerformanceData] = useState({});

  useEffect(() => {
    console.log('app.component mount');
    socket.on('data', data => {
      console.log(data);
    });

    return (() => {
      console.log('app.component unmount');
    })
  }, []);

  return (
    <div className="App">
      <h1>Hello</h1>
    </div>
  )
}

export default App
