const express = require('express');
const router = express.Router();

const {register, login, userProfile, logout} = require('../controllers/UserController');
const{getUserIDMiddleware} = require('../controllers/ItemController')

router.post('/register', register);// Route to handle user registration
router.post('/login', login);// Route to handle user login
router.get('/:userID/userprofile', getUserIDMiddleware, userProfile)// Route to fetch user profile information for a specific user ID
router.post('/:userID/logout', logout);// Route to handle user logout

module.exports = router; // Exporting the router containing the user-related routes
