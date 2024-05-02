import React, { useState } from 'react';
import '../css/logreg.css';
import { CiUser, CiLock} from "react-icons/ci";
import { FaEnvelope } from "react-icons/fa";
const LogReg = () => {

  const[action, setAction] = useState('');

  const registerLink = () => {
    setAction('active');
  };

  const loginLink = () => {
    setAction('');
  };

  return (
    <div className={`wrapper ${action}`}>
      <div className='form-box login'>
        <form>
          <h1>Login</h1>
          <div className='input-box'>
              <input type='text'
              placeholder='Username' required/>
               <CiUser className='icon' />
          </div>
          <div className='input-box'>
              <input type='password'
              placeholder='Password' required/>
               <CiLock className='icon'/>
          </div>
            <div className='remember-forgot'>
            <label>
              <input type='checkbox'/>Remember Me
            </label>
            <a href='#'>Forgot Password?</a>
          </div>

          <br></br><button type='submit'>Login</button>

          <div className='register-link'>
              <p>Don't Have an Account? 
                <a href='#' onClick={registerLink}> Register Now!</a>
              </p>
          </div>
        </form>
      </div>
      
      <div className='form-box register'>
        <form>
          <h1>Registration</h1>
          <div className='input-box'>
              <input type='text'
              placeholder='Username' required/>
               <CiUser className='icon' />
          </div>

          <div className='input-box'>
              <input type='email'
              placeholder='Email' required/>
               <FaEnvelope className='icon' />
          </div>

          <div className='input-box'>
              <input type='password'
              placeholder='Password' required/>
               <CiLock className='icon'/>
          </div>

          <div className='remeber-forgot'>
            <label>
              <input type='checkbox'/> I accept all the terms and Conditions
            </label>
          </div>
          <br></br><button type='submit'>Register</button>

          <div className='register-link'>
              <p>Already Have an Account?
                <a href='#' onClick={loginLink}> Login Now!</a>
              </p>
          </div>
        </form>
      </div>


    </div>


    
  );
};

export default LogReg;
