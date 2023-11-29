const express = require('express');
const cors = require('cors');
const logger = require('./logger');
const pino = require('pino-http')({
    // Use our default logger instance, which is already configured
    logger,
  });

const mongoose = require('mongoose');
const morgan = require('morgan');
const bodyParser = require('body-parser')


const UserRoute = require('./routes/UserRoute');
const ItemRoute = require('./routes/ItemRoute');
const MapRoute = require('./routes/MapRoute');

// Connect to the MongoDB database
mongoose.connect('mongodb+srv://admin:020826@cluster0.wersqd9.mongodb.net/lalala?retryWrites=true&w=majority', {
    useNewUrlParser: true,
    useUnifiedTopology: true
});
const db = mongoose.connection;

db.on('error', (err) => {
    console.log(err);
});

db.once('open', () => {
    console.log('Database Connection Established!');
});

const PORT = process.env.PORT || 3000;


const app = express();


// Middleware setup
app.use(morgan('dev'));
app.use(express.json({limit: '10mb'})); // For parsing JSON, request body size limit = 10MB
// app.use(express.urlencoded({ extended: true})); // For parsing URL-encoded data
app.use(pino);
app.use(cors());


// Define routes for user-related operations under the '/api' path


app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});

app.use('/api', UserRoute);
app.use('/api', ItemRoute);
app.use('/api', MapRoute);

module.exports = app
