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

app.put('/', (req, res) => {
   console.log('Received PUT request:', JSON.stringify(req.body), req.body);
   res.send('POST request received');
});

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
            },
            {
                name: "Road(Clone)",
                position: { x: 4.0, y: 0.0, z: 1.0 },
                rotation: { x: 0.0, y: 0.0, z: 0.0 }
            },
            {
                name: "Road(Clone)",
                position: { x: 3.0, y: 0.0, z: 1.0 },
                rotation: { x: 0.0, y: 0.0, z: 0.0 }
            },
            {
                name: "Road(Clone)",
                position: { x: 2.0, y: 0.0, z: 1.0 },
                rotation: { x: 0.0, y: 0.0, z: 0.0 }
            },
            {
                name: "Road(Clone)",
                position: { x: 1.0, y: 0.0, z: 1.0 },
                rotation: { x: 0.0, y: 0.0, z: 0.0 }
            },
            {
                name: "Road(Clone)",
                position: { x: 0.0, y: 0.0, z: 1.0 },
                rotation: { x: 0.0, y: 0.0, z: 0.0 }
            }
        ],	
	   tileData:[
      {
         name:"grass1 (15)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":-4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (18)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (15)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (11)",
         position:{
            "x":-1.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (7)",
         position:{
            "x":-1.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (8)",
         position:{
            "x":-1.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (22)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (33)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (41)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":-2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (26)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (40)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":-1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (24)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (27)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":-3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (15)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (13)",
         position:{
            "x":-1.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (11)",
         position:{
            "x":-2.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (8)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (6)",
         position:{
            "x":-4.0,
            "y":-0.10000000149011612,
            "z":-2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (2)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":-2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (17)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (6)",
         position:{
            "x":-4.0,
            "y":-0.10000000149011612,
            "z":-4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (43)",
         position:{
            "x":-1.0,
            "y":-0.10000000149011612,
            "z":-1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (4)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":-3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (10)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (16)",
         position:{
            "x":-3.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (3)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":-1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (16)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":-1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (10)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (38)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (22)",
         position:{
            "x":-2.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (35)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3",
         position:{
            "x":-1.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (9)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (42)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":-4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (21)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (10)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (27)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (14)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (12)",
         position:{
            "x":-3.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (18)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (19)",
         position:{
            "x":-4.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (8)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (30)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (36)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (25)",
         position:{
            "x":-1.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (20)",
         position:{
            "x":-1.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (5)",
         position:{
            "x":-4.0,
            "y":-0.10000000149011612,
            "z":-3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (23)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":-4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (21)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (31)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":-4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (12)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (13)",
         position:{
            "x":-2.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (20)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (7)",
         position:{
            "x":-2.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (28)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":-2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (3)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":-1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (37)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (4)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":-3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (4)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":-3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (28)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (22)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":-3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (9)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (33)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":-4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (3)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":-1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (23)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (44)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (11)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (2)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":-3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (2)",
         position:{
            "x":-3.0,
            "y":-0.10000000149011612,
            "z":-4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (34)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (25)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (17)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (26)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (30)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":-2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (7)",
         position:{
            "x":-2.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (34)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (39)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":-1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (29)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":-2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (29)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (23)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (18)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":-2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":0.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (20)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":-1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (19)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (12)",
         position:{
            "x":-4.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (31)",
         position:{
            "x":4.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (24)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (17)",
         position:{
            "x":-2.0,
            "y":-0.10000000149011612,
            "z":-5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (24)",
         position:{
            "x":3.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (13)",
         position:{
            "x":-2.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (14)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (16)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":-3.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (14)",
         position:{
            "x":-5.0,
            "y":-0.10000000149011612,
            "z":2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (21)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (32)",
         position:{
            "x":0.0,
            "y":-0.10000000149011612,
            "z":5.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (36)",
         position:{
            "x":2.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass2 (25)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":-2.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (19)",
         position:{
            "x":5.0,
            "y":-0.10000000149011612,
            "z":-4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass1 (35)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":4.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      },
      {
         name:"grass3 (9)",
         position:{
            "x":1.0,
            "y":-0.10000000149011612,
            "z":1.0
         },
         rotation:{
            "x":0.0,
            "y":0.0,
            "z":0.0
         },
         isOccupied:false
      }
   ]
	
    };
    res.json(jsonResponse);
});

app.listen(PORT, () => {
    console.log(`Server running at http://localhost:${PORT}`);
});
