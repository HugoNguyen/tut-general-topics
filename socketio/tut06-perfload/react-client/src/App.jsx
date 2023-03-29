import { useEffect, useState } from 'react';
import './App.css';
import socket from './utilities/socketConnection';
import Widget from './components/Widget';

function App() {
  const [performanceData, setPerformanceData] = useState({});

  const handleData = (data) => {
    const { macA } = data;
    // console.log(data);
    setPerformanceData(curr => ({
      ...curr,
      [macA]: data
    }));
  }

  useEffect(() => {
    socket.on('data', handleData);

    return (() => {
      console.log('app.component unmount');
      socket.off('data', handleData);
    })
  }, []);

  return (
    <div className='App'>
      {
        Object
          .entries(performanceData)
          .map(([key, value]) => <Widget key={key} data={value} />)
      }
    </div>
  )
}

export default App
