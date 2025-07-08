import './App.css'
import Table from './Components/Table'
import { Routes, Route } from "react-router-dom";
import Add from '../src/Components/Add';
import MapView from '../src/Components/MapView';
import Update from './Components/Update';


function App() {
  

  return (
    <>
    
    <Routes>
      <Route path="/" element={<><MapView/><Table /></>} />
      <Route path="/add" element={<Add/>} />
      <Route path="/update" element={<Update/>} />
      <Route path="/map" element={<MapView />} />
    </Routes>
    </>
  )
}

export default App
