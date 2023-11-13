const express = require('express');
const router = express.Router();

const {register, login, userProfile, logout} = require('../controllers/UserController');
const{getUserIDMiddleware} = require('../controllers/ItemController')

router.post('/register', register);
router.post('/login', login);
router.get('/:userID/userprofile', getUserIDMiddleware, userProfile)
router.post('/:userID/logout', logout);

module.exports = router;
