import React from 'react';
import './style.css';

import logoImage from '../../assets/logo.svg'
import padlock from '../../assets/padlock.png'

export default function Login(){
    return (
        <div className="login-container">
            <section className="form">
                <img src={logoImage} alt="Erudio Logo" />
                <form action="">
                    <h1>Access your account</h1>
                    <input placeholder="Username"/>
                    <input type="password" placeholder="password"/>

                    <button type="submit"></button>
                </form>
            </section>

            <img src={padlock} alt="Login" />
        </div>
    )
}