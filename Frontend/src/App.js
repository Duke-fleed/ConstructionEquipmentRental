import './App.css';
import ResponsiveAppBar from './components/AppBar';
import RentalOrderComponent from './components/RentalOrderComponent'

function App() {
  return (
    <div className="App">
      <ResponsiveAppBar></ResponsiveAppBar>
      <RentalOrderComponent></RentalOrderComponent>
    </div>
  );
}

export default App;
