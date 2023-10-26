const express = require('express');
const User = require('../models/UserModel');
const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');





// Register function
const register = async(req, res) => {
    
       
    try {
        const {name, email, password} = req.body;
        console.log(req.body);
        // Check if a user with the provided email already exists
        const existingUser = await User.findOne({ email });
        if (existingUser) {
            return res.status(400).json({ error: "User with this email already exists" });
          }
        //If a new email, create user
        const user = await User.create({name, email, password});
        
        res.status(201).json(user);
      } catch (e) {
        let msg;
        // if(e.code == 11000){
        //   msg = "User already exists"
        // } else {
        //   msg = e.message;
        // }
        console.log(e);
        res.status(400).json(msg)
    }
        
    };



// const login = (req, res, next) =>{
//     var email = req.body.email
//     var password = req.body.password

//     User.findOne({email})
//     .then(user => {
//         if(user){
//             if(password != user.password){
//                 // console.log('password:', password);
//                 // console.log('user.password', user.password);
//                 res.json({
//                     message:'Password does not matched!'
//                 })
//             }else{
//                 res.json({
//                     message:'Login Successfully!'
//                 })
//             }
//         }else{
//             res.json({
//                 message: 'No user found!'
//             })
//         }
//     })
// }

const login = async (req, res) => {
    try {
      const { email, password } = req.body;
      console.log(req.body)
      const user = await User.findOne({ email });
  
      if (!user) {
        return res.status(401).json({ error: 'User not found' });
      }
      const isPasswordValid = await bcrypt.compare(password, user.password);
  
      if (!isPasswordValid) {
        return res.status(401).json({ error: 'Password not match' });
      }
      user.status = 'online';
      await user.save();

      const token = jwt.sign({ userId: user._id }, 'your-secret-key', { expiresIn: '1h' });
  
      res.status(200).json({ user, token });
    } catch (e) {
      console.error(e);
      res.status(500).json({ error: 'Internal server error' });
    }
  };


  const userProfile = async (req, res) => {
    const userID = req.params.userID;
    console.log(userID)
    User.findById(userID)
      .then(user => {
        if (!user) {
          return res.status(404).json({ message: 'User not found' });
        }
        res.json(user);
      })
      .catch(error => {
        console.error(error);
        res.status(500).json({ message: 'Internal Server Error' });
      });
  };

  
  
  
module.exports = {register, login, userProfile};
