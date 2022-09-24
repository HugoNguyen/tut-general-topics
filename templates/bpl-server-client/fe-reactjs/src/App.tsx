import React from 'react';
import logo from './logo.svg';
import './App.css';

function App() {
  const [cars, setCars] = useState([]);

  useEffect(() => {
    // /api -> proxy
    // localhost:3001 -> backend
    fetch(`/api/cars`)
      .then(data => data.json())
      .then(data => setCars(data))
      .catch(err => console.log(err));
  }, []);

  return (
    <div>
      <div className='car-list'>
        {
          cars.map((car, i) => {
            return (
              <div key={i}>{car.name}, color: {car.color}</div>
            )
          })
        }
      </div>
    </div>
  );
}

export default App;
