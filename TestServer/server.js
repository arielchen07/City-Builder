const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');

const app = express();
const PORT = 3000; // Use any available port

app.use(cors({
    credentials: true,
    origin: '*',
    methods: 'GET,POST,OPTIONS',
    allowedHeaders: 'X-ACCESS_TOKEN, Access-Control-Allow-Origin, Authorization, Origin, x-requested-with, Content-Type, Content-Range, Content-Disposition, Content-Description'
}));
app.use(bodyParser.json()); // Middleware to parse JSON request body

app.post('/', (req, res) => {
    console.log('Received POST request:', JSON.stringify(req.body));
    res.send('POST request received');
});

app.get('/', (req, res) => {
    console.log('Received GET request');
    const jsonResponse = {
        structureObjData: [
            {
                name: "WaterTower(Clone)",
                position: { x: 4.0, y: 0.0, z: 2.0 },
                rotation: { x: 0.0, y: 0.0, z: 0.0 }
            },
            {
                name: "CoalPowerPlant(Clone)",
                position: { x: 2.5, y: 0.0, z: 2.5 },
                rotation: { x: 0.0, y: 0.0, z: 0.0 }
            },
            {
                name: "SingleHouse1(Clone)",
                position: { x: 0.5, y: 0.0, z: 2.5 },
                rotation: { x: 0.0, y: 0.0, z: 0.0 }
            }
        ]
    };
    res.json(jsonResponse);
});

app.listen(PORT, () => {
    console.log(`Server running at http://localhost:${PORT}`);
});
