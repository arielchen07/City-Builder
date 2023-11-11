const express = require('express');
const router = express.Router();

const {
    createMap, deleteMap, getUserIDMiddleware, getMapIDMiddleware, saveMap, loadAllMaps, loadMap
} = require('../controllers/MapController');

router.post('/:userID/createmap', getUserIDMiddleware, createMap);

router.post('/:userID/deletemap/:mapID', getUserIDMiddleware, getMapIDMiddleware, deleteMap);

router.post('/:mapID/savemap',getMapIDMiddleware, saveMap);

router.get('/:userID/allmap',getUserIDMiddleware, loadAllMaps);

router.get('/:mapID/map',getMapIDMiddleware, loadMap);


module.exports = router;