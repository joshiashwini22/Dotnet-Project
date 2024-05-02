// UserProfile.js

import React from 'react';
import '../css/UserProfile.css'; // Import CSS file for styling

const UserProfile = () => {
  return (
    
    <div className="box">
    <div className="user-profile">
      <h2>User Profile</h2>
      <div className="profile-details">
        <div className="profile-picture">
          <img src="https://via.placeholder.com/150" alt="Profile" />
        </div>
        <div className="user-info">
          <p>Username: JohnDoe</p>
          <p>Email: johndoe@example.com</p>
          {/* Additional profile details */}
        </div>
      </div>
      <div className="profile-actions">
        <button>Update Profile</button>
        <button>Delete Profile</button>
      </div>
      <div className="changepw">
        <p><a href="#"> Change Password</a></p>
      </div>
    </div>
    </div>
  );
};

export default UserProfile;
