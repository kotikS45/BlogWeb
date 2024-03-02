import './App.css'
import {Route, Routes} from "react-router-dom";
import Home from './components/home/Home';
import ContainerDefault from './components/containers/ContainerDefault';

const App: React.FC = () => {
  return (
    <Routes>
        <Route path="/" element={<ContainerDefault />}>
            <Route index element={<Home />} />
        </Route>
    </Routes>
  )
}

export default App
