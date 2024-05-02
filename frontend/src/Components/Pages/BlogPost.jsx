// BlogPost.jsx

import React from 'react';
import '../css/BlogPost.css'; // Import CSS file for styling

const BlogPost = () => {
  return (
    <div className="blog-post">
      <img src="https://via.placeholder.com/400" alt="Blog Post" className="blog-post-image" />
      <div className="blog-post-content">
        <h2 className="blog-post-title">Title of the Blog Post</h2>
        <p className="blog-post-author">Author Name</p>
        <p className="blog-post-date">April 30, 2024</p>
        <div className="category">
           <p>Category</p>
          </div>
        <p className="blog-post-text">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut urna eu nulla congue placerat. Nulla facilisi. 
          Vestibulum sodales sapien nec nisi fringilla, at tristique justo luctus. Quisque ut lorem et eros eleifend fermentum.
        </p>
        <div className="readmore">
            <a href="#">Read More</a>
          </div>
      </div>
    </div>
  );
};

export default BlogPost;
