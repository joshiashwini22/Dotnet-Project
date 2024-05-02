import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './Components/Pages/Home';
import UserProfile from './Components/Pages/UserProfile';
import BlogPost from './Components/Pages/BlogPost';
// Make sure to import Contact if it exists

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/userprofile" element={<UserProfile />} />
        {/* Define other routes here */}
      </Routes>

     
    
    </Router>
  );
}

export default App;
