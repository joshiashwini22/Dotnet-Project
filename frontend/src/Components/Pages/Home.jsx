import React from 'react';
import '../css/Home.css'; // Import CSS file for styling
import { CiUser } from "react-icons/ci";
import { Link } from 'react-router-dom';
import BlogPost from './BlogPost';

function Home() {
  return (
    <header>
      <nav className='nav'>
        <div className="container">
          <div className="logo">
            <Link to="/">Bislerium Blogs</Link>
          </div>
          <ul className="nav-links">
            <li><Link to="/">Home</Link></li>
            <li><Link to="/recent">Recent</Link></li>
            <li><Link to="/trending">Trending</Link></li>
            <li><Link to="/contact">Popular</Link></li>
            {/* Additional navigation links can be added here */}
          </ul>
          <div className="login">
            <a href="#">Log In</a>
          </div>
          <div>
            <Link to="/userprofile">
              <CiUser className='icon' />
            </Link>
          </div>
        </div>
      </nav>
      <br></br>
      <BlogPost></BlogPost>
      <br></br>
      <BlogPost></BlogPost>
      <br></br>
      <BlogPost></BlogPost>
    </header>
  );
}

export default Home;
