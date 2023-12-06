const express = require('express');
const router = express.Router();

const {
    createMap, deleteMap, getUserIDMiddleware, getMapIDMiddleware, saveMap, loadAllMaps, loadMap
} = require('../controllers/MapController');

// Routes for handling map-related operations using different HTTP methods and corresponding controller functions
router.post('/:userID/createmap', getUserIDMiddleware, createMap);// Create a map for a specific user

router.post('/:userID/deletemap/:mapID', getUserIDMiddleware, getMapIDMiddleware, deleteMap);// Delete a specific map for a user

router.post('/:mapID/savemap',getMapIDMiddleware, saveMap);// Save changes to a specific map

router.get('/:userID/allmap',getUserIDMiddleware, loadAllMaps);// Load all maps associated with a user

router.get('/:mapID/map',getMapIDMiddleware, loadMap);// Load a specific map

// Exporting the router containing the map-related routes
module.exports = router;